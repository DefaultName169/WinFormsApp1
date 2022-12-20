using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynceOToHTLT.Services
{
    internal class Option_ListShow: Panel
    {
        public string origin_type;
        public string present_type;
        public string value_convert_selected;

        public Option_ListShow(string origin_type, string present_type, Control parent, string value_convert_selected)
        {
            this.origin_type = origin_type;
            this.present_type = present_type;
            this.value_convert_selected = value_convert_selected;
            Width = 235;
            Height = 26;
            Margin = new Padding(0, 0, 0, 0);
            Location = new Point(200, 1);
            Parent = parent;
            dynamic pa = parent;
            create_control(present_type, value_convert_selected);
        }
       

        public void create_control(string type, string value_convert_selected)
        {
            switch (type)
            {
                case "bit":
                    {
                        Controls.Add(new CheckBox()
                        {
                            Location = new Point(2, 1),
                            Checked = false,
                        }
                        );
                        break;
                    }
                case "datetime":
                    {
                        Controls.Add(new DateTimePicker()
                        {
                            Width = 230,
                            Height = 26,
                            Location = new Point(2, 1)
                        }
                        );
                        break;
                    }
                case "int":
                    {
                        Controls.Add(new NumericUpDown()
                        {
                            Width = 230,
                            Height = 26,
                            Location = new Point(2, 1)
                        }
                        );
                        break;
                    }
                case "guid":
                    {
                        Controls.Add(new TextBox()
                        {
                            Width = 230,
                            Height = 26,
                            Text = "Dạng Guid được nhập tự động"
                        }
                        );
                        break;
                    }
                case "selected":
                    {
                        string[] arr = value_convert_selected.Split(new Char[] { '[', ']' });
                        Controls.Add(new SelectedListShow(arr[1], arr[3]));
                        break;
                    }
                default:
                    {
                        TextBox thistext = new TextBox()
                        {
                            Width = 230,
                            Height = 26,
                            Location = new Point(2, 1)
                        };
                        Controls.Add(thistext);
                        thistext.Leave += thistext_Leave;
                        thistext.Enter += thistext_Enter;
                        break;
                    }
            };
        }

        public string get_value_selected()
        {
            string value = "";
            dynamic control = this.Controls[0];
            switch (present_type)
            {
                case "bit":
                    {
                        if (control.Checked == true)
                        {
                            value = "1";
                        }
                        else value = "0";
                        break;
                    }
                case "datetime":
                    {
                        value = control.Value.ToString();
                        break;
                    }
                case "int":
                    {
                        value = control.Value.ToString();
                        break;
                    }
                case "guid":
                    {
                        break;
                    }
                case "selected":
                    {
                        value = control.get_value_selected();
                        break;
                    }
                default:
                    {
                        if (this.ForeColor == Color.Black && this.Text != "")
                        {
                            value = control.Text;
                        }
                        break;
                    }
            }
            return value;
        }

        public void change_control(string type,string selected)
        {
            this.present_type = type;
            if (this.Controls.Count != 0)
            {
                this.Controls.Remove(Controls[0]);
            }
            create_control(type, selected);
        }

        public void back_control()
        {
            this.present_type = origin_type;
            this.Controls.Remove(Controls[0]);
            create_control(origin_type, value_convert_selected);
        }

        public void thistext_Leave(object sender, EventArgs e)
        {
            TextBox newtext = sender as TextBox;
            if (newtext.Text == "")
            {
                newtext.Text = this.present_type;
                newtext.ForeColor = Color.Silver;
            }
        }
        public void thistext_Enter(object sender, EventArgs e)
        {
            TextBox newtext = sender as TextBox;
            if (newtext.Text == "string" || (newtext.Text == "int"))
            {
                newtext.Text = "";
                newtext.ForeColor = Color.Black;
            }
        }


        public Control selected
        {
            get { return selected; }
            set {
                Location = new Point(2, 1);
            }
        }
        public Control bit { get; set; }
        public Control guid { get; set; }
        public Control datetime { get; set; }
        public Control text { get; set; }
        
    }
}
