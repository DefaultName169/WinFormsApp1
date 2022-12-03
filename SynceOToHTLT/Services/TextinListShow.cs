using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace SynceOToHTLT.Services
{
    internal class TextinListShow : Panel
    {
        public TextinListShow(string table, string column)
        {
            Size = new Size(300,20);
            BorderStyle = BorderStyle.FixedSingle;
            Button x = new Button()
            {
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(0, 0, 0, 0),
                Text = "x",
                Location = new Point(180, 0),
                Size = new Size(20, 20),
                BackColor = Color.FromArgb(224, 224, 224)
            };
            x.Click += xClick;
            this.Controls.AddRange(new Control[]
            {
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = table,
                    Location = new Point(3, 2),
                    Size = new Size(75, 15),
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = ":",
                    Location = new Point(85, 2),
                    Size = new Size(8, 15),
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = column,
                    Location = new Point(95, 2),
                    Size = new Size(75, 15),
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                x
            });
        }

        public string setTableAndColumn(dynamic paneltab3)
        {
            string str = "[" + this.Controls[1] + "][" + this.Controls[2] + "]"; 
            return str;
        }

        public void xClick(object sender, EventArgs e)
        {
            this.Parent.Controls.Clear();
        }
    }
}
