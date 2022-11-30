using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Tls.Crypto;
using SynceOToHTLT.DataAccess.Common;
using SynceOToHTLT.Models.HTLT.File;
using SynceOToHTLT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SynceOToHTLT.Services
{
    public class ShowMore : Panel
    {
        private DbContext _dbContextHTLT;
        public string str = "";
        public string Name { get; set; }
        public ShowMore(string name, dynamic Tablehtlt) 
        {
            Name = name;
            Size = new Size(30, 26);
            Location = new Point(468, 2);
            BorderStyle = BorderStyle.FixedSingle;
            _dbContextHTLT = new DbContext(Program.AppSettings.ConnectionSetting.HtltConnectionString);
            MenuStrip menuStrip = new MenuStrip() { Padding = new Padding(0, 0, 0, 0), Dock = DockStyle.Bottom }; 
            ToolStripMenuItem listToolStrip1 = new ToolStripMenuItem() { Text = "...", BackColor = Color.LightGray };
            //var Tablehtlt = _dbContextHTLT.GetSQLServer<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME");

            foreach (var tablehtlt in Tablehtlt)
            {
                ToolStripMenuItem x = new ToolStripMenuItem();
                var columnhtlt = _dbContextHTLT.GetSQLServer<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tablehtlt + "' ORDER BY COLUMN_NAME");
                foreach (var item in columnhtlt)
                {
                    ToolStripMenuItem y = new ToolStripMenuItem()
                    {
                        Text = item.ToString(),
                    };
                    y.Click += toolScripMenuItem_Click;
                    x.DropDownItems.Add(y);
                }
                x.Text = tablehtlt;
                listToolStrip1.DropDownItems.Add(x);
            }
            menuStrip.Name = name;
            menuStrip.Items.Add(listToolStrip1);
            this.Controls.Add(menuStrip);
        }

        private void toolScripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem toolStrip = sender as ToolStripItem;
            str = "[" + toolStrip.OwnerItem.Text + "] : [" + toolStrip.Text+ "]";
            //this.Parent.Controls[1].Text = str;
            this.Parent.Controls[1].Controls.Clear();
            this.Parent.Controls[1].Controls.Add(new TextinListShow(toolStrip.OwnerItem.Text, toolStrip.Text)); ;
        }
    }
}
