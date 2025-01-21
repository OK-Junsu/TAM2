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
        private DetailViewModel viewModel;


        public Details(int id)
        {
            InitializeComponent();

            var json = APIFunctions.Get(Index.targetURL + id);
            var item = APIFunctions.JsonDeserialize<Attendance>(json, true);

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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = new Attendance()
            {
                Id = Convert.ToInt32(Id.Text),
                Name = Name.Text,
                Arrive = Convert.ToDateTime(Arrive.Text),
                Leave = Convert.ToDateTime(Leave.Text)
            };

            NavigationService.Navigate(new Edit(item));
        }
    }
}
