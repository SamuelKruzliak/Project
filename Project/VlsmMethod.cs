using System.Collections.Generic;
using NetworkTools;

namespace Project
{
    class VlsmMethod
    {
        public static List<string> getVlsm(string ipAdress, string hosts)
        {
            return IpParser.Vlsm(ipAdress, hosts);
        }
    }
}
