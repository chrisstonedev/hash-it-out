using System.IO;

namespace HashItOut.Services
{
    /// <summary>
    /// Represents file-based operations.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Creates a dialog to allow the user to select a file to open.
        /// </summary>
        /// <returns>The </returns>
        string OpenFileDialog();

        Stream OpenFile(string path);
    }
}
