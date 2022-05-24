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

namespace ITB.Pages
{
    /// <summary>
    /// Логика взаимодействия для pCreate.xaml
    /// </summary>
    public partial class pCreate : Page
    {
        public pCreate(string Title, params DataGridColumn[] Columns)
        {
            InitializeComponent();
            this.Title = Title;
            foreach (var c in Columns)
                DataGridColumn.Add(dgTable, c.Header, c.Binding, c.MinWidth);
        }

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
                        break;
                    case More.Cancel:
                        break;
                }
            obj.SelectedIndex = 0;
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e) => ((DatePicker)sender).SelectedDate = DateTime.Now;

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
