using HashItOut.Models;
using HashItOut.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;

namespace HashItOut.ViewModels
{
    /// <summary>
    /// Represents state and operations for the program.
    /// </summary>
    public class HashViewModel
    {
        private readonly IFileService fileService;

        /// <summary>
        /// Gets a collection of algorithms used to hash a file.
        /// </summary>
        public ObservableCollection<AlgorithmModel> Algorithms { get; private set; }
            = new ObservableCollection<AlgorithmModel>
            {
                new AlgorithmModel(HashAlgorithmType.MD5),
                new AlgorithmModel(HashAlgorithmType.SHA1)
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="HashViewModel"/> class with dependencies.
        /// </summary>
        /// <param name="fileService">An instance of a file operstions service.</param>
        public HashViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            BrowseCommand = new RelayCommand(BrowseForFile);
        }

        /// <summary>
        /// Gets or sets an instance of a file operations model.
        /// </summary>
        public FileModel File { get; set; } = new FileModel();

        /// <summary>
        /// Informs instances with references to a property that the value has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// A command that tells the program to browse for a file to hash.
        /// </summary>
        public RelayCommand BrowseCommand { get; set; }

        private void BrowseForFile()
        {
            File.SelectedPath = fileService.OpenFileDialog() ?? string.Empty;

            if (string.IsNullOrEmpty(File.SelectedPath))
                return;

            foreach (AlgorithmModel algorithm in Algorithms)
            {
                HashAlgorithm hashAlgorithm = null;

                switch (algorithm.Type)
                {
                    case HashAlgorithmType.MD5:
                        hashAlgorithm = MD5.Create();
                        break;
                    case HashAlgorithmType.SHA1:
                        hashAlgorithm = SHA1.Create();
                        break;
                }
                using (hashAlgorithm)
                using (var stream = fileService.OpenFile(File.SelectedPath))
                    algorithm.ValueResult = BitConverter.ToString(hashAlgorithm.ComputeHash(stream)).Replace("-", string.Empty).ToLower();
            }
        }
    }
}