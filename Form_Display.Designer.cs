namespace Project2
{
    partial class Form_Display
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart_OLHCV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_LoadTickers = new System.Windows.Forms.Button();
            this.dateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label_EndDate = new System.Windows.Forms.Label();
            this.label_StartDate = new System.Windows.Forms.Label();
            this.openFileDialog_LoadTicker = new System.Windows.Forms.OpenFileDialog();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_Simulate = new System.Windows.Forms.Button();
            this.timer_Simulate = new System.Windows.Forms.Timer(this.components);
            this.hScrollBar_SimulationInterval = new System.Windows.Forms.HScrollBar();
            this.textBox_SimulationInterval = new System.Windows.Forms.TextBox();
            this.comboBox_Patterns = new System.Windows.Forms.ComboBox();
            this.label_Pattern = new System.Windows.Forms.Label();
            this.aCandlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_OLHCV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCandlestickBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_OLHCV
            // 
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.Name = "ChartArea_Volume";
            this.chart_OLHCV.ChartAreas.Add(chartArea1);
            this.chart_OLHCV.ChartAreas.Add(chartArea2);
            this.chart_OLHCV.DataSource = this.aCandlestickBindingSource;
            this.chart_OLHCV.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_OLHCV.Legends.Add(legend1);
            this.chart_OLHCV.Location = new System.Drawing.Point(0, 0);
            this.chart_OLHCV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart_OLHCV.Name = "chart_OLHCV";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series_OHLC";
            series1.XValueMember = "date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValueMembers = "high, low, open, close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_Volume";
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Series_Volume";
            series2.XValueMember = "date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.YValueMembers = "volume";
            this.chart_OLHCV.Series.Add(series1);
            this.chart_OLHCV.Series.Add(series2);
            this.chart_OLHCV.Size = new System.Drawing.Size(1470, 733);
            this.chart_OLHCV.TabIndex = 0;
            this.chart_OLHCV.Text = "chart1";
            title1.Name = "Title_Start";
            title1.Text = "Title Here";
            this.chart_OLHCV.Titles.Add(title1);
            // 
            // button_LoadTickers
            // 
            this.button_LoadTickers.Location = new System.Drawing.Point(32, 781);
            this.button_LoadTickers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_LoadTickers.Name = "button_LoadTickers";
            this.button_LoadTickers.Size = new System.Drawing.Size(212, 38);
            this.button_LoadTickers.TabIndex = 9;
            this.button_LoadTickers.Text = "Load Tickers";
            this.button_LoadTickers.UseVisualStyleBackColor = true;
            this.button_LoadTickers.Click += new System.EventHandler(this.button_LoadTickers_Click);
            // 
            // dateTimePicker_EndDate
            // 
            this.dateTimePicker_EndDate.Location = new System.Drawing.Point(980, 762);
            this.dateTimePicker_EndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker_EndDate.Name = "dateTimePicker_EndDate";
            this.dateTimePicker_EndDate.Size = new System.Drawing.Size(396, 31);
            this.dateTimePicker_EndDate.TabIndex = 8;
            // 
            // dateTimePicker_StartDate
            // 
            this.dateTimePicker_StartDate.Location = new System.Drawing.Point(420, 763);
            this.dateTimePicker_StartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker_StartDate.Name = "dateTimePicker_StartDate";
            this.dateTimePicker_StartDate.Size = new System.Drawing.Size(396, 31);
            this.dateTimePicker_StartDate.TabIndex = 7;
            // 
            // label_EndDate
            // 
            this.label_EndDate.AutoSize = true;
            this.label_EndDate.Location = new System.Drawing.Point(866, 767);
            this.label_EndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_EndDate.Name = "label_EndDate";
            this.label_EndDate.Size = new System.Drawing.Size(107, 25);
            this.label_EndDate.TabIndex = 6;
            this.label_EndDate.Text = "End Date:";
            // 
            // label_StartDate
            // 
            this.label_StartDate.AutoSize = true;
            this.label_StartDate.Location = new System.Drawing.Point(300, 767);
            this.label_StartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_StartDate.Name = "label_StartDate";
            this.label_StartDate.Size = new System.Drawing.Size(114, 25);
            this.label_StartDate.TabIndex = 5;
            this.label_StartDate.Text = "Start Date:";
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
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(32, 823);
            this.button_Refresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(212, 37);
            this.button_Refresh.TabIndex = 10;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // button_Simulate
            // 
            this.button_Simulate.Location = new System.Drawing.Point(32, 738);
            this.button_Simulate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Simulate.Name = "button_Simulate";
            this.button_Simulate.Size = new System.Drawing.Size(212, 37);
            this.button_Simulate.TabIndex = 11;
            this.button_Simulate.Text = "Simulate";
            this.button_Simulate.UseVisualStyleBackColor = true;
            this.button_Simulate.Click += new System.EventHandler(this.button_Simulate_Click);
            // 
            // timer_Simulate
            // 
            this.timer_Simulate.Interval = 500;
            this.timer_Simulate.Tick += new System.EventHandler(this.timer_Simulate_Tick);
            // 
            // hScrollBar_SimulationInterval
            // 
            this.hScrollBar_SimulationInterval.Location = new System.Drawing.Point(306, 815);
            this.hScrollBar_SimulationInterval.Maximum = 2000;
            this.hScrollBar_SimulationInterval.Minimum = 100;
            this.hScrollBar_SimulationInterval.Name = "hScrollBar_SimulationInterval";
            this.hScrollBar_SimulationInterval.Size = new System.Drawing.Size(482, 35);
            this.hScrollBar_SimulationInterval.TabIndex = 12;
            this.hScrollBar_SimulationInterval.Value = 1000;
            this.hScrollBar_SimulationInterval.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_SimulationInterval_Scroll);
            // 
            // textBox_SimulationInterval
            // 
            this.textBox_SimulationInterval.Location = new System.Drawing.Point(820, 819);
            this.textBox_SimulationInterval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_SimulationInterval.Name = "textBox_SimulationInterval";
            this.textBox_SimulationInterval.Size = new System.Drawing.Size(128, 31);
            this.textBox_SimulationInterval.TabIndex = 13;
            this.textBox_SimulationInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_Patterns
            // 
            this.comboBox_Patterns.FormattingEnabled = true;
            this.comboBox_Patterns.Location = new System.Drawing.Point(1184, 817);
            this.comboBox_Patterns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Patterns.Name = "comboBox_Patterns";
            this.comboBox_Patterns.Size = new System.Drawing.Size(192, 33);
            this.comboBox_Patterns.TabIndex = 14;
            // 
            // label_Pattern
            // 
            this.label_Pattern.AutoSize = true;
            this.label_Pattern.Location = new System.Drawing.Point(1086, 817);
            this.label_Pattern.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Pattern.Name = "label_Pattern";
            this.label_Pattern.Size = new System.Drawing.Size(92, 25);
            this.label_Pattern.TabIndex = 15;
            this.label_Pattern.Text = "Patterns";
            // 
            // aCandlestickBindingSource
            // 
            this.aCandlestickBindingSource.DataSource = typeof(Project2.aCandlestick);
            // 
            // Form_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1470, 871);
            this.Controls.Add(this.label_Pattern);
            this.Controls.Add(this.comboBox_Patterns);
            this.Controls.Add(this.textBox_SimulationInterval);
            this.Controls.Add(this.hScrollBar_SimulationInterval);
            this.Controls.Add(this.button_Simulate);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_LoadTickers);
            this.Controls.Add(this.dateTimePicker_EndDate);
            this.Controls.Add(this.dateTimePicker_StartDate);
            this.Controls.Add(this.label_EndDate);
            this.Controls.Add(this.label_StartDate);
            this.Controls.Add(this.chart_OLHCV);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form_Display";
            this.Text = "Stock Data";
            ((System.ComponentModel.ISupportInitialize)(this.chart_OLHCV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCandlestickBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_OLHCV;
        private System.Windows.Forms.Button button_LoadTickers;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartDate;
        private System.Windows.Forms.Label label_EndDate;
        private System.Windows.Forms.Label label_StartDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadTicker;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.BindingSource aCandlestickBindingSource;
        private System.Windows.Forms.Button button_Simulate;
        private System.Windows.Forms.Timer timer_Simulate;
        private System.Windows.Forms.HScrollBar hScrollBar_SimulationInterval;
        private System.Windows.Forms.TextBox textBox_SimulationInterval;
        private System.Windows.Forms.ComboBox comboBox_Patterns;
        private System.Windows.Forms.Label label_Pattern;
    }
}

