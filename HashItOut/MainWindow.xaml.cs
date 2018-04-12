using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace HashItOut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName = "";
        private string calculatedMD5 = "";
        private string calculatedSHA1 = "";
        private string inputMD5 = "";
        private string inputSHA1 = "";
        public MainWindow()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                validate(args[1]);
            }
        }

        private void txtBox_InputMD5_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputMD5 = txtBox_InputMD5.Text.Trim();
            compareValuesMD5();
        }
        private void txtBox_InputSHA1_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputSHA1 = txtBox_InputSHA1.Text.Trim();
            compareValuesSHA1();
        }
        protected string GetMD5HashFromFile(string nameOfFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(nameOfFile))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
        protected string GetSHA1HashFromFile(string nameOfFile)
        {
            using (var sha1 = SHA1.Create())
            {
                using (var stream = File.OpenRead(nameOfFile))
                {
                    return BitConverter.ToString(sha1.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a File to Validate";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                validate(ofd.FileName);
            }
        }
        private void compareValuesMD5()
        {
            if (inputMD5.Length > 0 && calculatedMD5.Length > 0)
            {
                if (inputMD5.ToLower().Equals(calculatedMD5.ToLower()))
                {
                    txtBlk_CompareMD5.Text = "MATCH";
                    txtBlk_CompareMD5.Foreground = Brushes.DarkGreen;
                }
                else
                {
                    txtBlk_CompareMD5.Text = "DIFFERENT";
                    txtBlk_CompareMD5.Foreground = Brushes.DarkRed;
                }
            }
        }
        private void compareValuesSHA1()
        {
            if (inputSHA1.Length > 0 && calculatedSHA1.Length > 0)
            {
                if (inputSHA1.ToLower().Equals(calculatedSHA1.ToLower()))
                {
                    txtBlk_CompareSHA1.Text = "MATCH";
                    txtBlk_CompareSHA1.Foreground = Brushes.DarkGreen;
                }
                else
                {
                    txtBlk_CompareSHA1.Text = "DIFFERENT";
                    txtBlk_CompareSHA1.Foreground = Brushes.DarkRed;
                }
            }
        }

        private void btn_Options_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
        
        private void validate(String inputFile)
        {
            fileName = inputFile;
            txtBlk_FilePath.Text = fileName;
            calculatedMD5 = GetMD5HashFromFile(fileName).ToLower();
            txtBlk_ValueMD5.Text = calculatedMD5;
            compareValuesMD5();
            calculatedSHA1 = GetSHA1HashFromFile(fileName).ToLower();
            txtBlk_ValueSHA1.Text = calculatedSHA1;
            compareValuesSHA1();
        }
    }
}