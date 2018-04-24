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
        /// <returns>The file path selected by the user.</returns>
        string OpenFileDialog();

        /// <summary>
        /// Opens an existing file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>A read-only <see cref="Stream"/> on the specified path.</returns>
        Stream OpenFile(string path);
    }
}
