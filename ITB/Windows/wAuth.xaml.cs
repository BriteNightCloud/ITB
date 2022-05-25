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
using ITB.DB;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var i in AppData.DB.Workers)
                cbUser.Items.Add(i.Login);

            if (cbUser.Items.Count > 0)
                cbUser.SelectedIndex = 0;
        }

        private void Window_Drag(object s, MouseButtonEventArgs e) => DragMove();

        private void Exit_Click(object s, RoutedEventArgs e) => Close();

        private void tbPassword_TextChanged(object sender, TextChangedEventArgs e) => pbPassword.Password = tbPassword.Text;

        private void ViewPass(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Visibility == Visibility.Collapsed)
            {
                tbPassword.Text = pbPassword.Password;
                tbPassword.Visibility = Visibility.Visible;
                pbPassword.Visibility = Visibility.Collapsed;
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(175, 175, 175));
            }
            else
            {
                pbPassword.Password = tbPassword.Text;
                tbPassword.Visibility = Visibility.Collapsed;
                pbPassword.Visibility = Visibility.Visible;
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(234, 234, 234));
            }    
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if(AppData.DB.Workers.ToList().Where(c => c.Login == cbUser.Text && c.Password == pbPassword.Password).Count() > 0)
            {
                Window w = new wMain();
                w.Show();
                Close();
            }
            else MessageBox.Show("Неверное имя пользователя или пароль!", "Ошибка при входе", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SignIn_Click(sender, e);
        }
    }
}
