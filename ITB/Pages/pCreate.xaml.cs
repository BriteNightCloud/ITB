using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для pCreate.xaml
    /// </summary>
    public partial class pCreate : Page
    {
        public pCreate(string Title)
        {
            InitializeComponent();
            this.Title = Title;
            this.Sale_ID = AppData.DB.Database.SqlQuery<Sale>("SELECT TOP 1 * FROM Sales ORDER BY ID DESC").ToList().Last().ID + 1;
            DataGridColumn[] Columns = {
                new DataGridColumn("N", "Index", 25),
                new DataGridColumn("Номенклатура", "Nomenclature_Name", 220),
                new DataGridColumn("Количество", "Count", 50),
                new DataGridColumn("Цена за ед.", "Price", 110),
                new DataGridColumn("Стоимость", "Cost", 125)
            };
            foreach (var c in Columns)
                DataGridColumn.Add(dgTable, c.Header, c.Binding, c.MinWidth);

            cbClient.ItemsSource = AppData.DB.Clients.ToList();
            cbManager.ItemsSource = AppData.DB.Workers.ToList();
        }

        public pCreate(string Title, Sale sale, bool isEdit = false) : this(Title)
        {
            this.isEdit = isEdit;

            this.sale = sale;
            if (isEdit)
            this.Sale_ID = sale.ID;

            if (isEdit)
                this.tbID.Text = sale.ID.ToString();

            this.cbStatus.Text = sale.Status;
            this.tbCost.Text = sale.Cost.ToString();
            this.cbManager.SelectedItem = AppData.DB.Workers.Where(c => c.ID == sale.Manager_ID).First();
            this.cbClient.SelectedItem = AppData.DB.Clients.Where(c => c.ID == sale.Client_ID).First();
            this.tbComment.Text = sale.Comment;
            this.dpDate.SelectedDate = sale.Date;

            foreach (Sale_Details i in AppData.DB.Sale_Details.Where(c => c.Sales_ID == sale.ID))
            {
                i.Sales_ID = this.Sale_ID;
                dgTable.Items.Add(i);
            }
        }

        private bool isEdit;
        private int Sale_ID;
        private Sale sale;
        private bool isDetailsChanged = false;

        enum More
        {
            Create = 1,
            Cancel
        }

        private void DropMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox obj = (ComboBox)sender;
            if (!obj.IsDropDownOpen)
                return;
            if (obj.Name == "cbMore")
                switch ((More)obj.SelectedIndex)
                {
                    case More.Create:
                        AddAndSave_Click(sender, e);
                        break;
                    case More.Cancel:
                        ((wMain)Window.GetWindow(this)).lvTabs.Items.RemoveAt(((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex);

                        if (((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex == -1)
                            ((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex = ((wMain)Window.GetWindow(this)).lvTabs.Items.Count - 1;
                        break;
                }
            obj.SelectedIndex = 0;
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e) => ((DatePicker)sender).SelectedDate = DateTime.Now;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            wCreate window = new wCreate();
            window.Closing += AddElem;
            window.Closing += UpdateCost;
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
            this.isDetailsChanged = true;
        }

        private void AddElem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Sale_Details a = ((wCreate)sender).Elem;
            if (a != null)
            {
                a.Sales_ID = Sale_ID;
                a.Index = dgTable.Items.Count + 1;
                dgTable.Items.Add(a);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgTable.SelectedIndex == -1)
                return;
            dgTable.Items.Remove(dgTable.SelectedItem);

            int i = 1;
            List<Sale_Details> list = new List<Sale_Details>();
            foreach (Sale_Details a in dgTable.Items)
            {
                a.Index = i++;
                list.Add(a);
            }
            dgTable.Items.Clear();
            foreach (Sale_Details a in list)
                dgTable.Items.Add(a);
            this.isDetailsChanged = true;
        }

        private void AddAndSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
                goto Edit;
            try
            {
                if (dgTable.Items.Count < 1)
                    throw new Exception("Необходимо указать хотя бы один товар!");
                AppData.DB.Sales.Add(new Sale() { Client_ID = ((Client)cbClient.SelectedItem).ID, Cost = Decimal.Parse(tbCost.Text), Date = dpDate.DisplayDate, 
                    Comment = tbComment.Text, Manager_ID = ((Worker)cbManager.SelectedItem).ID, Status = cbStatus.Text});
                AppData.DB.SaveChanges();
                foreach (Sale_Details a in dgTable.Items)
                    AppData.DB.Sale_Details.Add(a);
                AppData.DB.SaveChanges();

                // Закрыть вкладку
                ((wMain)Window.GetWindow(this)).lvTabs.Items.RemoveAt(((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex);

                // Выбрать вкладку слева от закрытой
                if (((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex == -1)
                    ((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex = ((wMain)Window.GetWindow(this)).lvTabs.Items.Count - 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.InnerException?.InnerException.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            return;

        Edit:

            try
            {
                if (dgTable.Items.Count < 1)
                    throw new Exception("Необходимо указать хотя бы один товар!");
                Sale a = AppData.DB.Sales.Where(c => c.ID == sale.ID).First();
                a.Client_ID = ((Client)cbClient.SelectedItem).ID;
                a.Cost = Decimal.Parse(tbCost.Text);
                a.Date = dpDate.DisplayDate;
                a.Comment = tbComment.Text;
                a.Manager_ID = ((Worker)cbManager.SelectedItem).ID;
                a.Status = cbStatus.Text;

                AppData.DB.SaveChanges();

                if (isDetailsChanged)
                {
                    foreach (Sale_Details i in AppData.DB.Sale_Details.Where(c => c.Sales_ID == sale.ID))
                        AppData.DB.Sale_Details.Remove(i);
                    AppData.DB.SaveChanges();

                    foreach (Sale_Details i in dgTable.Items)
                        AppData.DB.Sale_Details.Add(i);
                    AppData.DB.SaveChanges();
                }

                // Закрыть вкладку
                ((wMain)Window.GetWindow(this)).lvTabs.Items.RemoveAt(((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex);

                // Выбрать вкладку слева от закрытой
                if (((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex == -1)
                    ((wMain)Window.GetWindow(this)).lvTabs.SelectedIndex = ((wMain)Window.GetWindow(this)).lvTabs.Items.Count - 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.InnerException?.InnerException.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void UpdateCost(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tbCost.Text = "0";
            foreach (Sale_Details a in dgTable.Items)
                tbCost.Text = (decimal.Parse(tbCost.Text) + a.Cost).ToString();
        }
    }
}
