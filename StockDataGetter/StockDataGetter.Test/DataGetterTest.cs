﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;

namespace StockDataGetter.Test
{
    [TestFixture]
    public class DataGetterTest
    {

        [Test]public void Test1()
        {
            DataGetter dg = new DataGetter("000716", DataType.GetValue("总资产利润率"));
            String[] d = dg.excute();
            Assert.AreEqual(51, d.Length);
        }
    }
}
