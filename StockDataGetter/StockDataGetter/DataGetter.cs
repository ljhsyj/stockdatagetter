using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSoup;
using NSoup.Select;
using System.Collections;
using NSoup.Nodes;
using System.Net;

namespace StockDataGetter
{
    public class DataGetter
    {
        private String baseUri = "http://app.finance.ifeng.com/data/stock/cwzb_item.php?symbol={0}&item={1}";

        private String number;

        public String Number
        {
            get { return number; }
            set { number = value; }
        }
        private String type;

        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public DataGetter(String number, String type)
        {
            this.number = number;
            this.type = type;
        }

        public String[] excute()
        {
            ArrayList list = new ArrayList();
            String url = String.Format(baseUri, this.number, this.type);
            WebClient wc = new WebClient();
            Document doc = NSoupClient.Parse(wc.OpenRead(url), "utf-8");
            Element table = doc.Select("div.tab01 tbody").First();
            Elements trs = table.Select("tr");
            for (int i=1;i<trs.Count;i++)
            {
                Elements tds = trs[i].Select("td");
                list.Add(tds[0].Text()+","+tds[1].Text());
            }
            String[] returnValue = new String[list.Count];
            for (int i = 0; i < returnValue.Length; i++)
            {
                returnValue[i] = (String)list[i];
            }
            return returnValue;
        }
    }
}
