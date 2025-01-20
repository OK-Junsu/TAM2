namespace TestView.ViewModels
{
    public class DetailViewModel() : Notifier
    {
        public string Id
        {
            get => _Id;
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _Id;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _Name;
        public string Arrive
        {
            get => _Arrive;
            set
            {
                _Arrive = value;
                OnPropertyChanged(nameof(Arrive));
            }
        }
        private string _Arrive;
        public string Leave
        {
            get => _Leave;
            set
            {
                _Leave = value;
                OnPropertyChanged(nameof(Leave));
            }
        }
        private string _Leave;
    }
}
