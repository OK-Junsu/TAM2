namespace TestView.ViewModels
{
    public class DetailViewModel : Notifier
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

        public string UserId
        {
            get => _UserId;
            set
            {
                _UserId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private string _UserId;

        public string Date
        {
            get => _Date;
            set
            {
                _Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        private string _Date;

        public string Time
        {
            get => _Time;
            set
            {
                _Time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        private string _Time;

        public string State
        {
            get => _State;
            set
            {
                _State = value;
                OnPropertyChanged(nameof(State));
            }
        }
        private string _State;
    }
}
