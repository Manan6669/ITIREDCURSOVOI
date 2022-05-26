using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursovoi
{
    /// <summary>
    /// Логика взаимодействия для WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
       
        public WindowMain()
        {
            InitializeComponent();
            
          
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            var window = Application.Current.Windows[0];
            if (window != null)
                window.Close();
        }

        private void UserPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("UserPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Catalog.xaml", UriKind.Relative));
        }
      
    }
}
