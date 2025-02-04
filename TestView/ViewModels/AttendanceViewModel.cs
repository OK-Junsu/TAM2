using System.Collections.ObjectModel;
using TAM2.Models;

namespace TestView.ViewModels
{
    public class AttendanceViewModel : Notifier
    {
        public bool IsIncludeDeletedItems
        {
            get => _IsIncludeDeletedItems;
            set
            {
                _IsIncludeDeletedItems = value;
                OnPropertyChanged(nameof(IsIncludeDeletedItems));
                MainWindow.IsIncludeDeletedItems = value;
            }
        }
        private bool _IsIncludeDeletedItems;
        
        public string Quantity
        {
            get => _Quantity;
            set
            {
                _Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        private string _Quantity = default!;
        
        public ObservableCollection<DetailViewModel> Attendances
        {
            get => _Attendances;
            set
            {
                _Attendances = value;
                OnPropertyChanged(nameof(Attendances));
            }
        }
        private ObservableCollection<DetailViewModel> _Attendances = [];
    }
}
