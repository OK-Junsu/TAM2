using System.Windows;
using System.Windows.Controls;
using TAM2.Models;
using TestView.ViewModels;

namespace TestView
{
    /// <summary>
    /// Details.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Details : Page
    {
        private readonly DetailViewModel viewModel = new();
        private readonly int id;


        public Details(int id)
        {
            InitializeComponent();

            this.id = id;
            SetViewModel();

            DataContext = viewModel;
        }


        private void SetViewModel()
        {
            var item = Task.Run(() => MainWindow.Api.Get<Attendance>(id))?.Result ?? new();

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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Edit(id));
        }
    }
}
