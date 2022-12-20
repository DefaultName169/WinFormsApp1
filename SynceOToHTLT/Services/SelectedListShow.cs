using Google.Protobuf.WellKnownTypes;
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
    internal class SelectedListShow : Panel
    {
        public string type;
        public SelectedListShow(string table, string column)
        {
            string[] col = column.Split('(');
            col[0] = col[0].Replace(" ","");
            Size = new Size(300,20);
            BorderStyle = BorderStyle.FixedSingle;
            Button x = new Button()
            {
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(0, 0, 0, 0),
                Text = "x",
                Location = new Point(211, 0),
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
                    Size = new Size(97, 15),
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = ":",
                    Location = new Point(102, 2),
                    Size = new Size(8, 15),
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = col[0],
                    Location = new Point(112, 2),
                    Size = new Size(97, 15),
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                x
            });
        }
        
        public void xClick(object sender, EventArgs e)
        {
            dynamic control = this.Parent;
            control.back_control();
        }

        public void set_value_selected()
        {

        }

        public string get_value_selected()
        {
            string column = this.Controls[2].Text.ToString();
            return "[" + this.Controls[0].Text + "][" + this.Controls[2].Text + "]";
        }
    }
}
