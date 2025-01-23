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
                UserId = item.UserId.ToString(),
                Date = item.Date.ToString(),
                Time = item.Time.ToString(),
                State = item.State.ToString()
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
                UserId = Convert.ToInt32(UserId.Text),
                Date = DateOnly.Parse(Date.Text),
                Time = TimeOnly.Parse(Time.Text),
                State = State.Text
            };

            await MainWindow.Api.Update(newItem, id);

            NavigationService.Navigate(new Index());
        }
    }
}
