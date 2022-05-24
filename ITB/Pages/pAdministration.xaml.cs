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

namespace ITB.Pages
{
    /// <summary>
    /// Логика взаимодействия для pAdministration.xaml
    /// </summary>
    public partial class pAdministration : Page
    {
        public pAdministration(string Title, params DataGridColumn[] Columns)
        {
            InitializeComponent(); 
            this.Title = Title;
            foreach (var c in Columns)
                DataGridColumn.Add(dgTable, c.Header, c.Binding, c.MinWidth);
        }

        enum Actions
        {
            Edit = 1,
            Delete,
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
                        break;
                    case More.Update:
                        break;
                }
            obj.SelectedIndex = 0;
        }

        private void tbSearch_Clear(object sender, RoutedEventArgs e) => tbSearch.Text = String.Empty;

        private void btnCreate_Click(object sender, RoutedEventArgs e) => dgTable.Items.Add(new { });
    }
}
