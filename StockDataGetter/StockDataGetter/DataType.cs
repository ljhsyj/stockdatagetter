using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockDataGetter
{
    public class DataType
    {
        public static Dictionary<String, String> type = new Dictionary<string, string>();

        static DataType()
        {
            using (StreamReader sr = new StreamReader("data.dat",Encoding.GetEncoding("utf-8")))
            {
                String line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    type.Add(line.Split(',')[0], line.Split(',')[1]);
                }
                sr.Close();
            }
        }

        public static String GetValue(String key)
        {
            String tempData;
            type.TryGetValue(key,out tempData);
            return tempData;
        }
    }
}
