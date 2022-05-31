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
        public pReview(string Title)
        {
            InitializeComponent();
            this.Title = Title;

            DataGridColumn[] Columns = {
                new DataGridColumn("Номер", "ID", 40),
                new DataGridColumn("Дата", "Date", 80),
                new DataGridColumn("Сумма", "Cost", 60),
                new DataGridColumn("Клиент", "Client_Name", 175),
                new DataGridColumn("Текущее состояние", "Status", 125)
            };
            foreach (var c in Columns)
                DataGridColumn.Add(dgTable, c.Header, c.Binding, c.MinWidth);

            //dgTable.Items.Add(new {ID = "00УТ-004113", Date = "20.05.2022", Cost = "15 000,00", Client = "МОРП", Status = "В процессе отгрузки"});
        }

        private void pLoaded(object sender, RoutedEventArgs e)
        {
            GetData();

            cbClient.ItemsSource = AppData.DB.Clients.ToList();
            cbManager.ItemsSource = AppData.DB.Workers.ToList();
        }

        private void tbSearch_Clear(object sender, RoutedEventArgs e) => tbSearch.Text = "Введите для поиска";

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
            if (obj.Name == "cbActions" && dgTable.SelectedIndex != -1)
            {
                AppData.DB.Database.ExecuteSqlCommand("UPDATE Sales SET Status = '" + ((ComboBoxItem)cbActions.SelectedValue).Content + 
                    "' WHERE ID = " + ((Sale)dgTable.SelectedItem).ID.ToString());
                AppData.DB.SaveChanges();
                GetData();
            }
            else if (obj.Name == "cbMore")
                switch ((More)obj.SelectedIndex)
                {
                    case More.Create:
                        btnCreate_Click(sender, e);
                        break;
                    case More.Copy:
                        if (dgTable.SelectedIndex == -1)
                            goto DropMenuReturn;
                        ((wMain)Window.GetWindow(this)).OpenInTab("Заказ клиента (создание копии)",
                            new pCreate("Заказ клиента (создание копии)", (Sale)dgTable.SelectedItem, isEdit: false));
                        break;
                    case More.Edit:
                        if (dgTable.SelectedIndex == -1)
                            goto DropMenuReturn;
                        ((wMain)Window.GetWindow(this)).OpenInTab("Заказ клиента (редактирование)",
                            new pCreate("Заказ клиента (редактирование)", (Sale)dgTable.SelectedItem, isEdit: true));
                        break;
                    case More.Delete:
                        if (dgTable.SelectedIndex == -1)
                            goto DropMenuReturn;
                        AppData.DB.Database.ExecuteSqlCommand("DELETE FROM Sale_Details WHERE Sales_ID = " + dgTable.SelectedItem?.GetType()?.GetProperty("ID")?.GetValue(dgTable.SelectedItem, null).ToString());
                        AppData.DB.Database.ExecuteSqlCommand("DELETE FROM Sales WHERE ID = " + dgTable.SelectedItem?.GetType()?.GetProperty("ID")?.GetValue(dgTable.SelectedItem, null).ToString());
                        AppData.DB.SaveChanges();
                        goto case More.Update;
                    case More.Update:
                        GetData();
                        break;
                }
            DropMenuReturn:
            obj.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e) => ((wMain)Window.GetWindow(this)).OpenInTab("Заказ клиента (создание)", 
            new pCreate("Заказ клиента (создание)"));

        // Обновляет данные в списке.
        private void GetData()
        {
            // Если какой-либо объект не инициализирован, выходить из функции, чтобы предотвратить ошибки
            if (tbSearch == null || dgTable == null || cbClient == null || cbManager == null || cbStatus == null)
                return;

            // Длинный и "сложный" (на самом деле нет) SQL запрос
            dgTable.ItemsSource = AppData.DB.Database.SqlQuery<Sale>("SELECT * FROM Sales WHERE ID>0" +
                (tbSearch.Text != "Введите для поиска" && tbSearch.Text != String.Empty ?
                    " AND CHARINDEX('" + tbSearch.Text + "', Comment) > 0" : String.Empty) +
                (cbClient.SelectedIndex > -1 ?
                    " AND Client_ID=" + ((Client)cbClient.SelectedItem).ID.ToString() : String.Empty) +
                (cbManager.SelectedIndex > -1 ?
                    " AND Manager_ID=" + ((Worker)cbManager.SelectedItem).ID.ToString() : String.Empty) +
                (cbStatus.SelectedIndex > -1 ?
                    " AND Status='" + ((ComboBoxItem)cbStatus.SelectedValue).Content + "'" : "")
            ).ToList();
        }

        private void GetData(object sender, EventArgs e) => GetData();

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == "Введите для поиска")
                tbSearch.Text = String.Empty;
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                tbSearch.Text = "Введите для поиска";
        }
    }
}