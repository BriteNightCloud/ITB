using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ITB
{
    public class DataGridColumn
    {
        public DataGridColumn(string Header, string Binding, int minWidth = 0)
        {
            this.Header = Header;
            this.Binding = Binding;
            this.MinWidth = minWidth;
        }

        public static void Add(DataGrid dg, string Header, string Binding, int minWidth)
        {
            DataGridTextColumn a = new DataGridTextColumn();
            a.Header = Header;
            a.Binding = new Binding(Binding);
            a.MinWidth = minWidth;
            dg.Columns.Add(a);
        }

        public string Header { get; }
        public string Binding { get; }
        public int MinWidth { get; }
    }
}
