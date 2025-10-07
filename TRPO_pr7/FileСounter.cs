namespace TRPO_pr7
{
    internal class FileСounter : BaseViewModel
    {
        private string _totalFiles;
        public string TotalFiles
        {
            get => _totalFiles;
            set
            {
                _totalFiles = value;
                OnPropertyChanged();
            }
        }
    }
}
