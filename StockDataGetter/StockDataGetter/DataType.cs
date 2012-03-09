using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockDataGetter
{
    public class DataType
    {
        public static Dictionary<String, String> type = new Dictionary<string, string>();

        static DataType()
        {
            type.Add("总资产利润率", "F028");
            type.Add("主营业务利润率", "F029");
            type.Add("净资产报酬率", "F030");
            type.Add("总资产报酬率", "F031");
            type.Add("销售毛利率", "F032");
        }

        public static String GetValue(String key)
        {
            String tempData;
            type.TryGetValue(key,out tempData);
            return tempData;
        }
    }
}
