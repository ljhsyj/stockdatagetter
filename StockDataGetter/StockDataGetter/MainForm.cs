using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockDataGetter
{
    public partial class MainForm : Form
    {
        List<String> types = new List<String>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void getButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                types.Add(DataType.总资产利润率);
            }
            getBackgroundWorker.RunWorkerAsync();
        }

        private void getBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (String type in types)
            {

            }
        }
    }
}
