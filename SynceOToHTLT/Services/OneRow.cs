using MySqlX.XDevAPI.Relational;
using SynceOToHTLT.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SynceOToHTLT.Services
{
    internal class OneRow : Panel
    {
        public string origin_type;
        public string present_type;
        public OneRow(dynamic columnhtlt, Point location, Dictionary<string, List<string>> menustrip_eo, Dictionary<string, List<string>> menustrip_htlt, string value_convert_selected) 
        {
            origin_type = check_type_id(columnhtlt.type);
            if (value_convert_selected.StartsWith('[') && value_convert_selected.EndsWith(']') && value_convert_selected.IndexOf("][") != -1)
            {
                present_type = "selected";
            }
            else 
            { 
                present_type = origin_type; 
            }

            Location = location;
            Size = ListSize.ins.size_OneRow;
            Padding = new Padding(0, 0, 0, 0);
            if (!Convert.ToBoolean(columnhtlt.is_nullable) || check_type_id(columnhtlt.type) == "guid" )
            {
                BackColor = Color.Pink;
            }

            if (columnhtlt.is_identity)
            {
                BackColor = Color.FromArgb(198, 255, 179);
                this.Enabled = false;
            }

            BorderStyle = BorderStyle.Fixed3D;

            this.Controls.AddRange(new Control[]
                {
                    new Label()
                    {
                        Text = columnhtlt.name,
                        Location = new Point(7, 9),
                        Width = 120
                    },
                    new Option_OneRow(origin_type, present_type, value_convert_selected){ Location = ListLocation.ins.Location_Option_OneRow, Parent = this},
                    new ShowMore(menustrip_eo) {Location = ListLocation.ins.Location_ShowMore1, is_htlt = false, Parent = this},
                    new ShowMore(menustrip_htlt) {Location = new Point(555, 5), is_htlt = true, Parent = this}
                }
            );
        }



        public string check_type_id(dynamic str)
        {
            switch ((int)str)
            {
                case 36:
                {
                    this.Enabled = false;
                    return "guid";
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
