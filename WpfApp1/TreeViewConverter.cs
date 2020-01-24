using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1
{
    public class TreeViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var list = value as List<Node>;
            var col = new List<TreeViewItem>();

            CreateTree(list, col);

            return col;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }

        private void CreateTree(List<Node> list, dynamic ItemsSource)
        {
            foreach (var node in list)
            {
                var newTV = new TreeViewItem() { Header = node.Data };
                if (node.childs.Count > 0)
                {
                    CreateTree(node.childs, newTV.Items);
                    ItemsSource.Add(newTV);
                }
                else
                {
                    ItemsSource.Add(newTV);
                }
            }
        }
    }
}
