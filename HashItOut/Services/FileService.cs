using Microsoft.Win32;
using System.IO;

namespace HashItOut.Services
{
    /// <summary>
    /// Performs file-based operations.
    /// </summary>
    class FileService : IFileService
    {
        /// <summary>
        /// Implements <see cref="IFileService.OpenFile(string)"/>.
        /// </summary>
        public Stream OpenFile(string path)
        {
            return File.OpenRead(path);
        }

        /// <summary>
        /// Implements <see cref="IFileService.OpenFileDialog"/>.
        /// </summary>
        public string OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File to Validate"
            };

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;

            return null;
        }
    }
}
