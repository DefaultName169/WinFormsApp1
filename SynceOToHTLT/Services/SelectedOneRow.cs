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
    internal class SelectedOneRow : Panel
    {
        public string type;
        public SelectedOneRow(string table, string column)
        {
            string[] col = column.Split('(');
            col[0] = col[0].Replace(" ","");
            Size = ListSize.ins.size_SelectedOneRow;
            BorderStyle = BorderStyle.FixedSingle;
            Location = ListLocation.ins.Location_SelectedOneRow;
            Button x = new Button()
            {
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(0, 0, 0, 0),
                Text = "x",
                Location = ListLocation.ins.Location_buttonx_in_SelectedOneRow,
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
                    Location = ListLocation.ins.Location_label1_in_SelectedOneRow,
                    Size = ListSize.ins.size_label1_in_SelectedOneRow, 
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = ":",
                    Location = ListLocation.ins.Location_label2_in_SelectedOneRow, 
                    Size = ListSize.ins.size_label2_in_SelectedOneRow,
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                new Label()
                {
                    Padding = new Padding(0, 0, 0, 0),
                    Margin = new Padding(0, 0, 0, 0),
                    Text = col[0],
                    Location = ListLocation.ins.Location_label3_in_SelectedOneRow, 
                    Size = ListSize.ins.size_label1_in_SelectedOneRow, 
                    BackColor = Color.FromArgb(224, 224, 224)
                },
                x
            });
        }
        
        public void xClick(object sender, EventArgs e)
        {
            dynamic control = this.Parent;
            control.is_htlt = false;
            control.back_control();
        }

        public string get_value_selected()
        {
            string column = this.Controls[2].Text.ToString();
            return "[" + this.Controls[0].Text + "][" + this.Controls[2].Text + "]";
        }
    }
}
