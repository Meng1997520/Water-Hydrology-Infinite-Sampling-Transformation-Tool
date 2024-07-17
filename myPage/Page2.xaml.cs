using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_App.myClass;
namespace Wpf_App
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        const int MaxEllipseTableCount = 10;
        int currentDotTableCount = 0;
        public Page2()
        {
            GlobalVariables.GetInstance();
            InitializeComponent();
            showEllipseGrid(currentDotTableCount);
        }
        public void showEllipseGrid(int i)
        {
            int j = 0;
            
            while( i != GlobalVariables.MyDotTable.Count && j != MaxEllipseTableCount)
            {
                switch (i)
                {
                    case 0:
                        initialDataGrid(datagrid1, GlobalVariables.MyDotTable[i]);
                        textblock1.Text = (i + 1).ToString();
                        break;
                    case 1:
                        initialDataGrid(datagrid2, GlobalVariables.MyDotTable[i]);
                        textblock2.Text = (i + 1).ToString();
                        break;
                    case 2:
                        initialDataGrid(datagrid3, GlobalVariables.MyDotTable[i]);
                        textblock3.Text = (i + 1).ToString();
                        break;
                    case 3:
                        initialDataGrid(datagrid4, GlobalVariables.MyDotTable[i]);
                        textblock4.Text = (i + 1).ToString();
                        break;
                    case 4:
                        initialDataGrid(datagrid5, GlobalVariables.MyDotTable[i]);
                        textblock5.Text = (i + 1).ToString();
                        break;
                    case 5:
                        initialDataGrid(datagrid6, GlobalVariables.MyDotTable[i]);
                        textblock6.Text = (i + 1).ToString();
                        break;
                    case 6:
                        initialDataGrid(datagrid7, GlobalVariables.MyDotTable[i]);
                        textblock7.Text = (i + 1).ToString();
                        break;
                    case 7:
                        initialDataGrid(datagrid8, GlobalVariables.MyDotTable[i]);
                        textblock8.Text = (i + 1).ToString();
                        break;
                    case 8:
                        initialDataGrid(datagrid9, GlobalVariables.MyDotTable[i]);
                        textblock9.Text = (i + 1).ToString();
                        break;
                    case 9:
                        initialDataGrid(datagrid10, GlobalVariables.MyDotTable[i]);
                        textblock10.Text = (i + 1).ToString();
                        break;
                   
                    default:
                        break;
                }
                i++;
            }
        }
        public void initialDataGrid(DataGrid dataGrid, EllipseGrid ellipseGrid)
        {
            if(GlobalVariables.MyDotTable.Count == 0)
            {
                return;
            }
            // 获取列数
            int columnCount = ellipseGrid.Ellipsetable[0].Count;
            // 设置DataGrid的行和列
            for (int i = 0; i != columnCount; i++)
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn
                {
                    Header = $"{i + 1}",
                    MinWidth = 6,
                   

                };
                dataGrid.Columns.Add(column);
                // 创建数据模板
                DataTemplate dataTemplate = new DataTemplate();

                // 创建一个 FrameworkElementFactory
                FrameworkElementFactory ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
                ellipseFactory.SetValue(Ellipse.WidthProperty, 9.0);
                ellipseFactory.SetValue(Ellipse.HeightProperty, 9.0);
                ellipseFactory.SetBinding(Ellipse.FillProperty, new Binding($"[{i}].FillColor"));

                // 将 Ellipse 添加到数据模板的可视树中
                dataTemplate.VisualTree = ellipseFactory;

                // 将数据模板设置为 DataGridTemplateColumn 的 CellTemplate
                column.CellTemplate = dataTemplate;

            }
            dataGrid.LoadingRow += (sender, e) =>
            {
                e.Row.Header = e.Row.GetIndex() + 1;
            };
           
            dataGrid.ItemsSource = ellipseGrid.Ellipsetable;
            
        }

       
    }
}
