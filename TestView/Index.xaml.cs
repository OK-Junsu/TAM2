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
        private readonly bool isInitialized;
        private readonly AttendanceViewModel viewModel = new();


        public Index()
        {
            InitializeComponent();
            viewModel.IsIncludeDeletedItems = MainWindow.IsIncludeDeletedItems;
            SetViewModel();

            DataContext = viewModel;
            isInitialized = true;
        }


        private static List<Attendance> GetItems() => Task.Run(MainWindow.Api.Get<List<Attendance>>)?.Result ?? [];
        private static Attendance GetItem(int id) => Task.Run(() => MainWindow.Api.Get<Attendance>(id))?.Result ?? new();

        private void SetViewModel()
        {
            if (!(viewModel.Attendances?.Any() ?? false)) viewModel.Attendances = [];

            viewModel.Attendances.Clear();
            GetItems().ForEach(e =>
            {
                if (MainWindow.IsIncludeDeletedItems)
                    SetAttendances(e);
                else
                {
                    if (!e.IsDeleted)
                        SetAttendances(e);
                }
            });

            var count = viewModel.Attendances.Count(e => !e.IsDeleted);
            viewModel.Quantity = $"{count} {(count <= 1 ? "attendance" : "attendances")}";
        }

        private void SetAttendances(Attendance e)
        {
            viewModel.Attendances.Add(new()
            {
                Id = e.Id.ToString(),
                UserId = e.UserId.ToString(),
                Date = e.Date.ToString("yyyy-MM-dd"),
                Time = e.Time?.ToString("HH:mm") ?? string.Empty,
                State = e.State,
                IsDeleted = e.IsDeleted
            });
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var items = Task.Run(MainWindow.Api.Get<List<Attendance>>)?.Result;
            var id = items?.Count > 0 ? items.Max(e => e.Id) + 1 : 1;

            NavigationService.Navigate(new Add(id));
        }

        private static int GetIndexOfDataGrid(object sender)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                    return row.GetIndex();
            return -1;
        }

        private int GetItemId(object sender)
        {
            var index = GetIndexOfDataGrid(sender);
            return index != -1 ? Convert.ToInt32(viewModel.Attendances[index].Id) : throw new ArgumentException("Can't Find Item");
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

            Delete(id);
        }

        private void DeletePermanently_Click(object sender, RoutedEventArgs e)
        {
            var id = GetItemId(sender);

            DeletePermanently(id);
        }

        private async void Delete(int id)
        {
            if (id > 0)
            {
                var item = GetItem(id);

                item.Delete();

                await MainWindow.Api.Update(item, id);

                NavigationService.Navigate(new Index());
            }
        }

        private async void DeletePermanently(int id)
        {
            if (id > 0)
            {
                await MainWindow.Api.Delete(id);

                NavigationService.Navigate(new Index());
            }
        }

        private void IncludeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (isInitialized) SetViewModel();
        }

        private void IncludeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (isInitialized) SetViewModel();
        }
    }
}
