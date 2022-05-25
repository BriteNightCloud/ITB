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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ITB.Windows;
using ITB.DB;

namespace ITB.Pages
{
    /// <summary>
    /// Логика взаимодействия для pReview.xaml
    /// </summary>
    public partial class pReview : Page
    {
        public pReview(string Title, params DataGridColumn[] Columns)
        {
            InitializeComponent();
            this.Title = Title;
            foreach (var c in Columns)
                DataGridColumn.Add(dgTable, c.Header, c.Binding, c.MinWidth);
            AppData.DB.Clients.Add(new Client() { Name = "Виктория Ивановна", Email = "fsdfs", Phone = "fsdf" });
            AppData.DB.Clients.Add(new Client() { Name = "Глеб Михайлович", Email = "fsdfs", Phone = "fsdf" });
            AppData.DB.Clients.Add(new Client() { Name = "Путин ВВ", Email = "fsdfs", Phone = "fsdf" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 1, Comment = "Test", Cost = 1424.41m, Date = DateTime.Now, Manager_ID = 1, Status = "На согласовании" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 2, Comment = "Тест", Cost = 51283.2m, Date = DateTime.Now, Manager_ID = 1, Status = "В резерве" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 1, Comment = "Чисто рандомный текст", Cost = 1442.5m, Date = DateTime.Now, Manager_ID = 1, Status = "К отгрузке" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 3, Comment = "qwerty", Cost = 12.5m, Date = DateTime.Now, Manager_ID = 1, Status = "Закрытый заказ" });
            AppData.DB.Sales.Add(new Sale() { Client_ID = 3, Comment = "zxc", Cost = 12.51m, Date = DateTime.Now, Manager_ID = 1, Status = "Закрытый заказ" });
            AppData.DB.SaveChanges();
            dgTable.ItemsSource = AppData.DB.Sales.ToList();
            //dgTable.Items.Add(new {ID = "00УТ-004113", Date = "20.05.2022", Cost = "15 000,00", Client = "МОРП", Status = "В процессе отгрузки"});
        }

        private void tbSearch_Clear(object sender, RoutedEventArgs e) => tbSearch.Text = String.Empty;

        enum Actions
        {
            ToBeAgreed = 1,
            InReserve,
            ForShipment,
            CloseOrder
        }

        enum More
        {
            Create = 1,
            Copy,
            Edit,
            Delete,
            Update
        }

        private void DropMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox obj = (ComboBox)sender;
            if (!obj.IsDropDownOpen)
                return;
            if (obj.Name == "cbActions")
                switch ((Actions)obj.SelectedIndex)
                {
                    case Actions.ToBeAgreed:
                        break;
                    case Actions.InReserve:
                        break;
                    case Actions.ForShipment:
                        break;
                    case Actions.CloseOrder:
                        break;
                }
            else if (obj.Name == "cbMore")
                switch ((More)obj.SelectedIndex)
                {
                    case More.Create:
                        break;
                    case More.Copy:
                        break;
                    case More.Edit:
                        break;
                    case More.Delete:
                        break;
                    case More.Update:
                        break;
                }
            obj.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e) => ((wMain)Window.GetWindow(this)).OpenInTab("Заказ клиента (создание)", 
            new pCreate("Заказ клиента (создание)",
                   new DataGridColumn("N", "Index", 25),
                   new DataGridColumn("Номенклатура", "Nomenclature", 220),
                   new DataGridColumn("Количество", "Count", 50),
                   new DataGridColumn("Цена за ед.", "Price", 125),
                   new DataGridColumn("Стоимость", "Cost", 125)));

        //// Обновляет данные в списке.
        //private void GetData()
        //{
        //    // Если какой-либо объект не инициализирован, выходить из функции, чтобы предотвратить ошибки
        //    if (tbSearch == null || dgTable == null || cbClient == null || cbManager == null || cbStatus == null)
        //        return;

        //    // Метод Where() ругается, если эту строку напрямую в него запихнуть,
        //    // поэтому создаю отдельную переменную для этого
        //    int filter1 = (cbClient.SelectedItem).ID;
        //    int filter2 = (cbManager.SelectedItem).ID;
        //    string filter3 = cbStatus?.SelectedItem?.ToString();

        //    // Получаем данные, соответствующие нашим критериям
        //    dgTable.ItemsSource = AppData.DB.Sales.Where(c =>
        //        tbSearch.Text != "Введите для поиска" && tbSearch.Text != String.Empty ?
        //            c.Comment.ToLower().Contains(tbSearch.Text.ToLower()) : true &&
        //        cbClient.SelectedIndex > 1 ?
        //            c.Client_ID == filter1 : true &&
        //        cbManager.SelectedIndex > 1 ?
        //            c.Manager_ID == filter2 : true &&
        //        cbStatus.SelectedIndex > 1 ?
        //            c.Status == filter3 : true
        //    ).ToList();
        //}
    }
}