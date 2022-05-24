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

            spMenu.Children.Add(new Button() { Name = "General", Content = "Главная" });
            spMenu.Children.Add(new Button() { Name = "Sales", Content = "Продажи" });
            spMenu.Children.Add(new Button() { Name = "Administration", Content = "Администрирование" });

            foreach (Button elem in spMenu.Children)
            {
                elem.Click += Menu_Click;
                elem.Style = FindResource("MainLeftButton") as Style;
            }

            OpenInTab("Главная", new pGeneral());
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            wModal w = new wModal(frmMain, lvTabs, spMenu);
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
                    OpenInTab("Главная", new pGeneral());
                    break;
                case "Sales":
                    w.AddCategory("Продажи");
                    w.AddItem("Заказы клиентов", 
                        new pReview("Заказы клиентов",
                        new DataGridColumn("Номер", "ID", 100),
                        new DataGridColumn("Дата", "Date", 80),
                        new DataGridColumn("Сумма", "Cost", 75),
                        new DataGridColumn("Клиент", "Client", 175),
                        new DataGridColumn("Текущее состояние", "Status", 150)));
                    w.AddItem("Счета на оплату", new Page());
                    w.AddCategory("Создать");
                    w.AddItem("Заказ клиента",
                        new pCreate("Заказ клиента (создание)",
                        new DataGridColumn("N", "Index", 25),
                        new DataGridColumn("Номенклатура", "Nomenclature", 220),
                        new DataGridColumn("Количество", "Count", 50),
                        new DataGridColumn("Цена за ед.", "Price", 125),
                        new DataGridColumn("Стоимость", "Cost", 125)));
                    w.ShowDialog();
                    break;
                case "Administration":
                    w.AddCategory("НСИ");
                    w.AddItem("Номенклатуры", new pAdministration("Номенклатуры", 
                        new DataGridColumn("ID", "ID",30),
                        new DataGridColumn("Наименование", "Name",220),
                        new DataGridColumn("Цена за 1", "Price",150)));
                    w.AddItem("Клиенты", new pAdministration("Клиенты",
                        new DataGridColumn("ID", "ID",30),
                        new DataGridColumn("Наименование", "Name",220),
                        new DataGridColumn("Телефон", "Phone",150),
                        new DataGridColumn("Почта", "Email",175)));
                    w.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        // overcoder.net/q/2679817/wpf-как-определить-индекс-текущего-элемента-в-списке-из-обработчика-кнопки
        private void btnTabClose_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = lvTabs.ItemContainerGenerator.IndexFromContainer(dep);

            lvTabs.Items.RemoveAt(index);

            if (lvTabs.SelectedIndex == -1)
                lvTabs.SelectedIndex = lvTabs.Items.Count - 1;
        }

        public void OpenInTab(string ItemTitle, Page ItemPage)
        {
            lvTabs.Items.Add(new { Content = ItemTitle, page = ItemPage });
            lvTabs.UnselectAll();
            lvTabs.SelectedIndex = lvTabs.Items.Count - 1;
            frmMain.Navigate(ItemPage);
        }

        private void lvTabs_SelectionChanged(object sender, SelectionChangedEventArgs e) => frmMain.Navigate(lvTabs.SelectedItem?.GetType()?.GetProperty("page")?.GetValue(lvTabs.SelectedItem, null));
    }
}