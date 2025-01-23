using System.Windows;

namespace TestView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static ApiHelper Api;
        private const string uri = "https://localhost:7297";
        private const string apiRoot = "api/Attendances";


        public MainWindow()
        {
            InitializeComponent();

            Api = new(uri, apiRoot);
            frame.Content = new Index();
        }
    }
}