using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.Model;
using NPOI.SS.Util;

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
            formStateUpdate(FormState.Wait);
            quickSelect.SelectedIndex = 0;
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
            if ("获取".Equals(getButton.Text))
            {
                if (!getBackgroundWorker.IsBusy)
                {
                    getBackgroundWorker.RunWorkerAsync();
                    progressBar.Minimum = 0;
                    progressBar.Maximum = types.Count;
                    formStateUpdate(FormState.Running);
                }
            }
            else
            {
                getBackgroundWorker.CancelAsync();
            }
        }

        private void getBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (!getBackgroundWorker.CancellationPending)
                {
                    getBackgroundWorker.ReportProgress(i, "获取" + types[i]);
                    data.Add(types[i], new DataGetter(number, DataType.GetValue(types[i])).excute());
                    getBackgroundWorker.ReportProgress(i + 1);
                }
                else
                {
                    break;
                }
            }
            getBackgroundWorker.ReportProgress(100, "写入文件");
            writeToFile();
        }

        #region 写入文件
        private void writeToFile()
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet("数据");
            sheet.DefaultColumnWidth = 18;

            #region 创建头部
            IRow row1 = sheet.CreateRow(0);
            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("代码：" + number);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, types.Count));
            IRow row2 = sheet.CreateRow(1);
            row2.CreateCell(0).SetCellValue("日期");
            for (int i = 0; i < types.Count; i++)
            {
                row2.CreateCell(i + 1).SetCellValue(types[i]);
            }
            #endregion

            int length = 0;

            #region 补全行和列
            foreach (String key in data.Keys)
            {
                String[] currentData;
                data.TryGetValue(key, out currentData);
                length = currentData.Length;
                for (int i = 0; i < currentData.Length; i++)
                {
                    IRow row = sheet.CreateRow(i + 2);
                    for (int j = 0; j <= types.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(j);
                    }
                }
                break;
            }
            #endregion

            #region 填充数据

            bool isCreateTime = false;
            int columIndex = 0;
            ICellStyle dataStyle = hssfworkbook.CreateCellStyle();
            IDataFormat format = hssfworkbook.CreateDataFormat();
            dataStyle.DataFormat = format.GetFormat("yyyy年m月d日");
            ICellStyle indexStyle = hssfworkbook.CreateCellStyle();
            indexStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.0000");

            foreach (String key in data.Keys)
            {
                String[] currentData;
                data.TryGetValue(key, out currentData);
                if (!isCreateTime)
                {
                    for (int i = 0; i < currentData.Length; i++)
                    {
                        sheet.GetRow(i + 2).GetCell(columIndex).CellStyle = dataStyle;
                        sheet.GetRow(i + 2).GetCell(columIndex).SetCellValue(DateTime.Parse(currentData[i].Split(',')[0]));
                    }
                    columIndex++;
                    isCreateTime = true;
                    for (int i = 0; i < currentData.Length; i++)
                    {
                        sheet.GetRow(i + 2).GetCell(columIndex).CellStyle = indexStyle;
                        sheet.GetRow(i + 2).GetCell(columIndex).SetCellValue(double.Parse(currentData[i].Split(',')[1]));
                    }
                    columIndex++;
                }
                else
                {
                    for (int i = 0; i < currentData.Length; i++)
                    {
                        sheet.GetRow(i + 2).GetCell(columIndex).CellStyle = indexStyle;
                        sheet.GetRow(i + 2).GetCell(columIndex).SetCellValue(double.Parse(currentData[i].Split(',')[1]));
                    }
                    columIndex++;
                }
            }

            #endregion

            FileStream file = new FileStream(String.Format("{0}.xls", number), FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        #endregion

        private void getBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int progress = e.ProgressPercentage;
                if (!progress.Equals(100))
                {
                    progressBar.Value = progress;
                }
                if (e.UserState != null)
                {
                    progressBarLabel.Text = "正在" + e.UserState.ToString();
                    int labelWidth = progressBarLabel.Size.Width;
                    int width = progressPanel.Size.Width;
                    if (width <= labelWidth)
                    {
                        progressBarLabel.Location = new Point(0, progressBarLabel.Location.Y);
                    }
                    else
                    {
                        progressBarLabel.Location = new Point((width - labelWidth) / 2, progressBarLabel.Location.Y);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void quickSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (quickSelect.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    quickSelectMethod(0, 8);
                    break;
                case 2:
                    quickSelectMethod(9, 18);
                    break;
                case 3:
                    quickSelectMethod(19, 26);
                    break;
                case 4:
                    quickSelectMethod(27, 39);
                    break;
                case 5:
                    quickSelectMethod(40, 50);
                    break;
                case 6:
                    quickSelectMethod(51, 57);
                    break;
                case 7:
                    quickSelectMethod(58, 63);
                    break;
                case 8:
                    quickSelectMethod(0, 63);
                    break;
                case 9:
                    quickSelectMethod(0, 63, false);
                    break;
            }
        }

        private void quickSelectMethod(int startIndex, int endIndex, bool state)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                typeCheckedListBox.SetItemChecked(i, state);
            }
            if (startIndex<=8)
            {
                typeCheckedListBox.SelectedIndex = 0;
            }
            else if (startIndex < typeCheckedListBox.Items.Count - 7)
            {
                typeCheckedListBox.SelectedIndex = startIndex + 7;
            }
            else
            {
                typeCheckedListBox.SelectedIndex=endIndex;
            }
        }

        private void quickSelectMethod(int startIndex, int endIndex)
        {
            quickSelectMethod(startIndex, endIndex, true);
        }

        private void formStateUpdate(FormState state)
        {
            switch (state)
            {
                case FormState.Wait:
                    getButton.Text = "获取";
                    progressPanel.Visible = false;
                    typeCheckedListBox.Enabled = true;
                    quickSelect.Enabled = true;
                    break;
                case FormState.Running:
                    getButton.Text = "取消";
                    typeCheckedListBox.Enabled = false;
                    progressPanel.Visible = true;
                    quickSelect.Enabled = false;
                    break;
            }
        }

        enum FormState { Wait, Running };

        private void getBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            formStateUpdate(FormState.Wait);
        }

        private void homeLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/stockdatagetter/");
        }
    }
}
