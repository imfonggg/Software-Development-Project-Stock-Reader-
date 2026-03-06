namespace Project2
{
    partial class Form_Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_StartDate = new System.Windows.Forms.Label();
            this.label_EndDate = new System.Windows.Forms.Label();
            this.dateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.button_LoadTickers = new System.Windows.Forms.Button();
            this.openFileDialog_LoadTicker = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label_StartDate
            // 
            this.label_StartDate.AutoSize = true;
            this.label_StartDate.Location = new System.Drawing.Point(26, 51);
            this.label_StartDate.Name = "label_StartDate";
            this.label_StartDate.Size = new System.Drawing.Size(114, 25);
            this.label_StartDate.TabIndex = 0;
            this.label_StartDate.Text = "Start Date:";
            // 
            // label_EndDate
            // 
            this.label_EndDate.AutoSize = true;
            this.label_EndDate.Location = new System.Drawing.Point(869, 52);
            this.label_EndDate.Name = "label_EndDate";
            this.label_EndDate.Size = new System.Drawing.Size(107, 25);
            this.label_EndDate.TabIndex = 1;
            this.label_EndDate.Text = "End Date:";
            // 
            // dateTimePicker_StartDate
            // 
            this.dateTimePicker_StartDate.Location = new System.Drawing.Point(146, 46);
            this.dateTimePicker_StartDate.Name = "dateTimePicker_StartDate";
            this.dateTimePicker_StartDate.Size = new System.Drawing.Size(397, 31);
            this.dateTimePicker_StartDate.TabIndex = 2;
            this.dateTimePicker_StartDate.Value = new System.DateTime(2023, 3, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_EndDate
            // 
            this.dateTimePicker_EndDate.Location = new System.Drawing.Point(982, 46);
            this.dateTimePicker_EndDate.Name = "dateTimePicker_EndDate";
            this.dateTimePicker_EndDate.Size = new System.Drawing.Size(397, 31);
            this.dateTimePicker_EndDate.TabIndex = 3;
            this.dateTimePicker_EndDate.Value = new System.DateTime(2024, 4, 1, 0, 0, 0, 0);
            // 
            // button_LoadTickers
            // 
            this.button_LoadTickers.Location = new System.Drawing.Point(603, 40);
            this.button_LoadTickers.Name = "button_LoadTickers";
            this.button_LoadTickers.Size = new System.Drawing.Size(211, 47);
            this.button_LoadTickers.TabIndex = 4;
            this.button_LoadTickers.Text = "Load Tickers";
            this.button_LoadTickers.UseVisualStyleBackColor = true;
            this.button_LoadTickers.Click += new System.EventHandler(this.button_LoadTickers_Click);
            // 
            // openFileDialog_LoadTicker
            // 
            this.openFileDialog_LoadTicker.FileName = "AAPL-Month";
            this.openFileDialog_LoadTicker.Filter = "All Files|*.CSV|Monthly Files|*-Month.csv|Weekly Files|*-Week.csv|Daily Files|*-D" +
    "ay.csv";
            this.openFileDialog_LoadTicker.InitialDirectory = "\"C:\\Users\\popot\\Downloads\\Stock Data\"";
            this.openFileDialog_LoadTicker.Multiselect = true;
            this.openFileDialog_LoadTicker.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_LoadTicker_FileOk);
            // 
            // Form_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1408, 110);
            this.Controls.Add(this.button_LoadTickers);
            this.Controls.Add(this.dateTimePicker_EndDate);
            this.Controls.Add(this.dateTimePicker_StartDate);
            this.Controls.Add(this.label_EndDate);
            this.Controls.Add(this.label_StartDate);
            this.Name = "Form_Home";
            this.Text = "Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_StartDate;
        private System.Windows.Forms.Label label_EndDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndDate;
        private System.Windows.Forms.Button button_LoadTickers;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadTicker;
    }
}