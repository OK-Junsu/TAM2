using System.Windows;
using System.Windows.Controls;
using TAM2.Models;
using TestView.ViewModels;

namespace TestView
{
    /// <summary>
    /// Add.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Add : Page
    {
        private readonly bool isInitialized;
        private readonly DetailViewModel viewModel = new();
        private readonly int id;


        public Add(int id)
        {
            InitializeComponent();

            StateComboBox.ItemsSource = DayState.UserSelectables();

            this.id = id;
            SetViewModel();

            DataContext = viewModel;
            isInitialized = true;
        }


        private void SetViewModel()
        {
            viewModel.Date = DateTime.Now.ToString("yyyy-MM-dd");
            viewModel.Time = DateTime.Now.ToString("HH:mm");
            viewModel.State = DayStateType.Arrive.ToString();
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
            if (string.IsNullOrEmpty(UserId.Text) || Convert.ToInt32(UserId.Text) <= 0)
            {
                MessageBox.Show("Must Enter User ID",
                                "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var newItem = new Attendance()
            {
                Id = id,
                UserId = Convert.ToInt32(UserId.Text),
                Date = DateOnly.Parse(Date.Text),
                Time = string.IsNullOrEmpty(Time.Text) ? null : TimeOnly.Parse(Time.Text),
                State = StateComboBox.SelectedValue?.ToString() ?? string.Empty,
            };

            newItem.Register();

            await MainWindow.Api.Create(newItem);

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
