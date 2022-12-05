using BSS.DataValidator;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using SynceOToHTLT.DataAccess.Common;
using SynceOToHTLT.Services;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace SynceOToHTLT
{
    public partial class Form1 : Form
    {
        private readonly EOService _service;
        private readonly DbContext _dbContextHTLT;
        private readonly DbContext _dbContextConvert;
        private readonly DbContext _dbContextConvertTab3;
        public string cbbtab3_eo_table_last;
        MenuStrip menuStrip;
        Dictionary<string, List<string>> datalist;
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
            Dictionary<string, List<string>> datalist = new Dictionary<string, List<string>>();
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
            }

            foreach (var tablehtlt in Tablehtlt)
            {
                newdatahtlt.Add(tablehtlt);
            }
            cbbtab2_eo_table.DataSource = newdataeo;
            cbbtab3_eo_table.DataSource = newdataeo;
            cbbtab2_htlt_table.DataSource = newdatahtlt;
            cbbtab3_eo_table_last = cbbtab3_eo_table.Text;
            foreach(dynamic item in TableConvertTab2)
            {
                AddNewListViewItemTab2(item.Eoffice, item.htlt); 
            }

            //_dbContextHTLT = new DbContext(Program.AppSettings.ConnectionSetting.HtltConnectionString);
            //menuStrip = new MenuStrip() { Padding = new Padding(0, 0, 0, 0), Dock = DockStyle.Bottom };
            //ToolStripMenuItem listToolStrip1 = new ToolStripMenuItem() { Text = "...", BackColor = Color.LightGray };
            //var Tablehtlt = _dbContextHTLT.GetSQLServer<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME");
            

            foreach (var tablehtlt in Tablehtlt)
            {
                List<string> newlist = new List<string>();
                var columnhtlt = _dbContextHTLT.GetSQLServer<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tablehtlt + "' ORDER BY COLUMN_NAME");
                foreach (var item in columnhtlt)
                {
                   newlist.Add(item.ToString());
                }
                datalist.Add(tablehtlt, newlist);
            }
        }

        private void toolScripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem toolStrip = sender as ToolStripItem;
            var str = "[" + toolStrip.OwnerItem.Text + "] : [" + toolStrip.Text + "]";
            //this.Parent.Controls[1].Text = str;
            this.Parent.Controls[1].Controls.Clear();
            this.Parent.Controls[1].Controls.Add(new TextinListShow(toolStrip.OwnerItem.Text, toolStrip.Text)); ;
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
            uptoServer();
            paneltab3.Controls.Clear();
            int i = 0;
            var ColumnEO = _service._dbContext.GetSQLServer<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + cbbtab3_eo_table.Text + "' ORDER BY COLUMN_NAME");
            var Tablehtlt = _dbContextHTLT.GetSQLServer<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME");
            foreach (var columneo in ColumnEO)
            {
                downtoServer(columneo, i, Tablehtlt);
                i++;
            }
            cbbtab3_eo_table_last = cbbtab3_eo_table.Text;
        }

        //void uptoServer(int something)
        //{
        //    for (int j = 0; j < paneltab3.Controls.Count; j++)
        //    {
        //        string key = cbbtab3_eo_table_last + ":" + paneltab3.Controls[j].Controls[0].Text;
        //        int index = listsave.FindIndex(x => x.key == key);
        //        if (index != -1)
        //        {
        //            listsave[index].value = (paneltab3.Controls[j].Controls[1].Controls.Count != 0) ? paneltab3.Controls[j].Controls[1].Controls[0] : paneltab3.Controls[j].Controls[1].Text;
        //        }
        //        else
        //        {
        //            var newsave = new save()
        //            {
        //                key = key,
        //                value = (paneltab3.Controls[j].Controls[1].Controls.Count != 0) ? paneltab3.Controls[j].Controls[1].Controls[0] : paneltab3.Controls[j].Controls[1].Text
        //            };
        //            listsave.Add(newsave);
        //        }
        //    }
        //}

        void uptoServer()
        {
            for (int j = 0; j < paneltab3.Controls.Count; j++)
            {
                string key = cbbtab3_eo_table_last + ":" + paneltab3.Controls[j].Controls[0].Text;
                string value = "";
                if (paneltab3.Controls[j].Controls[1].Controls.Count != 0 || paneltab3.Controls[j].Controls[1].Text != "")
                {
                    if (paneltab3.Controls[j].Controls[1].Controls.Count != 0)
                    {
                        var selected = paneltab3.Controls[j].Controls[1].Controls[0];
                        value = "[" + selected.Controls[0].Text + "][" + selected.Controls[2].Text + "]";
                    }
                    else
                    {
                        value = paneltab3.Controls[j].Controls[1].Text;
                    }
                    string sqlcommand = "if exists(SELECT * from [ConVert] where [key] = '" + key + "') BEGIN update [ConVert] set [value] = '" + value + "' where [key] = '" + key + "'End else BEGIN insert into [ConVert] values ('" + key + "','" + value + "') End ";
                    _dbContextConvertTab3.GetSQLServer<string>(sqlcommand);
                }
               
            }
        }

        void downtoServer(dynamic columneo, int i, dynamic Tablehtlt)
        {
            var listshow = new ListShow(columneo, new Point(3, 5 + 33 * i), Tablehtlt, datalist); 
            paneltab3.Controls.Add(listshow);

            string key = cbbtab3_eo_table.Text + ":" + columneo.ToString();
            string str = "SELECT [value] from [ConVert] where [key] = '" + key + "'";
            dynamic exists = _dbContextConvertTab3.GetSQLServer<string>(str);
            if (exists.Count != 0)
            {
                string exist = exists[0];
                if (exist.StartsWith('[') && exist.EndsWith(']') && exist.IndexOf("][") != -1)
                {
                    string[] arr = exist.Split(new Char[] { '[', ']' });
                    listshow.Controls[1].Controls.Add(new TextinListShow(arr[1], arr[3]));
                }
                else
                    listshow.Controls[1].Text = exist;
            }
        }

        private void btntab3_OK_Click(object sender, EventArgs e)
        {
            uptoServer();
        }
    }
}