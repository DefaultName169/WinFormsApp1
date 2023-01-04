using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynceOToHTLT.Services
{
    internal class Option_OneRow: Panel
    {
        public string origin_type;
        public string present_type;
        public string value_convert_selected;
        public bool is_htlt = false;
        public Option_OneRow(string origin_type, string present_type, string value_convert_selected)
        {
            this.origin_type = origin_type;
            this.present_type = present_type;
            this.value_convert_selected = value_convert_selected;
            Size = ListSize.ins.size_Option_OneRow;
            Margin = new Padding(0, 0, 0, 0);
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
                            Location = ListLocation.ins.Location_opiton_in_Option_OneRow,
                            Checked = (value_convert_selected == "" || value_convert_selected == "0")? false : true,
                            Parent = this,
                        }
                        );;
                        break;
                    }
                case "datetime":
                    {
                        Controls.Add(new DateTimePicker()
                        {
                            Size = ListSize.ins.size_option_in_Option_Listshow,
                            Location = ListLocation.ins.Location_opiton_in_Option_OneRow,
                            Parent = this,
                            Value = (value_convert_selected == "") ? DateTime.Now : DateTime.ParseExact(value_convert_selected, "yyyy-MM-dd hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture)
                        }
                        );;
                        break;
                    }
                case "int":
                    {
                        Controls.Add(new NumericUpDown()
                        {
                            Size = ListSize.ins.size_option_in_Option_Listshow,
                            Location = ListLocation.ins.Location_opiton_in_Option_OneRow,
                            Maximum = 50000,
                            Parent = this,
                            Value = (value_convert_selected == "") ? 0 : Int32.Parse(value_convert_selected),
                        }
                        );
                        break;
                    }
                case "guid":
                    {
                        Controls.Add(new TextBox()
                        {
                            Size = ListSize.ins.size_option_in_Option_Listshow,
                            Location = ListLocation.ins.Location_opiton_in_Option_OneRow,
                            Parent = this,
                            Text = "Dạng Guid được nhập tự động"
                        }
                        );
                        break;
                    }
                case "selected":
                    {
                        string[] arr = value_convert_selected.Split(new Char[] { '[', ']' });
                        Controls.Add(new SelectedOneRow(arr[1], arr[3]) { Parent = this });
                        break;
                    }
                default:
                    {
                        TextBox thistext = new TextBox()
                        {
                            Size = ListSize.ins.size_option_in_Option_Listshow,
                            Location = ListLocation.ins.Location_opiton_in_Option_OneRow,
                            Text = value_convert_selected.Replace("'",""),
                            
                        };
                        Controls.Add(thistext);
                        thistext.Leave += thistext_Leave;
                        thistext.Enter += thistext_Enter;
                        break;
                    }
            };
        }

        public string get_present_type()
        {
            return present_type;
        }

        public string get_origin_type()
        {
            return origin_type;
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
                        DateTime date = control.Value;
                        string dateTime = date.ToString("yyyy-MM-dd hh:mm:ss tt");
                        value = dateTime.Contains("SA") ? dateTime.Replace("SA", "AM") : dateTime.Replace("CH", "PM"); 
                        break;
                    }
                case "int":
                    {
                        if (this.Parent.BackColor == Color.FromArgb(198, 255, 179))
                        {
                            value = "";
                        }
                        else
                        {
                            value = control.Value.ToString();
                        }
                        
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
                        if (control.ForeColor.ToArgb() == Color.Black.ToArgb() && control.Text != "")
                        {
                            value = control.Text ;
                        }
                        break;
                    }
            }
            return value;
        }

        public void change_control(string type, string selected)
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
