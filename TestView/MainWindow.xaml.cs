using System.Windows;

namespace TestView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string uri = "https://localhost:7297";
        private const string apiRoot = "api/Attendances";
        internal static ApiHelper Api = new(uri, apiRoot);
        internal static bool IsIncludeDeletedItems { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            IsIncludeDeletedItems = true;
            frame.Content = new Index();
        }
    }
}