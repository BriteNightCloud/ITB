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
            this.a = new Sale_Details();
        }

        private Sale_Details a;

        public wCreate(Sale_Details a) : base()
        {
            this.a.Index = a.Index;
            this.a.Price = a.Price;
            this.a.Count = a.Count;
            this.a.Nomenclature_ID = a.Nomenclature_ID;
            Nomenclatures.SelectedItem = AppData.DB.Database.SqlQuery<Nomenclature>("SELECT * WHERE ID = " + a.ID).ToList().First();
            Count.Text = a.Count.ToString();
            Price.Text = a.Price.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
        }

        private void Count_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Count.Text, out int a))
                Cost.Text = (a * ((Nomenclature)Nomenclatures.SelectedItem).Price).ToString();
        }
    }
}
