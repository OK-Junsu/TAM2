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


        public Edit(Attendance item)
        {
            InitializeComponent();

            viewModel = new()
            {
                Id = item.Id.ToString(),
                Name = item.Name?.ToString() ?? "",
                Arrive = item.Arrive.ToString(),
                Leave = item.Leave.ToString()
            };
            DataContext = viewModel;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Index());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new Attendance()
            {
                Id = Convert.ToInt32(Id.Text),
                Name = Name.Text,
                Arrive = Convert.ToDateTime(Arrive.Text),
                Leave = Convert.ToDateTime(Leave.Text)
            };

            NavigationService.Navigate(new Index());
        }
    }
}
