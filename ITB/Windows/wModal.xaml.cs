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
        public wModal(Frame frmMain, ListView lvTabs, StackPanel spMenu)
        {
            InitializeComponent();
            frm = frmMain;
            lvt = lvTabs;
            spm = spMenu;
        }

        private Frame frm;
        private ListView lvt;
        private StackPanel spm;

        public void AddCategory(string Header)
        {
            StackPanel a = new StackPanel() { Margin = new Thickness(40, 0, 0, 30) };
            a.Children.Add(new TextBlock() { Text = Header, FontSize = 18, Margin = new Thickness(0,0,0,5), Foreground = new SolidColorBrush(Color.FromRgb(61, 175, 128)) });
            wpContainer.Children.Add(a);
        }

        public void AddItem(string ItemTitle, Page ItemPage)
        {
            Button a = new Button() { Content = ItemTitle, Style = FindResource("ModalMenuButton") as Style };
            a.Click += (object s, RoutedEventArgs e) => ((wMain)Owner).OpenInTab(ItemTitle, ItemPage);
            a.Click += (object s, RoutedEventArgs e) => Close();
            ((StackPanel)wpContainer.Children[wpContainer.Children.Count-1]).Children.Add(a);
        }

        private void Exit_Click(object sender, EventArgs e) => Close();
    }
}