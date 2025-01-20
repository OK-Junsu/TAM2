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
        private AttendanceViewModel? viewModel { get; set; }


        public Index()
        {
            InitializeComponent();
            viewModel = this.DataContext as AttendanceViewModel;

            SetQuantity();
        }


        private void SetQuantity()
        {
            var additional = viewModel.Attendances.Count <= 1 ? "attendance" : "attendances";
            viewModel.Quantity = $"{viewModel.Attendances.Count} {additional}";
        }

        private void AutoAdd_Click(object sender, RoutedEventArgs e)
        {
            var id = viewModel.Attendances.Count > 0 ? viewModel.Attendances.Max(e => e.Id) + 1 : 1;

            viewModel.Attendances.Add(new Attendance
            {
                Id = id,
                Name = $"Test_{id}",
                Arrive = DateTime.Now,
                Leave = DateTime.Now
            });

            SetQuantity();
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
            return index != -1 ? viewModel.Attendances[index] : null;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var item = GetItem(sender);

            NavigationService.Navigate(new Details(item));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = GetItem(sender);

            NavigationService.Navigate(new Edit(item));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var index = GetIndexOfDataGrid(sender);

            if (index != -1)
            {
                viewModel.Attendances.RemoveAt(index);
                SetQuantity();
            }
        }
    }
}
