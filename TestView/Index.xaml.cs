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
        public const string targetURL = "https://localhost:7297/api/Attendances/";

        private AttendanceViewModel? viewModel { get; set; } = new();

#nullable disable


        public Index()
        {
            InitializeComponent();

            SetItemList();
            SetQuantity();

            DataContext = viewModel;
        }


        private void SetItemList()
        {
            var jsonResult = APIFunctions.Get(targetURL);
            viewModel.Attendances = APIFunctions.JsonDeserialize<ObservableCollection<Attendance>>(jsonResult, true);
        }

        private void SetQuantity()
        {
            var additional = viewModel.Attendances.Count <= 1 ? "attendance" : "attendances";
            viewModel.Quantity = $"{viewModel.Attendances.Count} {additional}";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var id = viewModel.Attendances.Count > 0 ? viewModel.Attendances.Max(e => e.Id) + 1 : 1;

            NavigationService.Navigate(new Add(id));
        }

        private int GetIndexOfDataGrid(object sender)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    return row.GetIndex();
                }
            return -1;
        }
        private Attendance GetItem(object sender)
        {
            var index = GetIndexOfDataGrid(sender);
            return index != -1 ? viewModel.Attendances[index] : throw new ArgumentException("Can't Find Item");
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
            var item = GetItem(sender);

            NavigationService.Navigate(new Edit(item));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var id = GetItemId(sender);

            if (id > 0)
            {
                APIFunctions.Delete(targetURL + id);
                NavigationService.Navigate(new Index());
            }
        }
    }
}
