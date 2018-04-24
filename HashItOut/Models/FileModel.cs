using System.ComponentModel;

namespace HashItOut.Models
{
    /// <summary>
    /// Provides file-based operations.
    /// </summary>
    public class FileModel : INotifyPropertyChanged
    {
        private string selectedPath;

        /// <summary>
        /// Gets or sets the selected path of a file to hash.
        /// </summary>
        public string SelectedPath
        {
            get => selectedPath;
            set
            {
                selectedPath = value;
                RaisePropertyChanged("SelectedPath");
            }
        }

        /// <summary>
        /// Informs instances with references to a property that the value has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
