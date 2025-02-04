using System.Windows;
using System.Windows.Controls;
using TAM2.Models;
using TestView.ViewModels;

namespace TestView
{
    /// <summary>
    /// Edit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Edit : Page
    {
        private readonly bool isInitialized;
        private readonly DetailViewModel viewModel = new();
        private readonly int id;


        public Edit(int id)
        {
            InitializeComponent();

            StateComboBox.ItemsSource = DayState.UserSelectables();

            this.id = id;
            SetViewModel();

            DataContext = viewModel;
            isInitialized = true;
        }


        private Attendance GetItem() => Task.Run(() => MainWindow.Api.Get<Attendance>(id))?.Result ?? new();

        private void SetViewModel()
        {
            var item = GetItem();

            viewModel.Id = item.Id.ToString();
            viewModel.UserId = item.UserId.ToString();
            viewModel.Date = item.Date.ToString("yyyy-MM-dd");
            viewModel.Time = item.Time?.ToString("HH:mm") ?? string.Empty;
            viewModel.State = item.State.ToString();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Index());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private async void Save()
        {
            var item = GetItem();

            if (!item.IsDeleted)
            {
                item.UserId = Convert.ToInt32(UserId.Text);
                item.Date = DateOnly.Parse(Date.Text);
                item.Time = string.IsNullOrEmpty(Time.Text) ? null : TimeOnly.Parse(Time.Text);
                item.State = StateComboBox.SelectedValue?.ToString() ?? string.Empty;

                await MainWindow.Api.Update(item, id);
            }
            else
                MessageBox.Show("Can Not Save Changes" + "\r\n" +
                                "This Item is Already Deleted",
                                "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning,
                                MessageBoxResult.OK);

            NavigationService.Navigate(new Index());
        }

        private void StateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized) return;

            var selected = StateComboBox.SelectedValue?.ToString();
            if (!string.IsNullOrEmpty(selected))
            {
                var state = new DayState(selected).Value;

                viewModel.Time = state switch
                {
                    DayStateType.Arrive or
                    DayStateType.Leave => DateTime.Now.ToString("HH:mm"),
                    _ => string.Empty,
                };
                Time.IsReadOnly = state switch
                {
                    DayStateType.Arrive or
                    DayStateType.Leave => false,
                    _ => true,
                };
            }
        }
    }
}
