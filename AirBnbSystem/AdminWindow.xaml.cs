using AirBnbSystem.Airbnb.Pages;
using System.Windows;
using System.Windows.Controls;

namespace AirBnbSystem
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private MainWindow mainWindow;

        public AdminWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            contentFrame.Content = new SearchPage(this);
        }

        private void DragBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }

        public void ChangeFrame(Page page)
        {
            contentFrame.Content = page;
        }
    }
}