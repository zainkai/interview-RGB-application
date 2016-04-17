using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterviewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtFilePath.Text = ofd.FileName;
                }
            }
        }

        private void btnBeginProcessing_Click(object sender, RoutedEventArgs e)
        {
            string filePath = this.txtFilePath.Text;

            this.txtOutput.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(filePath)
                && System.IO.File.Exists(filePath))
            {
                IColorReader reader = new ColorReader();
                ((ColorReader)reader).CallBack += this.UpdateStatus;

                this.UpdateStatus("Process Beginning...");

                ((ColorReader)reader).StartProcessing(filePath);

                this.UpdateStatus("Process Finished");
                this.UpdateStatus(reader.MatchesFound.ToString() + " total matches found");
                this.UpdateStatus(reader.ColorsFound.ToString() + " had Red as the most significant color");
                this.UpdateStatus(this.txtFilePath.Text.Split("\n".ToCharArray()).ToString() + " total rows displayed in GUI");
            }
            else
            {
                this.UpdateStatus("File not found or file not selected");
            }
        }

        private void UpdateStatus(string message)
        {
            this.Dispatcher.BeginInvoke((MethodInvoker)delegate
            {
                this.txtOutput.Text += message + Environment.NewLine;
            });
        }

        #endregion
    }
}
