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
        private DetailViewModel viewModel;
        private readonly int id;


        public Add(int id)
        {
            InitializeComponent();

            this.id = id;
            SetViewModel();

            DataContext = viewModel;
        }


        private void SetViewModel()
        {
            viewModel = new()
            {
                Arrive = DateTime.Now.ToString(),
                Leave = DateTime.Now.ToString()
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

            await MainWindow.Api.Create(newItem);

            NavigationService.Navigate(new Index());
        }
    }
}
