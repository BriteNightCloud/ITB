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
using ITB.Pages;

namespace ITB.Windows
{
    /// <summary>
    /// Логика взаимодействия для wMain.xaml
    /// </summary>
    public partial class wMain : Window
    {
        public wMain()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "General":
                    frmMain.Navigate(new Page());
                    break;
                case "Sales":
                    frmMain.Navigate(new pSales());
                    break;
                default:
                    break;
            }
        }
    }
}
