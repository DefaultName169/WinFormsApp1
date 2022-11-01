using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SynceOToHTLT.Models.HTLT
{
    public class Audit
    {
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public int CreateUser { get; set; }
        [JsonIgnore]
        public DateTime LastUpdate { get; set; }
        [JsonIgnore]
        public int UpdateUser { get; set; }
    }
}
