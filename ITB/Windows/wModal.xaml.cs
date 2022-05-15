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

namespace ITB.Windows
{
    /// <summary>
    /// Логика взаимодействия для wModal.xaml
    /// </summary>
    public partial class wModal : Window
    {
        public wModal()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
