using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TAM2.Models;
using TestView.ViewModels;

namespace TestView
{
    /// <summary>
    /// Index.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Index : Page
    {
        private AttendanceViewModel viewModel = new();


        public Index()
        {
            InitializeComponent();
            SetViewModel();

            DataContext = viewModel;
        }


        private void SetViewModel()
        {
            viewModel.Attendances = Task.Run(MainWindow.Api.Get<ObservableCollection<Attendance>>)?.Result ?? [];
            viewModel.Quantity = $"{viewModel.Attendances.Count} {(viewModel.Attendances.Count <= 1 ? "attendance" : "attendances")}";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var id = viewModel.Attendances.Count > 0 ? viewModel.Attendances.Max(e => e.Id) + 1 : 1;

            NavigationService.Navigate(new Add(id));
        }

        private static int GetIndexOfDataGrid(object sender)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    return row.GetIndex();
                }
            return -1;
        }

        private int GetItemId(object sender)
        {
            var index = GetIndexOfDataGrid(sender);
            return index != -1 ? viewModel.Attendances[index].Id : throw new ArgumentException("Can't Find Item");
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var id = GetItemId(sender);

            NavigationService.Navigate(new Details(id));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var id = GetItemId(sender);

            NavigationService.Navigate(new Edit(id));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var id = GetItemId(sender);

            if (id > 0)
            {
                Task.Run(() => MainWindow.Api.Delete(id));
                NavigationService.Navigate(new Index());
            }
        }
    }
}
