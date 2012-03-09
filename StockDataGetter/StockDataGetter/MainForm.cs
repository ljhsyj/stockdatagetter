using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace StockDataGetter
{
    public partial class MainForm : Form
    {
        List<String> types = new List<String>();
        Dictionary<String, String[]> data = new Dictionary<string, string[]>();
        private String number;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            typeCheckedListBox.Items.Clear();
            foreach (String key in DataType.type.Keys)
            {
                typeCheckedListBox.Items.Add(key);
            }
            progressPanel.Visible = false;
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            number = stockNumber.Text;
            types.Clear();
            data.Clear();
            for (int i = 0; i < typeCheckedListBox.Items.Count; i++)
            {
                if (typeCheckedListBox.GetItemChecked(i))
                {
                    types.Add(typeCheckedListBox.Items[i].ToString());
                }
            }
            if (!getBackgroundWorker.IsBusy)
            {
                getBackgroundWorker.RunWorkerAsync();
                progressBar.Minimum = 0;
                progressBar.Maximum = types.Count;
                progressPanel.Visible = true;
            }
        }

        private void getBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i=0;i<types.Count;i++)
            {
                getBackgroundWorker.ReportProgress(i,types[i]);
                data.Add(types[i], new DataGetter(number, DataType.GetValue(types[i])).excute());
                getBackgroundWorker.ReportProgress(i+1);
            }
            getBackgroundWorker.ReportProgress(100);
        }

        private void getBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int progress = e.ProgressPercentage;
                if (progress.Equals(100))
                {
                    progressPanel.Visible = false;
                }
                else
                {
                    progressBar.Value = progress;
                    if (e.UserState != null)
                    {
                        progressBarLabel.Text = "正在获取" + e.UserState.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
