using System.Collections.ObjectModel;
using TAM2.Models;

namespace TestView.ViewModels
{
    public class AttendanceViewModel() : Notifier
    {
        public string Quantity
        {
            get => _Quantity;
            set
            {
                _Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        private string _Quantity;
        
        public ObservableCollection<Attendance> Attendances
        {
            get => _Attendances;
            set
            {
                _Attendances = value;
                OnPropertyChanged(nameof(Attendances));
            }
        }
        private ObservableCollection<Attendance> _Attendances;
    }
}
