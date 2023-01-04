using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynceOToHTLT.Services
{
    public class ListSize
    {
        
        public Size size_OneRow = new Size(590, 40);
        public Size size_ShowMore = new Size(30, 26);
        public Size size_SelectedOneRow = new Size(350, 28);
        public Size size_Option_OneRow = new Size(360, 35);
        public Size size_label_OneRow = new Size(120,26);
        public Size size_option_in_Option_Listshow = new Size(350, 26);
        public Size size_label1_in_SelectedOneRow = new Size(150, 20);
        public Size size_label2_in_SelectedOneRow = new Size(8, 20);


        public static ListSize ins;
        public ListSize()
        {
            ins = this;
        }
    }
}
