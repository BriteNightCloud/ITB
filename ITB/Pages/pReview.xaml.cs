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
            dgTable.Items.Add(new {ID = "00УТ-004111", Date = "20.05.2022", Cost = "15 000,00", Client = "МОРП", Status = "В процессе отгрузки"});
            dgTable.Items.Add(new {ID = "00УТ-004112", Date = "20.05.2022", Cost = "15 000,00", Client = "МОРП", Status = "В процессе отгрузки", Test = "123"});
            dgTable.Items.Add(new {ID = "00УТ-004113", Date = "20.05.2022", Cost = "15 000,00", Client = "МОРП", Status = "В процессе отгрузки"});
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
    }
}