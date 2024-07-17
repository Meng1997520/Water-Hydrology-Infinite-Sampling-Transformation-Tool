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
using Wpf_App.myClass;

namespace Wpf_App.myPage
{
    /// <summary>
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class Page4 : Page
    {
        List<DigitalGrid> DigitalTable = new List<DigitalGrid>();
       // List<EllipseGrid> EillipseTable = new List<EllipseGrid>();
        List<DataGrid> EillipseDataGrid = new List<DataGrid>();
        int count;
        int secondcount = 0;
        public Page4()
        {
            
            InitializeComponent();
            for(int i = 0;i!= 12; i++)
            {
                DigitalGrid temp = new DigitalGrid();
                DigitalTable.Add(temp);
                
            }

            creatcolumn(datagrid1);
            creatcolumn(datagrid2);
            creatcolumn(datagrid3);
            creatcolumn(datagrid4);
            creatcolumn(datagrid5);
            creatcolumn(datagrid6);
            creatcolumn(datagrid7);
            creatcolumn(datagrid8);
            creatcolumn(datagrid9);
            creatcolumn(datagrid10);
            creatcolumn(datagrid11);
            creatcolumn(datagrid12);

            DigitalTable[0].Connect(datagrid1);
            DigitalTable[1].Connect(datagrid2);
            DigitalTable[2].Connect(datagrid3);
            DigitalTable[3].Connect(datagrid7);
            DigitalTable[4].Connect(datagrid8);
            DigitalTable[5].Connect(datagrid9);

            DigitalTable[6].Connect(datagrid4);
            DigitalTable[7].Connect(datagrid5);
            DigitalTable[8].Connect(datagrid6);
            DigitalTable[9].Connect(datagrid10);
            DigitalTable[10].Connect(datagrid11);
            DigitalTable[11].Connect(datagrid12);

            //ellipse
            /*initialDataGrid(Ellipse1, EillipseTable[0]);

            initialDataGrid(Ellipse2, EillipseTable[1]);

            initialDataGrid(Ellipse4, EillipseTable[3]);
            initialDataGrid(Ellipse5, EillipseTable[4]);
            initialDataGrid(Ellipse7, EillipseTable[6]);
            initialDataGrid(Ellipse8, EillipseTable[7]);
            initialDataGrid(Ellipse10, EillipseTable[9]);
            initialDataGrid(Ellipse11, EillipseTable[10]);*/

            

            EillipseDataGrid.Add(Ellipse1);
            EillipseDataGrid.Add(Ellipse2);
            EillipseDataGrid.Add(Ellipse3);
            EillipseDataGrid.Add(Ellipse7);
            EillipseDataGrid.Add(Ellipse8);
            EillipseDataGrid.Add(Ellipse9);
            EillipseDataGrid.Add(Ellipse4);
            EillipseDataGrid.Add(Ellipse5);
            EillipseDataGrid.Add(Ellipse6);

            EillipseDataGrid.Add(Ellipse10);
            EillipseDataGrid.Add(Ellipse11);
            EillipseDataGrid.Add(Ellipse12);

            /*for(int i = 0;i!= 12; i++)
            {
                initialDataGrid(EillipseDataGrid[i]);
            }
*/
            show(0, 0);


        }
       
       
        //frist parameter is symbol of GUAN  two-dimensional array first dimension . k represent second dimension
        void show(int index,int k)
        {
            
            if (GlobalVariables.GetInstance().Guan[0].Count == 0)
                return;
            for(int i =0;i!= 4; i++)
            {
                int offset = 3 * i;
                DigitalTable[2 + offset].Flush(GlobalVariables.GetInstance().Guan[index][k + i]);

                DigitalTable[1 + offset].Flush(GlobalVariables.GetInstance().Guan[index][k + i].mother);

               // EillipseTable[1 + offset] = GlobalVariables.GetInstance().Guan[index][k + i].motherDot;
               // EillipseDataGrid[1 + offset].ItemsSource = GlobalVariables.GetInstance().Guan[index][k + i].motherDot.Ellipsetable;
            
                changedot(EillipseDataGrid[1 + offset], GlobalVariables.GetInstance().Guan[index][k + i].motherDot);

                DigitalTable[0 + offset].Flush(GlobalVariables.GetInstance().Guan[index][k + i].mother.mother);
                //EillipseTable[offset] = GlobalVariables.GetInstance().Guan[index][k + i].mother.motherDot;
                // EillipseDataGrid[offset].ItemsSource = GlobalVariables.GetInstance().Guan[index][k + i].mother.motherDot.Ellipsetable;
                changedot(EillipseDataGrid[offset], GlobalVariables.GetInstance().Guan[index][k + i].mother.motherDot);

            }
            TextBlock1.Text = $"{k + 1}";
            TextBlock2.Text = $"{k + 2}";
            TextBlock3.Text = $"{k + 3}";
            TextBlock4.Text = $"{k + 4}";
        }
        public void  lastpage(object sender, RoutedEventArgs e)
        {
           
            if(secondcount == 0)
            {
                MessageBox.Show("已是第一页");
            }
            else
            {
                secondcount -= 4;
                show(count, secondcount);
            }
            
        }
        public void nextpage(object sender, RoutedEventArgs e)
        {
           
            if (secondcount + 4 >= GlobalVariables.GetInstance().Guan[count].Count)
            {
                MessageBox.Show("已是最后一页");
            }
            else
            {
                secondcount += 4;
                show(count, secondcount);
            }
        }
        public void initialDataGrid(DataGrid dataGrid/*,EllipseGrid ellipseGrid*/)
        {
            if (GlobalVariables.MyDotTable.Count == 0)
            {
                return;
            }
            // 获取列数
             //int columnCount = ellipseGrid.Ellipsetable[0].Count;
            int columnCount = 8;
            // 设置DataGrid的行和列
            for (int i = 0; i != columnCount; i++)
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn
                {
                    //Header = $"{i + 1}",
                    MinWidth = 4,


                };
                dataGrid.Columns.Add(column);
                // 创建数据模板
                DataTemplate dataTemplate = new DataTemplate();

                // 创建一个 FrameworkElementFactory
                FrameworkElementFactory ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
                ellipseFactory.SetValue(Ellipse.WidthProperty, 6.0);
                ellipseFactory.SetValue(Ellipse.HeightProperty, 6.0);
                ellipseFactory.SetBinding(Ellipse.FillProperty, new Binding($"[{i}].FillColor"));

                // 将 Ellipse 添加到数据模板的可视树中
                dataTemplate.VisualTree = ellipseFactory;

                // 将数据模板设置为 DataGridTemplateColumn 的 CellTemplate
                column.CellTemplate = dataTemplate;

            }
           

            //dataGrid.ItemsSource = ellipseGrid.Ellipsetable;

        }
        public void changedot(DataGrid dataGrid, EllipseGrid after)
        {
            if (GlobalVariables.MyDotTable.Count == 0)
            {
                return;
            }
            // 获取列数
            int columnCount = after.Ellipsetable[0].Count;
            if (columnCount > dataGrid.Columns.Count)
            {
                // 设置DataGrid的行和列
                for (int i = dataGrid.Columns.Count; i != columnCount; i++)
                {
                    DataGridTemplateColumn column = new DataGridTemplateColumn
                    {
                        //Header = $"{i + 1}",
                        MinWidth = 4,


                    };
                    dataGrid.Columns.Add(column);
                    // 创建数据模板
                    DataTemplate dataTemplate = new DataTemplate();

                    // 创建一个 FrameworkElementFactory
                    FrameworkElementFactory ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
                    ellipseFactory.SetValue(Ellipse.WidthProperty, 6.0);
                    ellipseFactory.SetValue(Ellipse.HeightProperty, 6.0);
                    ellipseFactory.SetBinding(Ellipse.FillProperty, new Binding($"[{i}].FillColor"));

                    // 将 Ellipse 添加到数据模板的可视树中
                    dataTemplate.VisualTree = ellipseFactory;

                    // 将数据模板设置为 DataGridTemplateColumn 的 CellTemplate
                    column.CellTemplate = dataTemplate;

                }

            }
            else
            {
                int colums = dataGrid.Columns.Count;
                for (int i = columnCount; i != colums; i++)
                {
                    dataGrid.Columns.RemoveAt(dataGrid.Columns.Count - 1);
                }
            }

            dataGrid.ItemsSource = after.Ellipsetable;

        }

        private void creatcolumn(DataGrid myDataGrid)
        {
            // 添加列
            for (int i = 0; i < 40; i++)
            {
                var column = new DataGridTemplateColumn
                {
                    Header = "",
                    Width = 20
                };

                //创建一个数据模板
                DataTemplate dataTemplate = new DataTemplate();

                // 创建一个 TextBlock 用于显示数据
                FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                //textBlockFactory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding($"[{i}]"));
                textBlockFactory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding($"[{i}].Value"));
                textBlockFactory.SetBinding(TextBlock.ForegroundProperty, new System.Windows.Data.Binding($"[{i}].ForeColor"));
                /* FrameworkElementFactory LineFactory = new FrameworkElementFactory(typeof(Line));
                 LineFactory.SetBinding(Line.X1Property, new System.Windows.Data.Binding($"[{i}].Value"));
                 textBlockFactory.AppendChild(LineFactory);*/
                textBlockFactory.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
                // 将 TextBlock 添加到数据模板的可视树中
                dataTemplate.VisualTree = textBlockFactory;

                // 将数据模板设置为 DataGridTemplateColumn 的 CellTemplate
                column.CellTemplate = dataTemplate;

                // 将列添加到 DataGrid 的列集合中
                myDataGrid.Columns.Add(column);
            }
        }
       
       
    }
}
