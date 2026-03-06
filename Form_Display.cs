using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project2
{
    public partial class Form_Display : Form
    {
        // Initiate lists for candlesticks and filtered candlesticks
        List<smartCandleStick> candlesticks = new List<smartCandleStick>();
        List<smartCandleStick> filteredCandlesticks = new List<smartCandleStick>();
        List<smartCandleStick> simulateCandleSticks = new List<smartCandleStick>(capacity: 256);
        List<Recognizer> LOR;

        // Name of ticker file without extension
        string tickerFile;

        // Catalog of possible patterns (do NOT preload ComboBox)
        private static readonly string[] AllPatternCatalog = new[]
        {
            "Doji", "Hammer", "Inverted Hammer", "Marubozu", "Harami", "Engulfing"
        };

        // Track which patterns have appeared so far
        private readonly HashSet<string> patternsFound = new HashSet<string>(StringComparer.Ordinal);

        public Form_Display()
        {
            InitializeComponent();
            InitializePatternComboBox();
        }

        public Form_Display(string fileName, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            InitializePatternComboBox();

            candlesticks = readCandlestickFile(fileName);

            tickerFile = Path.GetFileNameWithoutExtension(fileName);

            openFileDialog_LoadTicker.FileName = fileName;

            dateTimePicker_StartDate.Value = startDate;
            dateTimePicker_EndDate.Value = endDate;

            refreshDisplay();

            textBox_SimulationInterval.DataBindings.
            Add("Text", hScrollBar_SimulationInterval, "Value", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void InitializePatternComboBox()
        {
            comboBox_Patterns.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Patterns.Items.Clear(); // start EMPTY as requested
            comboBox_Patterns.SelectedIndexChanged += comboBox_Patterns_SelectedIndexChanged;
        }

        private void comboBox_Patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePatternAnnotations();
        }

        public void refreshDisplay()
        {
            filteredCandlesticks = filterCandlesticks(candlesticks, dateTimePicker_StartDate.Value, dateTimePicker_EndDate.Value);
            chart_OLHCV.DataSource = filteredCandlesticks;
            chart_OLHCV.DataBind(); // ensure points exist

            chart_OLHCV.Titles.Clear();
            chart_OLHCV.Titles.Add(tickerFile);
            chart_OLHCV.Titles.Add(dateTimePicker_StartDate.Value.Date + " -- " + dateTimePicker_EndDate.Value.Date);

            this.Show();

            // No patterns added here; simulation drives discovery
            if (simulateCandleSticks.Count == 0)
                UpdatePatternAnnotations();
        }

        private void button_LoadTickers_Click(object sender, EventArgs e)
        {
            openFileDialog_LoadTicker.ShowDialog();
        }

        private List<smartCandleStick> readCandlestickFile(string tickerFile)
        {
            var candleSticks = new List<smartCandleStick>();

            try
            {
                using (var reader = new StreamReader(tickerFile))
                {
                    string line;
                    bool isFirstLine = true;
                    int lineNo = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNo++;

                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        try
                        {
                            var c = new smartCandleStick(line.Trim('"'));
                            candleSticks.Add(c);
                        }
                        catch (Exception parseEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"Skipping line {lineNo}: {parseEx.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file could not be read:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return candleSticks;
        }

        public List<smartCandleStick> filterCandlesticks(List<smartCandleStick> originalList, DateTime startDate, DateTime endDate)
        {
            if (originalList == null || originalList.Count == 0)
                return new List<smartCandleStick>();

            if (startDate > endDate)
            {
                var tmp = startDate;
                startDate = endDate;
                endDate = tmp;
            }

            var sorted = originalList.OrderBy(c => c.date);

            var result = new List<smartCandleStick>();
            foreach (var candlestick in sorted)
            {
                if (candlestick.date < startDate) continue;
                if (candlestick.date > endDate) break;
                result.Add(candlestick);
            }

            return result;
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            refreshDisplay();
        }

        private void openFileDialog_LoadTicker_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = openFileDialog_LoadTicker.FileName;
            DateTime startDate = dateTimePicker_StartDate.Value;
            DateTime endDate = dateTimePicker_EndDate.Value;
            String[] filePaths = openFileDialog_LoadTicker.FileNames;

            foreach (String path in filePaths) new Form_Display(path, startDate, endDate);
        }

        private void button_Simulate_Click(object sender, EventArgs e)
        {
            simulateCandleSticks.Clear();
            chart_OLHCV.DataSource = simulateCandleSticks;
            chart_OLHCV.DataBind(); // ensure points exist

            // Reset pattern discovery state
            patternsFound.Clear();
            comboBox_Patterns.Items.Clear();
            comboBox_Patterns.Enabled = true;

            timer_Simulate.Start();
        }

        private void timer_Simulate_Tick(object sender, EventArgs e)
        {
            timer_Simulate.Stop();

            displayNextCandleStick();

            // Discover patterns incrementally as new candles appear
            DetectNewPatterns();

            // Annotate only for currently selected pattern (if any)
            UpdatePatternAnnotations();

            if (simulateCandleSticks.Count < filteredCandlesticks.Count)
            {
                timer_Simulate.Start();
            }
            else
            {
                comboBox_Patterns.Enabled = true;
            }
        }

        private void displayNextCandleStick()
        {
            smartCandleStick nextCandlestick = filteredCandlesticks[simulateCandleSticks.Count];
            simulateCandleSticks.Add(nextCandlestick);
            chart_OLHCV.DataBind();
        }

        private void hScrollBar_SimulationInterval_Scroll(object sender, ScrollEventArgs e)
        {
            timer_Simulate.Interval = hScrollBar_SimulationInterval.Value;
        }

        public void initializeRecognizers()
        {
            LOR = new List<Recognizer>
            {
                new Recognizer_Hammer(),
                new Recognizer_Inverted_Hammer(),
                new Recognizer_Doji(),
                new Recognizer_Marubozu(),
                new Recognizer_Harami()
            };
        }

        public void testRecognizers()
        {
            foreach (var recognizer in LOR)
            {
                bool result = recognizer.recognize(filteredCandlesticks);
                Console.WriteLine($"{recognizer.GetType().Name} recognition result: {result}");
            }
        }

        private void button_TestPatterns_Click(object sender, EventArgs e)
        {
            var smart = filteredCandlesticks.Select(cs => new smartCandleStick(cs)).ToList();

            var rDoji = new Recognizer_Doji();
            var rHammer = new Recognizer_Hammer();
            var rInvHammer = new Recognizer_Inverted_Hammer();
            var rMaru = new Recognizer_Marubozu();

            bool hasDoji = rDoji.recognize(smart);
            bool hasHammer = rHammer.recognize(smart);
            bool hasInvHammer = rInvHammer.recognize(smart);
            bool hasMarubozu = rMaru.recognize(smart);

            var dojiIndexes = rDoji.GetMatches();
            var hammerIndexes = rHammer.GetMatches();
        }

        // ===== Pattern selection + annotation =====

        private Recognizer CreateRecognizer(string patternName)
        {
            switch (patternName)
            {
                case "Doji": return new Recognizer_Doji();
                case "Hammer": return new Recognizer_Hammer();
                case "Inverted Hammer": return new Recognizer_Inverted_Hammer();
                case "Marubozu": return new Recognizer_Marubozu();
                case "Harami": return new Recognizer_Harami();
                case "Engulfing": return new Recognizer_EP();
                default: return null;
            }
        }

        // Drive discovery during simulation; guard null recognizer
        private void DetectNewPatterns()
        {
            if (simulateCandleSticks.Count == 0) return;

            foreach (var patternName in AllPatternCatalog)
            {
                if (patternsFound.Contains(patternName)) continue;

                var recognizer = CreateRecognizer(patternName);
                if (recognizer == null) continue;

                var copy = simulateCandleSticks.Select(cs => new smartCandleStick(cs)).ToList();
                if (recognizer.recognize(copy))
                {
                    patternsFound.Add(patternName);
                    AddPatternToComboBox(patternName);
                }
            }
        }

        private void AddPatternToComboBox(string patternName)
        {
            if (!comboBox_Patterns.Items.Contains("All"))
                comboBox_Patterns.Items.Add("All");

            if (!comboBox_Patterns.Items.Contains(patternName))
                comboBox_Patterns.Items.Add(patternName);

            if (comboBox_Patterns.SelectedIndex == -1)
                comboBox_Patterns.SelectedItem = patternName;
        }

        // Annotate with index guards and rebind points if needed
        private void UpdatePatternAnnotations()
        {
            if (chart_OLHCV.Series.Count > 0 && chart_OLHCV.Series[0].Points.Count == 0 && chart_OLHCV.DataSource != null)
                chart_OLHCV.DataBind();

            var data = simulateCandleSticks.Count > 0 ? simulateCandleSticks : filteredCandlesticks;

            chart_OLHCV.SuspendLayout();
            chart_OLHCV.Annotations.Clear();

            if (data == null || data.Count == 0 || comboBox_Patterns.SelectedItem == null)
            {
                chart_OLHCV.ResumeLayout();
                return;
            }

            var series = chart_OLHCV.Series.Count > 0 ? chart_OLHCV.Series[0] : null;
            if (series == null) { chart_OLHCV.ResumeLayout(); return; }

            series.IsXValueIndexed = true;

            string selection = comboBox_Patterns.SelectedItem.ToString();
            if (selection == "All")
            {
                foreach (var name in comboBox_Patterns.Items.Cast<object>().Select(o => o.ToString()).Where(n => n != "All"))
                    AnnotatePattern(name, data, series);
            }
            else
            {
                AnnotatePattern(selection, data, series);
            }

            chart_OLHCV.ResumeLayout();
            chart_OLHCV.Invalidate();
        }

        private void AnnotatePattern(string patternName, List<smartCandleStick> data, Series series)
        {
            var recognizer = CreateRecognizer(patternName);
            if (recognizer == null) return;

            var work = data.Select(cs => new smartCandleStick(cs)).ToList();
            recognizer.recognize(work);
            var indexes = recognizer.GetMatches();

            var color = GetPatternColor(patternName);

            foreach (var idx in indexes)
            {
                if (idx < 0 || idx >= series.Points.Count) continue;

                var ann = new RectangleAnnotation
                {
                    AnchorDataPoint = series.Points[idx],
                    LineColor = color,
                    BackColor = Color.FromArgb(60, color),
                    ForeColor = color,
                    Text = patternName,
                    Alignment = ContentAlignment.TopCenter,
                    AnchorAlignment = ContentAlignment.BottomCenter
                };

                chart_OLHCV.Annotations.Add(ann);
            }
        }

        private Color GetPatternColor(string patternName)
        {
            switch (patternName)
            {
                case "Doji": return Color.Goldenrod;
                case "Hammer": return Color.LimeGreen;
                case "Inverted Hammer": return Color.SeaGreen;
                case "Marubozu": return Color.DeepSkyBlue;
                case "Harami": return Color.OrangeRed;
                default: return Color.MediumPurple;
            }
        }
    }
}