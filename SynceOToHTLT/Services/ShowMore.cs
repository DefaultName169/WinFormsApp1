using Google.Protobuf;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Tls.Crypto;
using SynceOToHTLT.DataAccess.Common;
using SynceOToHTLT.Models.HTLT.File;
using SynceOToHTLT.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SynceOToHTLT.Services
{
    public class ShowMore : Panel
    {
        public bool is_htlt = false;
        private DbContext _dbContextHTLT;
        public string str = "";
        public string Name { get; set; }
        public string table_selected;
        public string column_selected;

        public ShowMore(Dictionary<string, List<string>> datalist)
        {
            Size = ListSize.ins.size_ShowMore;
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(0, 0, 0, 0);
            MenuStrip menuStrip = new MenuStrip() { Padding = new Padding(0, 0, 0, 0), Dock = DockStyle.Bottom };
            ToolStripMenuItem listToolStrip1 = new ToolStripMenuItem() { Text = "...", BackColor = Color.LightGray };

            foreach (var tablehtlt in datalist)
            {
                ToolStripMenuItem x = new ToolStripMenuItem();
                foreach (var item in tablehtlt.Value)
                {
                    ToolStripMenuItem y = new ToolStripMenuItem()
                    {
                        Text = item.ToString(),
                    };
                    y.Click += toolScripMenuItem_Click;
                    x.DropDownItems.Add(y);
                }
                x.Text = tablehtlt.Key;
                listToolStrip1.DropDownItems.Add(x);
            }
            menuStrip.Items.Add(listToolStrip1);
            this.Controls.Add(menuStrip);
        }

        private void toolScripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.ins.save_panel_right();
            ToolStripItem toolStrip = sender as ToolStripItem;
            string type = "";
            string[] toolarr = toolStrip.Text.Split("(");
            string tooltext = toolarr[0].Replace(" ","");
            string tooltype = "";
            table_selected = toolStrip.OwnerItem.Text;
            column_selected = toolStrip.Text;
            dynamic onerow = this.Parent;
            dynamic option_onerow = this.Parent.Controls[1];

            if (!is_htlt)
            {
                 tooltype = toolarr[1].Replace(")", "");
            }

            if (this.Parent.GetType() == typeof(OneRow))
            {
                type = onerow.origin_type;            
                if (is_htlt)
                {
                    this.Parent.Click += Form1.ins.onerow_Click;
                    option_onerow.is_htlt = true;
                    Form1.ins.showmore_htlt_Click(is_htlt, table_selected);
                    str = "[" + table_selected + "] : [" + column_selected + "]";
                    option_onerow.Controls.Clear();
                    option_onerow.change_control("selected", str);
                }
            }


            if(tooltype == type)
            {
                str = "[" + table_selected + "] : [" + column_selected + "]";
                this.Parent.Controls[1].Controls.Clear();
                dynamic pa = this.Parent;
                pa.Controls[1].change_control("selected",str);
            }

        }

        public void onerow_Click(object sender, EventArgs e)
        {
            OneRow this_list = sender as OneRow;
            //Form1.ins.showmore_htlt_Click(is_htlt, choose);
        }

    }
}
