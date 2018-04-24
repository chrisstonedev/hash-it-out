using System.ComponentModel;
using System.Windows.Media;

namespace HashItOut.Models
{
    /// <summary>
    /// Represents a hashing algorithm and stores values related to processing.
    /// </summary>
    public class AlgorithmModel : INotifyPropertyChanged
    {
        private string input;
        private string valueResult;
        private HashAlgorithmType hashAlgorithmType;

        /// <summary>
        /// Initializes an instance of the <see cref="AlgorithmModel"/> class.
        /// </summary>
        /// <param name="hashAlgorithmType">The type of algorithm represented by this instance.</param>
        public AlgorithmModel(HashAlgorithmType hashAlgorithmType)
        {
            this.hashAlgorithmType = hashAlgorithmType;
        }

        /// <summary>
        /// Gets or sets text entered by the user as the expected value for an algorithm output to match.
        /// </summary>
        public string Input
        {
            get => input;
            set
            {
                if (input != value)
                {
                    input = value;
                    RaisePropertyChanged("Input");
                    RaisePropertyChanged("CompareResult");
                    RaisePropertyChanged("CompareColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the resulting value of a hashing algorithm.
        /// </summary>
        public string ValueResult
        {
            get => valueResult;
            set
            {
                if (valueResult != value)
                {
                    valueResult = value;
                    RaisePropertyChanged("ValueResult");
                    RaisePropertyChanged("CompareResult");
                    RaisePropertyChanged("CompareColor");
                }
            }
        }

        /// <summary>
        /// Gets the result of a comparison check between the value of a hash function and a user's expected value.
        /// </summary>
        public string CompareResult
        {
            get
            {
                if (string.IsNullOrWhiteSpace(input))
                    return string.Empty;

                return input.Trim().ToUpper() == valueResult.Trim().ToUpper() ? "SUCCEEDED" : "FAILED";
            }
        }

        /// <summary>
        /// Gets the color to use to paint the compare result, indicating success or failure.
        /// </summary>
        public Brush CompareColor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(input))
                    return Brushes.Black;

                return input.Trim().ToUpper() == valueResult.Trim().ToUpper() ? Brushes.DarkGreen : Brushes.DarkRed;
            }
        }

        /// <summary>
        /// Gets the name of the function represented by this object.
        /// </summary>
        public string Function
        {
            get
            {
                switch (hashAlgorithmType)
                {
                    case HashAlgorithmType.MD5:
                        return "MD5";
                    case HashAlgorithmType.SHA1:
                        return "SHA-1";
                    default:
                        return "Unknown";
                }
            }
        }

        /// <summary>
        /// Gets the underlying type of the algorithm.
        /// </summary>
        public HashAlgorithmType Type => hashAlgorithmType;

        /// <summary>
        /// Handles cases in which a property value has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
