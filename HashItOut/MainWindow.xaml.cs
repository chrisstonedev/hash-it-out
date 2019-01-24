using HashItOut.Services;
using HashItOut.ViewModels;
using System;
using System.Windows;

namespace HashItOut
{
    /// <summary>
    /// Provides interaction logic for the main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                HashViewControl.DataContext = new HashViewModel(new FileService());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.ToString());
                else
                    MessageBox.Show(ex.ToString());
            }
        }
    }
}