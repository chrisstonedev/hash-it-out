using HashItOut.Models;
using HashItOut.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;

namespace HashItOut.ViewModels
{
    public class HashViewModel
    {
        private readonly IFileService fileService;

        public ObservableCollection<AlgorithmModel> Algorithms { get; private set; }
            = new ObservableCollection<AlgorithmModel>
            {
                new AlgorithmModel(HashAlgorithmType.MD5),
                new AlgorithmModel(HashAlgorithmType.SHA1)
            };

        public HashViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            BrowseCommand = new RelayCommand(BrowseForFile);
        }

        public FileModel File { get; set; } = new FileModel();

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

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
                    algorithm.ValueResult = BitConverter.ToString(hashAlgorithm.ComputeHash(stream)).Replace("-", string.Empty);
            }
        }
    }
}