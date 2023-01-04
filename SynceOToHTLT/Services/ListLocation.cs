using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynceOToHTLT.Services
{
    internal class ListLocation
    {
        public Point Location_ShowMore1 = new Point(520, 5);
        public Point Location_ShowMore2 = new Point(555, 5);
        public Point Location_Option_OneRow = new Point(155,2);
        public Point Location_opiton_in_Option_OneRow = new Point(2, 5);
        public Point Location_SelectedOneRow = new Point(2, 5);
        public Point Location_label1_in_SelectedOneRow = new Point(3, 2);
        public Point Location_label2_in_SelectedOneRow;
        public Point Location_label3_in_SelectedOneRow;
        public Point Location_buttonx_in_SelectedOneRow;

        public static ListLocation ins;

        public ListLocation()
        {
            ins = this;
            Location_label2_in_SelectedOneRow = new Point(Location_label1_in_SelectedOneRow.X + ListSize.ins.size_label1_in_SelectedOneRow.Width + 1, 2);
            Location_label3_in_SelectedOneRow = new Point(Location_label2_in_SelectedOneRow.X + ListSize.ins.size_label2_in_SelectedOneRow.Width + 1, 2);
            Location_buttonx_in_SelectedOneRow = new Point(Location_label3_in_SelectedOneRow.X + ListSize.ins.size_label1_in_SelectedOneRow.Width + 1, 2);
        }
    }
}
