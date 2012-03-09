namespace StockDataGetter
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.getBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.setData = new System.Windows.Forms.GroupBox();
            this.getButton = new System.Windows.Forms.Button();
            this.stockNumber = new System.Windows.Forms.TextBox();
            this.selectType = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.setData.SuspendLayout();
            this.selectType.SuspendLayout();
            this.SuspendLayout();
            // 
            // getBackgroundWorker
            // 
            this.getBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getBackgroundWorker_DoWork);
            // 
            // setData
            // 
            this.setData.Controls.Add(this.stockNumber);
            this.setData.Controls.Add(this.getButton);
            this.setData.Location = new System.Drawing.Point(13, 13);
            this.setData.Name = "setData";
            this.setData.Size = new System.Drawing.Size(305, 58);
            this.setData.TabIndex = 1;
            this.setData.TabStop = false;
            this.setData.Text = "指定上市公司";
            // 
            // getButton
            // 
            this.getButton.Location = new System.Drawing.Point(224, 20);
            this.getButton.Name = "getButton";
            this.getButton.Size = new System.Drawing.Size(75, 23);
            this.getButton.TabIndex = 1;
            this.getButton.Text = "获取";
            this.getButton.UseVisualStyleBackColor = true;
            this.getButton.Click += new System.EventHandler(this.getButton_Click);
            // 
            // stockNumber
            // 
            this.stockNumber.Location = new System.Drawing.Point(17, 21);
            this.stockNumber.Name = "stockNumber";
            this.stockNumber.Size = new System.Drawing.Size(151, 21);
            this.stockNumber.TabIndex = 2;
            // 
            // selectType
            // 
            this.selectType.Controls.Add(this.checkBox1);
            this.selectType.Location = new System.Drawing.Point(13, 78);
            this.selectType.Name = "selectType";
            this.selectType.Size = new System.Drawing.Size(305, 81);
            this.selectType.TabIndex = 2;
            this.selectType.TabStop = false;
            this.selectType.Text = "选择获取的数据项";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "总资产利润率";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 205);
            this.Controls.Add(this.selectType);
            this.Controls.Add(this.setData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "上市公司数据获取工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.setData.ResumeLayout(false);
            this.setData.PerformLayout();
            this.selectType.ResumeLayout(false);
            this.selectType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker getBackgroundWorker;
        private System.Windows.Forms.GroupBox setData;
        private System.Windows.Forms.Button getButton;
        private System.Windows.Forms.TextBox stockNumber;
        private System.Windows.Forms.GroupBox selectType;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

