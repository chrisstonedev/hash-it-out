using HashItOut.Services;
using HashItOut.ViewModels;
using System.Windows;

namespace HashItOut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HashViewControl.DataContext = new HashViewModel(new FileService());
        }
    }
}