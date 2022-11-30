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
        public ListShow(string labelName, Point location, dynamic Tablehtlt) 
        {
            Location = location;
            Width = 500;
            Height = 30;
            BorderStyle = BorderStyle.Fixed3D;
            this.Controls.AddRange(new Control[]{
                new Label()
                {
                    Text = labelName,
                    Location = new Point(7, 7),
                    Width = 200
                },
                new TextBox()
                {
                    Name = "textbox",
                    Location = new Point(265, 3),
                    Width = 200,
                    Height = 23,
                },
                new ShowMore(labelName, Tablehtlt)
                {
                    Parent = this
                }
            }) ;
        }
    }
}
