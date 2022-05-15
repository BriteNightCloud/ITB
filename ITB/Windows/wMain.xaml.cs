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
            Window w = new wModal();
            w.Owner = this;
            w.Left = (double)typeof(Window).GetField("_actualLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) + WidthPoint.ActualWidth;
            w.Top = (double)typeof(Window).GetField("_actualTop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) + 35;
            w.Width = this.ActualWidth - WidthPoint.ActualWidth - 15;
            w.Height = this.ActualHeight - 50;
            switch (((Button)sender).Name)
            {
                case "General":
                    break;
                case "Sales":
                    break;
                case "Clients":
                    break;
                default:
                    break;
            }
            w.ShowDialog();
        }
    }
}
