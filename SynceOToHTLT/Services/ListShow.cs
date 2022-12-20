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
    internal class ListShow : Panel
    {
        public string origin_type;
        public string present_type;
        public ListShow(dynamic columnhtlt, Point location, Dictionary<string, List<string>> datashowmore, string value_convert_selected) 
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
            Width = 500;
            Height = 30;
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
                        Location = new Point(7, 7),
                        Width = 195
                    },
                    new Option_ListShow(origin_type ,present_type,this, value_convert_selected),
                    new ShowMore(datashowmore)
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
