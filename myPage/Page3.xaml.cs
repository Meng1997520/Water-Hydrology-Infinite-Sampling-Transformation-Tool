using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Wpf_App.myClass;
namespace Wpf_App.myPage
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        
        double tableWidth;
       
        public Page3()
        {
           
            GlobalVariables.GetInstance();
          
            InitializeComponent();
            
           
            Loaded += YourWindow_Loaded;
            tableWidth = 80;
        }
       

        public void initialDataGrid(DataGrid dataGrid, EllipseGrid ellipseGrid)
        {
            if (GlobalVariables.GetInstance().tmp_MyDotTable.Count == 0)
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
                    //Header = $"{i + 1}",
                   
                    MinWidth = 8,
                    
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
            dataGrid.ItemsSource = ellipseGrid.Ellipsetable;
           
        }
        public void CreatAllEllipseDatagrid(int i)
        {

            // 创建 StackPanel
            DockPanel dockPanel = new DockPanel
            {
                
                Height = 100,
                Background = Brushes.AliceBlue,
                Width = tableWidth
            };
           
            // 创建 TextBlock
            TextBlock textBlock = new TextBlock
            {
                FontSize = 12,
                Text = (i + 1).ToString(),
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.Bold,
                Width = 15,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            // 创建DataGrid
            DataGrid dataGrid1 = new DataGrid
            {

                SelectionUnit = DataGridSelectionUnit.Cell,
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserResizeRows = false,
                CanUserResizeColumns = false,
                CanUserSortColumns = false,
               
            };
            initialDataGrid(dataGrid1, GlobalVariables.GetInstance().tmp_MyDotTable[i]);
            // 将DataGrid添加到Border的Child属性
            // 创建 ScrollViewer
            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
                Content = dataGrid1,
                Height = double.NaN
            };

            // 将 TextBlock 和 ScrollViewer 添加到 StackPanel
            dockPanel.Children.Add(textBlock);
            dockPanel.Children.Add(scrollViewer);

            // 添加到界面
            myWrapPanel.Children.Add(dockPanel);
        }
        public void DeleteEllipseDatagrid(int i)
        {            
             myWrapPanel.Children.RemoveAt(i);
            
        }
        public void ClearEllipseDatagrid()
        {
            myWrapPanel.Children.Clear();
        }
        public void RefreshText(int i)
        {
            if (myWrapPanel.Children[i] is DockPanel dockPanel)
            {
                // 在 DockPanel 中查找 TextBlock
                //TextBlock textBlock = dockPanel.Children.OfType<TextBlock>().FirstOrDefault();
                if (dockPanel.Children[0] is TextBlock textblock)
                {
                    textblock.Text = (i + 1).ToString();
                }
                                  
            }
        }
      
        private async void YourWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                await LoadDataAsync();
            }
            catch 
            {
               // cancellationTokenSource?.Cancel();
            }
        }
        private async Task LoadDataAsync()
        {
            
            for (int i = 0; i != GlobalVariables.GetInstance().tmp_MyDotTable.Count; i++)
            {
                CreatAllEllipseDatagrid(i);
                if(i > 5)
                {
                  
                    await Dispatcher.InvokeAsync(() => { }, DispatcherPriority.ApplicationIdle).Task;

                }
            }

        }

    }
}
