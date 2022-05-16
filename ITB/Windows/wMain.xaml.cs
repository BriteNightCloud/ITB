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
            frmMain.Navigate(new Page() { Content = new TextBlock() { Text = "Потыкайте по меню слева...", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 32 } });
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            wModal w = new wModal(frmMain);
            w.Owner = this;
            w.Left = (double)typeof(Window).GetField("_actualLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) + WidthPoint.ActualWidth;
            w.Top = (double)typeof(Window).GetField("_actualTop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) + 35;
            w.Width = this.ActualWidth - WidthPoint.ActualWidth - 15;
            w.Height = this.ActualHeight - 50;
            w.Loaded += (object s, RoutedEventArgs ea) => ((Button)sender).Background = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
            w.Closed += (object s, EventArgs ea) => ((Button)sender).Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            switch (((Button)sender).Name)
            {
                case "General":
                    frmMain.Navigate(new Page() { Content = new TextBlock() { Text = "Потыкайте по меню слева...", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 32 } });
                    break;
                case "Sales":
                    w.AddCategory("Типа название категории");
                    w.AddItem("Владимир Пукин", new pSales());
                    w.AddItem("Джо Бидон", new pSales());
                    w.AddCategory("Второе");
                    w.AddItem("Может по пиву и домой?", new pSales());
                    w.AddCategory("Средней длинны");
                    w.AddItem("коротко", new pSales());
                    w.AddItem("и ясно", new pSales());
                    w.ShowDialog();
                    break;
                case "Clients":
                    w.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
