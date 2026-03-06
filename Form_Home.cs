using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2
{
    public partial class Form_Home : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Form_Home()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open openFileDialog() to select ticker files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LoadTickers_Click(object sender, EventArgs e)
        {
            openFileDialog_LoadTicker.ShowDialog();
        }

        /// <summary>
        /// From selected csv file(s), process and display chart for each ticker file in separate forms and 
        /// present appropriate titles for each
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_LoadTicker_FileOk(object sender, CancelEventArgs e)
        {
            DateTime startDate = dateTimePicker_StartDate.Value;
            DateTime endDate = dateTimePicker_EndDate.Value;
            String[] filePaths = openFileDialog_LoadTicker.FileNames;

            foreach (String path in filePaths) new Form_Display(path, startDate, endDate);
        }
    }
}
