using BSS.DataValidator;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using SynceOToHTLT.DataAccess.Common;
using SynceOToHTLT.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace SynceOToHTLT
{
    public partial class Form1 : Form
    {
        public struct convertTab3
        {
            public string key;
            public string value;
            public string present_type;
            public string origin_type;
            public convertTab3(string key, string value, string present_type, string origin_type)
            {
                this.key = key;
                this.value = value;
                this.present_type = present_type;
                this.origin_type = origin_type;
            }
        };

        public struct onerow_right
        {
            public string right_key;
            public dynamic right_value;
            public string origin_type;
            public string present_type;
            public bool is_true;

            public onerow_right(string right_key, string right_value, string origin_type, string present_type, bool is_true)
            {
                this.right_key = right_key;
                this.right_value = right_value;
                this.origin_type = origin_type;
                this.present_type = present_type;
                this.is_true = is_true;
            }
            public onerow_right(string right_key, List<string> right_value, string origin_type, string present_type, bool is_true)
            {
                this.right_key = right_key;
                this.right_value = right_value;
                this.origin_type = origin_type;
                this.present_type = present_type;
                this.is_true = is_true;
            }


        }
        
        public struct all_row_right
        {
            public List<onerow_right> all_row;
            public string key;
            public bool is_true;
            public all_row_right( string key, List<onerow_right> all_row, bool is_true)
            {
                this.all_row = all_row;
                this.key = key;
                this.is_true = is_true;
            }
        }

        public struct onerow_left
        {
            public string primary_key;
            public string left_key;
            public dynamic left_value;
            public string origin_type;
            public string present_type;
            public bool is_true;
            public onerow_left(string primary_key,string left_key, all_row_right left_value, string origin_type, string present_type, bool is_true)
            {
                this.primary_key = primary_key;
                this.left_key = left_key;
                this.left_value = left_value;
                this.origin_type = origin_type;
                this.present_type = present_type;
                this.is_true = is_true;
            }
            public onerow_left(string primary_key, string left_key, string left_value, string origin_key, string present_key, bool is_true)
            {
                this.primary_key = primary_key;
                this.left_key = left_key;
                this.left_value = left_value;
                this.present_type = origin_key;
                this.present_type = present_key;
                this.is_true = is_true;
            }

        }

        public static Form1 ins;
        private readonly EOService _service;
        private readonly DbContext _dbContextHTLT;
        private readonly DbContext _dbContextConvert;
        private readonly DbContext _dbContextConvertTab3;
        public string cbbtab3_eo_table_last;
        MenuStrip menuStrip;
        Dictionary<string, List<string>> menustrip_eo, menustrip_htlt;
        public List<convertTab3> ConverttableTab3, ConvertRam;


        public Form1()
        {
            ListSize size = new ListSize();
            ListLocation ListLocation = new ListLocation();
            ins = this;
            InitializeComponent();
            _service = new EOService();
            _dbContextHTLT = new DbContext(Program.AppSettings.ConnectionSetting.HtltConnectionString);
            _dbContextConvert = new DbContext(Program.AppSettings.ConnectionSetting.ConvertConnectionString);
            _dbContextConvertTab3 = new DbContext(Program.AppSettings.ConnectionSetting.ConvertTab3ConnectionString);
            dynamic exists = _dbContextConvertTab3.GetSQLServer<dynamic>("SELECT * from [ConVert]");
            ConverttableTab3 = new List<convertTab3>();
            ConvertRam = new List<convertTab3>();
            foreach (var item in exists)
            {
                ConverttableTab3.Add(new convertTab3(item.key, item.value, item.present_type, item.origin_type));
                ConvertRam.Add(new convertTab3(item.key, item.value, item.present_type, item.origin_type));
            }
            //ConvertRam = ConverttableTab3;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            menustrip_eo = new Dictionary<string, List<string>>();
            menustrip_htlt = new Dictionary<string, List<string>>();
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

            //menustrip_htlt data
            foreach (var tablehtlt in Tablehtlt)
            {
                newdatahtlt.Add(tablehtlt);
                List<string> newlist = new List<string>();
                dynamic columnhtlt = _dbContextHTLT.GetSQLServer<dynamic>("SELECT name, system_type_id as [type] FROM sys.columns WHERE [object_id] = OBJECT_ID('[dbo].[" + tablehtlt + "]', 'U')");
                foreach (var item in columnhtlt)
                {
                    newlist.Add(item.name.ToString()/*+ "(" + check_type_id(item.type) + ")"*/);
                }
                menustrip_htlt.Add(tablehtlt, newlist);
            }

            //menustrip_eo data
            foreach (var tableeo in TableEO)
            {
                newdataeo.Add(tableeo);
                List<string> newlist = new List<string>();
                dynamic columneo = _service._dbContext.GetSQLServer<dynamic>("SELECT name, system_type_id as [type] FROM sys.columns WHERE [object_id] = OBJECT_ID('[dbo].[" + tableeo + "]', 'U')");
                foreach (var item in columneo)
                {
                    newlist.Add(item.name.ToString().PadRight(30) + "(" + check_type_id(item.type) + ")");
                }
                menustrip_eo.Add(tableeo, newlist);
            }

            cbbtab2_eo_table.DataSource = newdataeo;
            cbbtab2_htlt_table.DataSource = newdatahtlt;

            cbbtab3_eo_table.DataSource = newdatahtlt;
            cbbtab3_eo_table_last = cbbtab3_eo_table.Text;

            foreach (dynamic item in TableConvertTab2)
            {
                AddNewListViewItemTab2(item.Eoffice, item.htlt);
            }
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
            var Columnhtlt = _dbContextHTLT.GetSQLServer<dynamic>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + cbbtab2_htlt_table.Text + "' ORDER BY COLUMN_NAME");
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
                string str = "('" + eo + "','" + htlt + "' )";
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
            panel_left_tab3.Controls.Clear();
            downtoServer();
            cbbtab3_eo_table_last = cbbtab3_eo_table.Text;
        }


        void savetoRam()
        {
            bool is_true = true;
            if (is_true)
            {
                for (int j = 0; j < panel_left_tab3.Controls.Count; j++)
                {
                    string key = cbbtab3_eo_table_last + ":" + panel_left_tab3.Controls[j].Controls[0].Text;
                    string value = "";
                    dynamic option = panel_left_tab3.Controls[j].Controls[1];
                    value = option.get_value_selected();
                    int exists = ConvertRam.FindIndex(x => x.key == key);
                    if (exists != -1)
                    {
                        if (value != "")
                        {
                            ConvertRam[exists] = new convertTab3(key, value, option.get_present_type(), option.get_origin_type());
                        }
                        else
                        {
                            ConvertRam.RemoveAt(exists);
                        }
                    }
                    else
                    {
                        if (value != "")
                        {
                            ConvertRam.Add(new convertTab3(key, value, option.get_present_type(), option.get_origin_type()));
                        }
                    }
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
                for (int i = 0; i < panel_left_tab3.Controls.Count; i++)
                {
                    dynamic option = panel_left_tab3.Controls[i].Controls[1];
                    if (panel_left_tab3.Controls[i].BackColor == Color.Pink && panel_left_tab3.Controls[i].Enabled != false && option.get_value_selected() == "")
                    {
                        is_true = false;
                        list_error.Add(panel_left_tab3.Controls[i]);
                        continue;
                    }
                    if (!is_true)
                    {
                        continue;
                    }
                    else
                    {
                        string key = cbbtab3_eo_table_last + ":" + panel_left_tab3.Controls[i].Controls[0].Text;
                        string value = "";
                        value = option.get_value_selected();

                        int exists = ConverttableTab3.FindIndex(x => x.key == key);
                        if (exists != -1)
                        {
                            if (value != "")
                            {
                                ConverttableTab3[exists] = new convertTab3(key, value, option.get_present_type(), option.get_origin_type());
                            }
                            else
                            {
                                ConverttableTab3.RemoveAt(exists);
                            }
                        }
                        else
                        {
                            if (value != "")
                            {
                                ConverttableTab3.Add(new convertTab3(key, value, option.get_present_type(), option.get_origin_type()));
                            }
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
                    string str = "('" + item.key + "','" + item.value + "','" + item.present_type + "','" + item.origin_type + "')";
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

        void downtoServer()
        {
            int i = 0;
            var ColumnHTLT = _dbContextHTLT.GetSQLServer<dynamic>("SELECT name, is_nullable, is_identity, system_type_id as type FROM sys.columns WHERE [object_id] = OBJECT_ID('dbo." + cbbtab3_eo_table.Text + "', 'U')");
            foreach (var columnhtlt in ColumnHTLT)
            {
                string value_convert_selected = "";
                string key = cbbtab3_eo_table.Text + ":" + columnhtlt.name.ToString();
                dynamic exists = ConvertRam.Find(x => x.key == key);
                if (exists.key != null)
                {
                    value_convert_selected = exists.value;
                }
                var onerow = new OneRow(columnhtlt, new Point(3, 5 + 33 * i), menustrip_eo, menustrip_htlt, value_convert_selected);
                panel_left_tab3.Controls.Add(onerow);
                i++;
            }
        }

        private void btntab3_Save_Click(object sender, EventArgs e)
        {
            save_panel_left();
        }

        private void btntab3_Convert_Click(object sender, EventArgs e)
        {
            Converttable();
            //if (uptoServer())
            //{
            //    ///table    column      value
            //    Dictionary<string, Dictionary<string[], List<string>>> data = new Dictionary<string, Dictionary<string[], List<string>>>();
            //    bool is_exits_selected = false;
            //    int min_selected_count = 10;
            //    string sql_command = "";
            //    foreach (var item in ConverttableTab3) /// list<converTab3>{key(table:column),value,type}
            //    {
            //        string key = item.key;
            //        string[] arrkey = key.Split(':');
            //        string data_table = arrkey[0];
            //        string data_column = arrkey[1];
            //        string origin_type = item.origin_type;
            //        if (data.ContainsKey(data_table))
            //        {
            //            if (item.present_type == "selected" && item.value.StartsWith('[') && item.value.EndsWith(']') && item.value.IndexOf("][") != -1)
            //            {
            //                string[] arr = item.value.Split(new Char[] { '[', ']' });
            //                dynamic data_value = _service._dbContext.GetSQLServer<string>("select top(" + min_selected_count + ") " + arr[3] + " from " + arr[1]);
            //                is_exits_selected = true;

            //                data[data_table].Add(new string[] { data_column, origin_type }, (List<string>)data_value);

            //                if (data_value.Count < min_selected_count)
            //                {
            //                    min_selected_count = data_value.Count;
            //                }
            //            }
            //            else
            //            {
            //                data[data_table].Add(new string[] { data_column, origin_type }, new List<string> { item.value });
            //            }
            //        }
            //        else
            //        {
            //            min_selected_count = 10;
            //            if (item.present_type == "selected" && item.value.StartsWith('[') && item.value.EndsWith(']') && item.value.IndexOf("][") != -1)
            //            {
            //                string[] arr = item.value.Split(new Char[] { '[', ']' });
            //                dynamic data_value = _service._dbContext.GetSQLServer<string>("select top(" + min_selected_count + ") " + arr[3] + " from " + arr[1]);
            //                is_exits_selected = true;

            //                data.Add(data_table, new Dictionary<string[], List<string>> { { new string[] { data_column, origin_type }, (List<string>)data_value } });
            //                if (data_value.Count < min_selected_count)
            //                {
            //                    min_selected_count = data_value.Count;
            //                }
            //            }
            //            else
            //            {
            //                data.Add(data_table, new Dictionary<string[], List<string>> { { new string[] { data_column, origin_type }, new List<string> { { item.value } } } });
            //            }
            //        }
            //    }

            //    if (is_exits_selected == false)
            //    {
            //        min_selected_count = 1;
            //    }


            //    /// Dictionart< table , Dictionary< [column, orign_type] , List<value>  >>

            //    foreach (var item in data)
            //    {
            //        int j = 0;
            //        string[] values = new string[min_selected_count];
            //        string table = "";
            //        string column = "";
            //        string value = "";
            //        string origin = "";
            //        table = item.Key;
            //        foreach (var col in item.Value)
            //        {
            //            column += "[" + col.Key[0] + "]";
            //            if (j == 0) { column = "(" + column; }
            //            if (j != item.Value.Count - 1) { column = column + ","; }
            //            if (j == item.Value.Count - 1) { column = column + ")"; }


            //            for (int i = 0; i < min_selected_count; i++)
            //            {
            //                string a_value = "";
            //                if (col.Value.Count == 1)
            //                {
            //                    a_value = format_string(col.Value[0], col.Key[1]);
            //                }
            //                else
            //                {
            //                    a_value = format_string(col.Value[i], col.Key[1]);
            //                }

            //                if (j == 0) { a_value = "(" + a_value; }
            //                if (j != item.Value.Count - 1) { a_value = a_value + ","; }
            //                if (j == item.Value.Count - 1) { a_value = a_value + ")"; }

            //                values[i] += a_value;
            //            }
            //            j++;
            //        }


            //        int k = 0;
            //        foreach (var val in values)
            //        {
            //            if (k != values.Count() - 1)
            //            {
            //                value += val + ",";
            //            }
            //            else value += val;
            //            k++;
            //        }

            //        string str = "insert into [" + table + "] " + column + " values " + value + ";";

            //        sql_command += str;
            //    }
            //    _dbContextHTLT.GetSQLServer<string>(sql_command);
            //}
        }

     

        List<onerow_left> all_row_left = new List<onerow_left>();
        List<all_row_right> all_rows_right = new List<all_row_right>();


        public void save_panel_right()
        {
            bool is_true = true;
            if(rowchoose.Text != "")
            {

                //if(exist_all_row_right)

                List<onerow_right> convert_right_Ram = new List<onerow_right>();
                foreach (dynamic onerow in panel_right_tab3.Controls)
                {
                    var option_onerow = onerow.Controls[1];
                    option_onerow.get_value_selected();
                    string column_onerow = onerow.Controls[0].Text;
                    //string column_option_onerow = option_onerow.Controls[0].Controls[2].Text;
                    string key = column_onerow;
                    string value = "";
                    value = option_onerow.get_value_selected();

                    int exists = convert_right_Ram.FindIndex(x => x.right_key == key);
                    if (exists != -1)
                    {
                        if (value != "")
                        {
                            convert_right_Ram[exists] = new onerow_right(key, value, option_onerow.get_origin_type(), option_onerow.get_present_type(), true);
                        }
                        else
                        {
                            convert_right_Ram.RemoveAt(exists);
                        }
                    }
                    else
                    {
                        if (value != "")
                        {
                            convert_right_Ram.Add(new onerow_right(key, value, option_onerow.get_origin_type(), option_onerow.get_present_type(), true));
                        }
                        else 
                        if(onerow.BackColor == Color.Pink && onerow.Enabled != false)
                        {
                            convert_right_Ram.Add(new onerow_right(key, value, option_onerow.get_origin_type(), option_onerow.get_present_type(), false));
                            is_true = false;
                        }
                    }
                }
                int exist_all_rows_right = all_rows_right.FindIndex(x => x.key == rowchoose.Text);
                if(exist_all_rows_right != -1)
                {
                    all_rows_right[exist_all_rows_right] = new all_row_right(rowchoose.Text, convert_right_Ram, is_true);
                }
                else
                {
                    all_rows_right.Add(new all_row_right(rowchoose.Text, convert_right_Ram, is_true));
                }
            }
        }



        public void save_panel_left()
        {
            save_panel_right();
            List<Control> list_error = new List<Control>();
            foreach(dynamic onerow in panel_left_tab3.Controls)
            {
                var option_onerow = onerow.Controls[1];
                string column_onerow = onerow.Controls[0].Text;

                
                string key = cbbtab3_eo_table_last + ":" + column_onerow;  ///filedocument:fileid  
                dynamic value;
                int exist_all_row_left = all_row_left.FindIndex(x => x.left_key == key);

                if (option_onerow.present_type == "selected")
                {
                    string table_option_onerow = option_onerow.Controls[0].Controls[0].Text;
                    string column_option_onerow = option_onerow.Controls[0].Controls[2].Text;
                    string primarykey = column_option_onerow;
                    int exist_all_row_right = all_rows_right.FindIndex(x => x.key == table_option_onerow);
                    string key_left = key + ":" + column_option_onerow;

                    if (exist_all_row_left != -1)
                    {
                        if (exist_all_row_right != -1)
                        {
                            all_row_left[exist_all_row_left] = new onerow_left(primarykey, key, all_rows_right[exist_all_row_right], option_onerow.get_origin_type(), option_onerow.get_present_type(), all_rows_right[exist_all_row_right].is_true);
                            if(all_row_left[exist_all_row_right].is_true == false)
                            {
                                list_error.Add((Control)onerow);
                            }
                        }
                    }
                    else
                    {
                        if (exist_all_row_right != -1)
                        {
                            all_row_left.Add(new onerow_left(primarykey, key, all_rows_right[exist_all_row_right], option_onerow.get_origin_type(), option_onerow.get_present_type(), all_rows_right[exist_all_row_right].is_true));
                            if (all_row_left[exist_all_row_right].is_true == false)
                            {
                                list_error.Add((Control)onerow);
                            }
                        }
                    }
                }

                /// option_onerow khong phai kieu selected
                else
                {
                    if(exist_all_row_left != -1)
                    {
                        all_row_left[exist_all_row_left] = new onerow_left(null, key, option_onerow.get_value_selected(), option_onerow.get_origin_type(), option_onerow.get_present_type(), true);
                    }
                }
            }

            foreach(var row_left in list_error)
            {
                errorProvider1.SetError(row_left.Controls[3], "Nhập cho đủ đi con zai!!!!");
            }

        }

        //List<onerow_left> all_row_left = new List<onerow_left>();
        //List<all_row_right> all_rows_right = new List<all_row_right>();
        public void Converttable()
        {
            save_panel_left();
            string sql_command = "";
            foreach (var item in all_row_left)
            {
                int min_selected_count = 10;
                if (item.left_value.GetType() == typeof(all_row_right))
                {
                    bool is_exits_selected = false;
                    

                    string key = item.left_value.key;

                    for (int i = 0; i < item.left_value.all_row.Count; i++)
                    {
                        string origin_type = item.origin_type;

                        //if (data.ContainsKey(data_table))
                        //{
                        if (item.left_value.all_row[i].present_type == "selected" && item.left_value.all_row[i].right_value.StartsWith('[') && item.left_value.all_row[i].right_value.EndsWith(']') && item.left_value.all_row[i].right_value.IndexOf("][") != -1)
                        {
                            string[] arr = item.left_value.all_row[i].right_value.Split(new Char[] { '[', ']' });
                            dynamic data_value = _service._dbContext.GetSQLServer<string>("select top(" + min_selected_count + ") " + arr[3] + " from " + arr[1]);
                            //is_exits_selected = true;
                            if (data_value.Count != 0)
                            {
                                item.left_value.all_row[i] = new onerow_right(item.left_value.all_row[i].right_key, (List<string>)data_value, item.left_value.all_row[i].origin_type, item.left_value.all_row[i].present_type, item.left_value.all_row[i].is_true);
                                if (data_value.Count < min_selected_count)
                                {
                                    min_selected_count = data_value.Count;
                                }
                            }
                        }
                    }
                }

                /// left_key = "FileDocument:FileID"
                /// primary_key = "File:FileID" 
                /// 

                string[] array = item.left_key.Split(':');
                string table_left = array[0];
                string column_left = array[1];
                sql_command += "Declare @" + item.primary_key + " table (id int); ";
                foreach(all_row_right value in item.left_value)
                {
                    string sql = "";
                    foreach(onerow_right onerow in value.all_row)
                    {


                        if(onerow.right_value.GetType() == typeof(List<string>))
                        {
                            foreach(string key in onerow.right_value)
                            {

                            }

                            string sql1 = "insert into " + value.key + "()  ;";
                        }

                    }


                }

            }




            //foreach (onerow_right value in item.left_value.all_row)
            //{

            //}
            //else
            //{
            //    min_selected_count = 10;
            //    if (item.present_type == "selected" && item.value.StartsWith('[') && item.value.EndsWith(']') && item.value.IndexOf("][") != -1)
            //    {
            //        string[] arr = item.value.Split(new Char[] { '[', ']' });
            //        dynamic data_value = _service._dbContext.GetSQLServer<string>("select top(" + min_selected_count + ") " + arr[3] + " from " + arr[1]);
            //        is_exits_selected = true;

            //        data.Add(data_table, new Dictionary<string[], List<string>> { { new string[] { data_column, origin_type }, (List<string>)data_value } });
            //        if (data_value.Count < min_selected_count)
            //        {
            //            min_selected_count = data_value.Count;
            //        }
            //    }
            //    else
            //    {
            //        data.Add(data_table, new Dictionary<string[], List<string>> { { new string[] { data_column, origin_type }, new List<string> { { item.value } } } });
            //    }
            //}
            //}

            //if (is_exits_selected == false)
            //{
            //    min_selected_count = 1;
            //}


            /// Dictionart< table , Dictionary< [column, orign_type] , List<value>  >>

            //    foreach (var item in data)
            //    {
            //        int j = 0;
            //        string[] values = new string[min_selected_count];
            //        string table = "";
            //        string column = "";
            //        string value = "";
            //        string origin = "";
            //        table = item.Key;
            //        foreach (var col in item.Value)
            //        {
            //            column += "[" + col.Key[0] + "]";
            //            if (j == 0) { column = "(" + column; }
            //            if (j != item.Value.Count - 1) { column = column + ","; }
            //            if (j == item.Value.Count - 1) { column = column + ")"; }


            //            for (int i = 0; i < min_selected_count; i++)
            //            {
            //                string a_value = "";
            //                if (col.Value.Count == 1)
            //                {
            //                    a_value = format_string(col.Value[0], col.Key[1]);
            //                }
            //                else
            //                {
            //                    a_value = format_string(col.Value[i], col.Key[1]);
            //                }

            //                if (j == 0) { a_value = "(" + a_value; }
            //                if (j != item.Value.Count - 1) { a_value = a_value + ","; }
            //                if (j == item.Value.Count - 1) { a_value = a_value + ")"; }

            //                values[i] += a_value;
            //            }
            //            j++;
            //        }


            //        int k = 0;
            //        foreach (var val in values)
            //        {
            //            if (k != values.Count() - 1)
            //            {
            //                value += val + ",";
            //            }
            //            else value += val;
            //            k++;
            //        }

            //        string str = "insert into [" + table + "] " + column + " values " + value + ";";

            //        sql_command += str;
            //    }

            //    //}
            //}
            //else
            //if (item.left_value.GetType = typeof(string))
            //{


            //}
        }


        public void changeTextrowchoose(string newtext)
        {
            rowchoose.Text = newtext;
        }


        
        public void onerow_Click(object sender, EventArgs e)
        {
            OneRow this_list = sender as OneRow;
            dynamic option_onerow = this_list.Controls[1];
            dynamic showmore1 = this_list.Controls[2];
            dynamic showmore2 = this_list.Controls[3];

            string table_choose = option_onerow.Controls[0].Controls[0].Text;
            if (table_choose != null)
            {
                showmore_htlt_Click(option_onerow.is_htlt, table_choose);
            }

        }


        public void showmore_htlt_Click(bool is_htlt, string choose)
        {
            rowchoose.Text = choose;
            if (is_htlt) 
            {
                panel_right_tab3.Controls.Clear();
                int i = 0;
                var ColumnHTLT = _dbContextHTLT.GetSQLServer<dynamic>("SELECT name, is_nullable, is_identity, system_type_id as type FROM sys.columns WHERE [object_id] = OBJECT_ID('dbo." + choose + "', 'U')");
                foreach (var columnhtlt in ColumnHTLT)
                {
                    string value_convert_selected = "";
                    string key = cbbtab3_eo_table.Text + ":" + columnhtlt.name.ToString();
                    dynamic exists = ConvertRam.Find(x => x.key == key);
                    if (exists.key != null)
                    {
                        value_convert_selected = exists.value;
                    }
                    var onerow = new OneRow(columnhtlt, new Point(3, 5 + 33 * i), menustrip_eo, menustrip_htlt, value_convert_selected);
                    panel_right_tab3.Controls.Add(onerow);
                    i++;
                }
            }
            else
            {
                panel_right_tab3.Controls.Clear();
            }
        }


        public string format_string(string value,string type)
        {
            switch (type)
            {
                case "string":
                    {
                        return "'" + value + "'";
                    }
                case "datetime":
                    {
                        return "'" + value + "'";
                    }
                default: return value;
            }
        }

        public string check_type_id(dynamic str)
        {
            switch ((int)str)
            {
                case 34:
                    {
                        return "image";
                        break;
                    }
                case 35:
                    {
                        return "string";
                        break;
                    }
                case 36:
                    {
                        this.Enabled = false;
                        return "guid";
                        break;
                    }
                case 48:
                    {
                        return "int";
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
                case 62:
                    {
                        return "float";
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
                case 175:
                    {
                        return "char";
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