using System.ComponentModel;

namespace HashItOut.Models
{
    public class FileModel : INotifyPropertyChanged
    {
        private string selectedPath;
        public string SelectedPath
        {
            get => selectedPath;
            set
            {
                selectedPath = value;
                RaisePropertyChanged("SelectedPath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
