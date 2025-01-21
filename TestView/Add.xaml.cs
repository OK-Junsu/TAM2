using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        private int newId;


        public Add(int newId)
        {
            InitializeComponent();
            viewModel = this.DataContext as DetailViewModel;

            this.newId = newId;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Index());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new Attendance()
            {
                Id = newId,
                Name = Name.Text,
                Arrive = Convert.ToDateTime(Arrive.Text),
                Leave = Convert.ToDateTime(Leave.Text)
            };

            var data = APIFunctions.JsonSerialize(newItem);
            APIFunctions.Post(Index.targetURL, data);

            NavigationService.Navigate(new Index());
        }
    }
}
