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
        private DetailViewModel viewModel;
        private readonly int id;


        public Edit(int id)
        {
            InitializeComponent();

            this.id = id;
            SetViewModel();

            DataContext = viewModel;
        }


        private void SetViewModel()
        {
            var item = Task.Run(() => MainWindow.Api.Get<Attendance>(id))?.Result ?? new();

            viewModel = new()
            {
                Id = item.Id.ToString(),
                Name = item.Name?.ToString() ?? string.Empty,
                Arrive = item.Arrive.ToString() ?? DateTime.MinValue.ToString(),
                Leave = item.Leave.ToString() ?? DateTime.MinValue.ToString()
            };
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
            var newItem = new Attendance()
            {
                Id = id,
                Name = Name.Text,
                Arrive = Convert.ToDateTime(Arrive.Text),
                Leave = Convert.ToDateTime(Leave.Text)
            };

            await MainWindow.Api.Update(newItem, id);

            NavigationService.Navigate(new Index());
        }
    }
}
