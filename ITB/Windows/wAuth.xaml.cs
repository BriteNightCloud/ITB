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

namespace ITB.Windows
{
    /// <summary>
    /// Логика взаимодействия для wAuth.xaml
    /// </summary>
    public partial class wAuth : Window
    {
        public wAuth()
        {
            InitializeComponent();
        }

        private void Window_Drag(object s, MouseButtonEventArgs e) => DragMove();

        private void Exit_Click(object s, RoutedEventArgs e) => Close();

        private void ViewPass(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Visibility == Visibility.Collapsed)
            {
                tbPassword.Text = pbPassword.Password;
                tbPassword.Visibility = Visibility.Visible;
                pbPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                pbPassword.Password = tbPassword.Text;
                tbPassword.Visibility = Visibility.Collapsed;
                pbPassword.Visibility = Visibility.Visible;
            }    
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if(true)
            {
                Window w = new wMain();
                w.Show();
                Close();
            }
        }
    }
}
