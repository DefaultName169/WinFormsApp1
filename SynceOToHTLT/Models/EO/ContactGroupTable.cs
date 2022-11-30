using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class ContactGroupTable
    {
        public int ContactGroupId { get; set; }
        public string ContactGroupName { get; set; } = null!;
        public int? ContactGroupParentId { get; set; }
    }
}
