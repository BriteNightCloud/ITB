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
using ITB.DB;

namespace ITB.Windows
{
    /// <summary>
    /// Логика взаимодействия для wCreate.xaml
    /// </summary>
    public partial class wCreate : Window
    {
        public wCreate()
        {
            InitializeComponent();
            Nomenclatures.ItemsSource = AppData.DB.Nomenclatures.ToList();
            Nomenclatures.Focus();
        }

        private Sale_Details a;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                a = new Sale_Details();
                a.Nomenclature_ID = ((Nomenclature)Nomenclatures.SelectedItem).ID;
                a.Price = decimal.Parse(Price.Text);
                a.Count = decimal.Parse(Count.Text);
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public Sale_Details Elem
        {
            get
            {
                return a;
            }
        }

        private void Nomenclatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Price.Text = ((Nomenclature)Nomenclatures.SelectedItem).Price.ToString();
            Count_TextChanged(sender, e);
            Count.Focus();
        }

        private void Count_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(Price.Text, out decimal b);
            if (int.TryParse(Count.Text, out int a))
                Cost.Text = (a * b).ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal.TryParse(Price.Text, out decimal b);
            if (int.TryParse(Count.Text, out int a))
                Cost.Text = (a * b).ToString();
        }
    }
}
