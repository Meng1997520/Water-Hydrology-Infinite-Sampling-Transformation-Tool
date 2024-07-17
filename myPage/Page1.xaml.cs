using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace Wpf_App
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public int currentCount = 0;
        int terminal;
        public Page1()
        {
            InitializeComponent();
            initialgrid();
        }
        public void initialgrid()
        {
            creatcolumn(datagrid1);
            creatcolumn(datagrid2);
            creatcolumn(datagrid3);
            creatcolumn(datagrid4);
            creatcolumn(datagrid5);
            creatcolumn(datagrid6);
            for (; currentCount != 6 && currentCount < GlobalVariables.MyDotTable.Count; currentCount++)
            {
                switch (currentCount)
                {
                    case 0:
                        textblock1.Text = $"{currentCount + 1}";
                       
                        GlobalVariables.MyDigitalTable.Add(new DigitalGrid(datagrid1));
                        break;
                    case 1:
                        textblock2.Text = $"{currentCount + 1}";
                        GlobalVariables.MyDigitalTable.Add(new DigitalGrid(datagrid2));
                        break;
                    case 2:
                        textblock3.Text = $"{currentCount + 1}";
                        GlobalVariables.MyDigitalTable.Add(new DigitalGrid(datagrid3));
                        break;
                    case 3:
                        textblock4.Text = $"{currentCount + 1}";
                        GlobalVariables.MyDigitalTable.Add(new DigitalGrid(datagrid4));
                        break;
                    case 4:
                        textblock5.Text = $"{currentCount + 1}";
                        GlobalVariables.MyDigitalTable.Add(new DigitalGrid(datagrid5));
                        break;
                    case 5:
                        textblock6.Text = $"{currentCount + 1}";
                        GlobalVariables.MyDigitalTable.Add(new DigitalGrid(datagrid6));
                        break;
                    default:
                        break;
                }
            }

        }
        private void ConnectDatagrid()
        {
            terminal = currentCount + 6;
            
            for(;currentCount != terminal && currentCount < GlobalVariables.MyDotTable.Count; currentCount++)
            {

                int n = currentCount % 6;
                int m = currentCount + 1;
                switch (n)
                {
                    case 0:
                        textblock1.Text = $"{m}";
                        GlobalVariables.MyDigitalTable[0].Flush(GlobalVariables.MyDigitalGridData[currentCount]);
                        break;
                    case 1:
                        textblock2.Text = $"{m}";
                        GlobalVariables.MyDigitalTable[1].Flush(GlobalVariables.MyDigitalGridData[currentCount]);
                        break;
                    case 2:
                        textblock3.Text = $"{m}";
                        GlobalVariables.MyDigitalTable[2].Flush(GlobalVariables.MyDigitalGridData[currentCount]);
                        break;
                    case 3:
                        textblock4.Text = $"{m}";
                        GlobalVariables.MyDigitalTable[3].Flush(GlobalVariables.MyDigitalGridData[currentCount]);
                        break;
                    case 4:
                        textblock5.Text = $"{m}";
                        GlobalVariables.MyDigitalTable[4].Flush(GlobalVariables.MyDigitalGridData[currentCount]);
                        break;
                    case 5:
                        textblock6.Text = $"{m}";                      
                        GlobalVariables.MyDigitalTable[5].Flush(GlobalVariables.MyDigitalGridData[currentCount]);
                        break;
                    default:
                        break;

                }
            }           
            
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
        public void UpPage()
        {
            if (currentCount  <= 6)
            {
                MessageBox.Show("已是第一页");
                return;
            }
            if(currentCount > GlobalVariables.MyDigitalGridData.Count)
            {
              
                GlobalVariables.MyDigitalTable[1].Connect(datagrid2);
                GlobalVariables.MyDigitalTable[2].Connect(datagrid3);
                GlobalVariables.MyDigitalTable[3].Connect(datagrid4);
                GlobalVariables.MyDigitalTable[4].Connect(datagrid5);
                GlobalVariables.MyDigitalTable[5].Connect(datagrid6);
            }
            currentCount -= 12;
          
            ConnectDatagrid();
            
        }
        public void DownPage()
        {
            if(currentCount < GlobalVariables.MyDigitalGridData.Count)
            {
                ConnectDatagrid();
                if (currentCount == GlobalVariables.MyDigitalGridData.Count)
                {
                    for (; currentCount != terminal; currentCount++)
                    {
                        int n = currentCount % 6;
                        switch (n)
                        {
                            case 1:
                                textblock2.Text = "";
                                GlobalVariables.MyDigitalTable[0].DisConnect(datagrid2);
                                break;
                            case 2:
                                textblock3.Text = "";
                                GlobalVariables.MyDigitalTable[0].DisConnect(datagrid3);
                                break;
                            case 3:
                                textblock4.Text = "";
                                GlobalVariables.MyDigitalTable[0].DisConnect(datagrid4);
                                break;
                            case 4:
                                textblock5.Text = "";
                                GlobalVariables.MyDigitalTable[0].DisConnect(datagrid5);
                                break;
                            case 5:
                                textblock6.Text = "";
                                GlobalVariables.MyDigitalTable[0].DisConnect(datagrid6);
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
            else
            {              
                
                MessageBox.Show("已是最后一页");
               
                return;
            }
            
        }
    }
}
