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
        private DbContext _dbContextHTLT;
        public string str = "";
        public string Name { get; set; }
        public ShowMore(Dictionary<string, List<string>> datalist)
        {
            Size = new Size(30, 26);
            Location = new Point(445, 0);
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
            ToolStripItem toolStrip = sender as ToolStripItem;
            string type = "";
            string[] toolarr = toolStrip.Text.Split("(");
            string tooltext = toolarr[0].Replace(" ","");
            string tooltype = toolarr[1].Replace(")", "");
            if(this.Parent.GetType() == typeof(ListShow))
            {
                dynamic opt = this.Parent;
                type = opt.origin_type;
            }
            if(tooltype == type)
            {
                str = "[" + toolStrip.OwnerItem.Text + "] : [" + toolStrip.Text + "]";
                this.Parent.Controls[1].Controls.Clear();
                dynamic pa = this.Parent;
                pa.Controls[1].change_control("selected",str);
            }
            else
            {

            }
        }


        public void is_check_datetime(string dateString)
        {
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};
            //string[] dateStrings = {"5/1/2009 6:32 PM", "05/01/2009 6:32:05 PM",
            //            "5/1/2009 6:32:00", "05/01/2009 06:32",
            //            "05/01/2009 06:32:00 PM", "05/01/2009 06:32:00"};
            DateTime dateValue;

            //foreach (string dateString in dateStrings)
            //{
                if (DateTime.TryParseExact(dateString, formats,
                                           new CultureInfo("en-US"),
                                           DateTimeStyles.None,
                                           out dateValue))
                {
                    Console.WriteLine("Converted '{0}' to {1}.", dateString, dateValue);
                }

                else
                {
                    Console.WriteLine("Unable to convert '{0}' to a date.", dateString);
                }
            //}
        }
    }
}
