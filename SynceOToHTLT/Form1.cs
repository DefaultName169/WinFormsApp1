using BSS.DataValidator;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using SynceOToHTLT.DataAccess.Common;
using SynceOToHTLT.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace SynceOToHTLT
{
    public partial class Form1 : Form
    {
        public static Form1 ins;
        private readonly EOService _service;
        private readonly DbContext _dbContextHTLT;
        private readonly DbContext _dbContextConvert;
        private readonly DbContext _dbContextConvertTab3;
        public string cbbtab3_eo_table_last;
        MenuStrip menuStrip;
        Dictionary<string, List<string>> datalist;
        public Dictionary<string, string> ConverttableTab3, ConvertRam, Convert_this_select;
        //List<datatable> datatables;
        public Form1()
        {
            InitializeComponent();
            _service = new EOService();
            _dbContextHTLT = new DbContext(Program.AppSettings.ConnectionSetting.HtltConnectionString);
            _dbContextConvert = new DbContext(Program.AppSettings.ConnectionSetting.ConvertConnectionString);
            _dbContextConvertTab3 = new DbContext(Program.AppSettings.ConnectionSetting.ConvertTab3ConnectionString);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ConverttableTab3 = new Dictionary<string, string>();
            datalist = new Dictionary<string, List<string>>();
            var TableEO = _service._dbContext.GetSQLServer<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME");
            var Tablehtlt = _dbContextHTLT.GetSQLServer<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME");
            var TableConvertTab2 = _dbContextConvert.GetSQLServer<dynamic>("select * from Converted");
            lvtab2.View = View.Details;
            lvtab2.GridLines = true;
            lvtab2.FullRowSelect = true;
            lvtab2.MultiSelect = true;

            //Thêm tiêu đề cho cột
            lvtab2.Columns.Add("Nguồn", 197);
            lvtab2.Columns.Add("", 50);
            lvtab2.Columns.Add("Đích", 197);
            lvtab2.Columns[1].TextAlign = HorizontalAlignment.Center;
            List<string> newdataeo = new List<string>();
            List<string> newdatahtlt = new List<string>();

            foreach (var tableeo in TableEO) 
            {
                newdataeo.Add(tableeo);
                List<string> newlist = new List<string>();
                dynamic columneo = _service._dbContext.GetSQLServer<dynamic>("SELECT name, system_type_id as [type] FROM sys.columns WHERE [object_id] = OBJECT_ID('dbo."+ tableeo + "', 'U')");
                foreach(var item in columneo)
                {
                    newlist.Add(item.name.ToString().PadRight(30) + "(" + check_type_id(item.type) + ")");
                }
                datalist.Add(tableeo, newlist);
            }

            foreach (var tablehtlt in Tablehtlt)
            {
                newdatahtlt.Add(tablehtlt);
            }
            cbbtab2_eo_table.DataSource = newdataeo;
            cbbtab3_eo_table.DataSource = newdatahtlt;
            cbbtab2_htlt_table.DataSource = newdatahtlt;
            cbbtab3_eo_table_last = cbbtab3_eo_table.Text;
            foreach(dynamic item in TableConvertTab2)
            {
                AddNewListViewItemTab2(item.Eoffice, item.htlt); 
            }

            dynamic exists = _dbContextConvertTab3.GetSQLServer<dynamic>("SELECT * from [ConVert]");
            foreach(var item in exists)
            {
                ConverttableTab3.Add(item.key, item.value);
            }
            ConvertRam = ConverttableTab3;
        }

        //
        //tab1
        //
        private void btntab1_Click(object sender, EventArgs e)
        {
            int count = 10;
            var rs = _service.GetListCongVan(count);
            progressBartab1.Maximum = count;
            progressBartab1.Step = 1;
            var num = rs.Current;
            while (rs.MoveNext())
            {
                progressBartab1.PerformStep();
            }

            MessageBox.Show("Chuyển dữ liệu thành công");
        }

        //
        //tab2
        //

        private void btntab2_Chuyen_Click(object sender, EventArgs e)
        {
            string eoselected = "[" + cbbtab2_eo_table.SelectedItem + "] : [" + cbbtab2_eo_column.SelectedItem + "]";
            string htmlselected = "[" + cbbtab2_htlt_table.SelectedItem + " : " + cbbtab2_htlt_column.SelectedItem + "]";
            AddNewListViewItemTab2(eoselected, htmlselected);
        }

        void AddNewListViewItemTab2(string eoselected, string htmlselected)
        {
            bool exist = true;
            string[] arr = new string[3];
            arr[0] = eoselected;
            arr[1] = "⬌";
            arr[2] = htmlselected;
            for (int i = 0; i < lvtab2.Items.Count; i++)
            {
                if (arr[0] == lvtab2.Items[i].SubItems[0].Text.ToString() && arr[2] == lvtab2.Items[i].SubItems[2].Text.ToString())
                {
                    MessageBox.Show("Đã tồn tại!!!!!");
                    exist = false;
                    break;
                }
            }
            if (exist)
            {
                ListViewItem itm = new ListViewItem(arr);
                lvtab2.Items.Add(itm);
            }
        }

        public void btntab2_Del_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lvtab2.SelectedItems)
            {
                lvtab2.Items.Remove(eachItem);
            }
            if (lvtab2.Items.Count == 0)                             
            {
                btntab2_Del.Enabled = false; 
            }
        }

        private void cbbtab2_eo_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> newdata = new List<string>();
            var ColumnEO = _service._dbContext.GetSQLServer<dynamic>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + cbbtab2_eo_table.Text + "' ORDER BY COLUMN_NAME");
            foreach (var columneo in ColumnEO)
            {
                newdata.Add(columneo.COLUMN_NAME);
            }
            cbbtab2_eo_column.DataSource = newdata;
        }


        private void cbbtab2_htlt_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> newdata = new List<string>();
            var Columnhtlt = _dbContextHTLT.GetSQLServer<dynamic>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '"+ cbbtab2_htlt_table.Text + "' ORDER BY COLUMN_NAME");
            foreach (var columnhtlt in Columnhtlt)
            {
                newdata.Add(columnhtlt.COLUMN_NAME);
            }
            cbbtab2_htlt_column.DataSource = newdata;
        }

        private void lvtab2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btntab2_Del.Enabled = true;
        }

        private void btntab2_OK_Click(object sender, EventArgs e)
        {
            string valueString = "";
            int count = lvtab2.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (i != 0) valueString += ",";
                string eo = lvtab2.Items[i].SubItems[0].Text.ToString();
                string htlt = lvtab2.Items[i].SubItems[2].Text.ToString();
                string str = "('" + eo + "','" + htlt + "' )" ;
                valueString += str;
            }

            valueString = "delete from Converted; insert into Converted values" + valueString + ";";
            var commit = _dbContextConvert.GetSQLServer<dynamic>(valueString);

            MessageBox.Show("Hoàn thành!");
        }
        

        //
        //tab3
        //
        private void cbbtab3_eo_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            savetoRam();
            paneltab3.Controls.Clear();
            downtoServer(datalist);
            cbbtab3_eo_table_last = cbbtab3_eo_table.Text;
        }


        void savetoRam()
        {
            bool is_true = true;
            if (is_true)
            {
                for (int j = 0; j < paneltab3.Controls.Count; j++)
                {
                    string key = cbbtab3_eo_table_last + ":" + paneltab3.Controls[j].Controls[0].Text;
                    string value = "";
                    dynamic option = paneltab3.Controls[j].Controls[1];
                    value = option.get_value_selected();
                    if (value != "")
                    {
                        if (ConvertRam.ContainsKey(key))
                        {
                            ConvertRam[key] = value;
                        }
                        else
                        {
                            ConvertRam.Add(key, value);
                        }
                    }
                    //string sqlcommand = "if exists(SELECT * from [ConVert] where [key] = '" + key + "') BEGIN update [ConVert] set [value] = '" + value + "' where [key] = '" + key + "'End else BEGIN insert into [ConVert] values ('" + key + "','" + value + "') End ";
                    //    _dbContextConvertTab3.GetSQLServer<string>(sqlcommand);
                    //}
                }
            }
        }

        bool uptoServer()
        {
            List<Control> list_error = new List<Control>();
            bool is_true = true;
            errorProvider1.Clear();
            if (is_true)
            {
                for (int i = 0; i < paneltab3.Controls.Count; i++)
                {
                    dynamic option = paneltab3.Controls[i].Controls[1];
                    if (paneltab3.Controls[i].BackColor == Color.Pink && paneltab3.Controls[i].Enabled != false && option.get_value_selected() == "")
                    {
                        is_true = false;
                        list_error.Add(paneltab3.Controls[i]);
                        continue;
                    }
                    if (!is_true)
                    {
                        continue;
                    }
                    string key = cbbtab3_eo_table_last + ":" + paneltab3.Controls[i].Controls[0].Text;
                    string value = "";
                    value = option.get_value_selected();
                    
                    if (ConverttableTab3.ContainsKey(key))
                    {
                        if (value != "")
                        {
                            ConverttableTab3[key] = value;
                        }
                        else
                        {
                            ConverttableTab3.Remove(key);
                        }
                    }
                    else
                    {
                        if (value != "")
                        {
                            ConverttableTab3.Add(key, value);
                        }
                    }
                }
            }

            if (is_true)
            {
                int i = 0;
                string sqlcommand = "";
                foreach (var item in ConverttableTab3)
                {
                    if (i != 0) sqlcommand += ",";
                    string str = "('" + item.Key + "','" + item.Value + "' )";
                    sqlcommand += str;
                    i++;
                }
                sqlcommand = "delete from [Convert]; insert into [Convert] values" + sqlcommand + ";";
                var commit = _dbContextConvertTab3.GetSQLServer<dynamic>(sqlcommand);
            }

            else
            {
                for (int i = 0; i < list_error.Count; i++)
                {
                    errorProvider1.SetError(list_error[i].Controls[2], "Nhập cho đủ đi con zai!!!!");
                }
                is_true = false;
            }
            return is_true;
        }

        void downtoServer(dynamic datalist)
        {
            int i = 0;
            var ColumnHTLT = _dbContextHTLT.GetSQLServer<dynamic>("SELECT name, is_nullable, is_identity, system_type_id as type FROM sys.columns WHERE [object_id] = OBJECT_ID('dbo." + cbbtab3_eo_table.Text + "', 'U')");
            foreach (var columnhtlt in ColumnHTLT)
            {
                string value_convert_selected = "";
                string key = cbbtab3_eo_table.Text + ":" + columnhtlt.name.ToString();
                if (ConverttableTab3.ContainsKey(key))
                {
                    value_convert_selected = ConverttableTab3[key];
                }
                var listshow = new ListShow(columnhtlt, new Point(3, 5 + 33 * i), datalist, value_convert_selected); 
                paneltab3.Controls.Add(listshow);
                //string str = "SELECT [value] from [ConVert] where [key] = '" + key + "'";
                //dynamic exists = _dbContextConvertTab3.GetSQLServer<string>(str);
                
                i++;
            }
        }

        private void btntab3_Save_Click(object sender, EventArgs e)
        {
            uptoServer();
        }

        private void btntab3_Convert_Click(object sender, EventArgs e)
        {
            if (uptoServer())
            {
                Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();
                foreach (var item in ConverttableTab3)
                {
                    string key = item.Key;
                    
                    string[] arrkey = key.Split(':');
                    if (data.ContainsKey(arrkey[0]))
                    {
                        data[arrkey[0]].Add(arrkey[1], item.Value);
                    }
                    else
                    {
                        data.Add(arrkey[0], new Dictionary<string, string>{ { arrkey[1], item.Value } });
                    }
                }
                string str = "";
                foreach(var table in data)
                {
                    string column = "";
                    string value = "";
                    int i = 0;
                    foreach(var item in table.Value)
                    {
                        if(item.Value.StartsWith('[') && item.Value.EndsWith(']') && item.Value.IndexOf("][") != -1)
                        {
                            string[] arr = item.Value.Split(new Char[] { '[', ']' });
                            dynamic kq = _service._dbContext.GetSQLServer<dynamic>("select "+ arr[3] + " from "+ arr[1]);
                            foreach(var j in kq)
                            {

                            }
                        }
                        else
                        {
                            value += item.Value;
                            column += item.Key;
                            if(i != table.Value.Count - 1)
                            {
                                column += ", ";
                                value += ", ";
                            }
                            i++;
                        }
                        
                    }

                    str = "insert into " + table.Key + "(" + column + ") values("+ value +")";
                }
                
                //_dbContextConvertTab3.GetSQLServer<string>("");
                //var item = _service._dbContext.GetSQLServer<string>("Select * from ");
            }
        }
        public string check_type_id(dynamic str)
        {
            switch ((int)str)
            {
                case 36:
                    {
                        this.Enabled = false;
                        return "guid";
                        break;
                    }
                case 56:
                    {
                        return "int";
                        break;
                    }
                case 61:
                    {
                        return "datetime";
                        break;
                    }
                case 104:
                    {
                        return "bit";
                        break;
                    }
                case 127:
                    {
                        return "int";
                        break;
                    }
                case 167:
                    {
                        return "string";
                        break;
                    }
                case 231:
                    {
                        return "string";
                        break;
                    }
                default: return "none";
            }
        }

    }
}