using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynceOToHTLT.Common
{
    #nullable disable
    public interface IAppSettings
    {
        ConnectionSetting ConnectionSetting { get; set; }
    }

    internal class AppSettings : IAppSettings
    {
        public ConnectionSetting ConnectionSetting { get; set; }
    }

    public class ConnectionSetting
    {
        public string EOConnectionString { get; set; }
        public string HtltConnectionString { get; set; }
    }
}
