using System;
using System.Collections.Generic;

namespace Project
{
    class IpCalculator
    {
        static public List<byte> ParseIpAdress(string ipAdress)
        {
            List<byte> list = new List<byte>();
            const int NumberOfIpAdressParts = 4;

            for (int i = 0; i < NumberOfIpAdressParts; i++)
            {
               if (ipAdress.IndexOf(".") == -1)
               {
                    list.Add(Convert.ToByte(ipAdress));
               }
               else if (ipAdress.IndexOf(".") != -1)
               {
                    list.Add(Convert.ToByte(ipAdress.Substring(0, ipAdress.IndexOf("."))));
                    ipAdress = ipAdress.Substring(ipAdress.IndexOf(".") + 1, ipAdress.Length - ipAdress.IndexOf(".") - 1);
               }
            }

            return list;
        }
        static public List<string> Result(int BorroweBits, double add, List<byte> list, int index)
        {
            List<string> ipAdresses = new List<string>();
            for (int i = 0; i < Math.Pow(2, BorroweBits); i++)
            {
                string k = list[0] + "." + list[1] + "." + list[2] + "." + list[3].ToString();
                list[index] += Convert.ToByte(add);
                ipAdresses.Add(k);
            }

            return ipAdresses;

        }
       

    }
}
