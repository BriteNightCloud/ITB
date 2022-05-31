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
using ITB.DB;

namespace ITB.Pages
{
    /// <summary>
    /// Логика взаимодействия для pAdministration.xaml
    /// </summary>
    public partial class pAdministration : Page
    {
        public pAdministration(string Title, string TableName, params DataGridColumn[] Columns)
        {
            InitializeComponent();
            this.Title = Title;
            this.TableName = TableName;
            foreach (var c in Columns)
                DataGridColumn.Add(dgTable, c.Header, c.Binding, c.MinWidth);
            Type a = Type.GetType(TableName);
            GetData();
        }

        private string TableName;

        private void GetData()
        {
            // Если какой-либо объект не инициализирован, выходить из функции, чтобы предотвратить ошибки
            if (tbSearch == null || dgTable == null)
                return;

            if (TableName == "Client")
                dgTable.ItemsSource = AppData.DB.Database.SqlQuery<Client>("SELECT * FROM Client WHERE ID>0" +
                    (tbSearch.Text != "Введите для поиска" && tbSearch.Text != String.Empty ?
                        " AND CHARINDEX('" + tbSearch.Text + "', Name) > 0 OR CHARINDEX('" + tbSearch.Text + "', Phone) > 0 OR CHARINDEX('" + tbSearch.Text + "', Email) > 0" : String.Empty)).ToList();
            else if (TableName == "Nomenclature")
                dgTable.ItemsSource = AppData.DB.Database.SqlQuery<Nomenclature>("SELECT * FROM Nomenclature WHERE ID>0" +
                    (tbSearch.Text != "Введите для поиска" && tbSearch.Text != String.Empty ?
                        " AND CHARINDEX('" + tbSearch.Text + "', Name) > 0" : String.Empty)).ToList();
        }

        private enum Actions
        {
            Edit = 1,
            Delete,
        }

        private enum More
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
                    case Actions.Edit:
                        break;
                    case Actions.Delete:
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
                        if (dgTable.SelectedIndex == -1)
                            goto DropMenuReturn;
                        AppData.DB.Database.ExecuteSqlCommand("DELETE FROM " + TableName + " WHERE ID = " + dgTable.SelectedItem?.GetType()?.GetProperty("ID")?.GetValue(dgTable.SelectedItem, null).ToString());
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

        private void btnCreate_Click(object sender, RoutedEventArgs e) { }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e) => GetData();

        private void tbSearch_Clear(object sender, RoutedEventArgs e) => tbSearch.Text = String.Empty;

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
