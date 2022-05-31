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
using ITB.DB;

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

            AppData.DB.Nomenclatures.Add(new Nomenclature() { Name = "Test1", Price = 20 });
            AppData.DB.Nomenclatures.Add(new Nomenclature() { Name = "Test2", Price = 125 });
            AppData.DB.Nomenclatures.Add(new Nomenclature() { Name = "Test3", Price = 15 });
            AppData.DB.Nomenclatures.Add(new Nomenclature() { Name = "Test4", Price = 252.1m });
            AppData.DB.Clients.Add(new Client() { Name = "Виктория Ивановна", Email = "qwe", Phone = "ewq" });
            AppData.DB.Clients.Add(new Client() { Name = "Глеб Михайлович", Email = "asd", Phone = "dsa" });
            AppData.DB.Clients.Add(new Client() { Name = "Виктор", Email = "dsa", Phone = "asd" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 1, Comment = "Test", Cost = 1424.41m, Date = DateTime.Now, Manager_ID = 1, Status = "На согласовании" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 2, Comment = "Тест", Cost = 51283.2m, Date = DateTime.Now, Manager_ID = 1, Status = "На согласовании" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 1, Comment = "Чисто рандомный текст", Cost = 1442.5m, Date = DateTime.Now, Manager_ID = 1, Status = "К отгрузке" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 3, Comment = "qwerty", Cost = 12.5m, Date = DateTime.Now, Manager_ID = 1, Status = "Закрытый заказ" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 3, Comment = "zxc", Cost = 12.51m, Date = DateTime.Now, Manager_ID = 1, Status = "Закрытый заказ" });
            AppData.DB.SaveChanges();
            AppData.DB.Sale_Details.Add(new Sale_Details() { Index = 1, Nomenclature_ID = 1, Price = 20, Sales_ID = 1, Count = 14 });
            AppData.DB.Sale_Details.Add(new Sale_Details() { Index = 2, Nomenclature_ID = 2, Price = 125, Sales_ID = 1, Count = 14 });
            AppData.DB.Sale_Details.Add(new Sale_Details() { Index = 3, Nomenclature_ID = 3, Price = 15, Sales_ID = 1, Count = 14 });
            AppData.DB.SaveChanges();
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
                        new pReview("Заказы клиентов"));
                    w.AddItem("Счета на оплату", new Page());
                    w.AddCategory("Создать");
                    w.AddItem("Заказ клиента",
                        new pCreate("Заказ клиента (создание)"));
                    w.ShowDialog();
                    break;
                case "Administration":
                    w.AddCategory("НСИ");
                    w.AddItem("Номенклатуры", new pAdministration("Номенклатуры", "Nomenclature",
                        new DataGridColumn("ID", "ID",30),
                        new DataGridColumn("Наименование", "Name",220),
                        new DataGridColumn("Цена за 1", "Price",150)));
                    w.AddItem("Клиенты", new pAdministration("Клиенты", "Client",
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