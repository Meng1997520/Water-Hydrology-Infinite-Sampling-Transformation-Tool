using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using Wpf_App.myClass;
using Wpf_App.myPage;
namespace Wpf_App
{
    /// <summary>
    /// AddColour.xaml 的交互逻辑
    /// </summary>
    /// 
    public class AddColourModel
    {
        public int shu_count = 0;
        public int  heng_count = 0;
        public SolidColorBrush color;
        private DataGrid dataGrid;
        private TextBox heng;
        private TextBox shu;
      
        public ObservableCollection<ObservableCollection<EllipseCellData>> SpecialModel;
        public AddColourModel(DataGrid datagrid, TextBox Heng,TextBox Shu)
        {
            CreatDatagrid(heng_count, shu_count, null, datagrid);
            dataGrid = datagrid;
            heng = Heng;
            shu = Shu;
            
            
        }
        private void CreatDatagrid(int heng, int shu, SolidColorBrush color, DataGrid dataGrid)
        {
           SpecialModel = new ObservableCollection<ObservableCollection<EllipseCellData>>();
            for (int i = 0; i < shu; i++)
            {
                var row = new ObservableCollection<EllipseCellData>();
                for (int j = 0; j < heng; j++)
                {
                    row.Add(new EllipseCellData() { FillColor = color });
                }
                SpecialModel.Add(row);
            }

            // 将数据绑定到 DataGrid
            dataGrid.ItemsSource = SpecialModel;
            // 创建列
            for (int i = 0; i < heng; i++)
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn
                {
                    Header = $"{i + 1}",
                    //CellTemplate = dataTemplate
                };

                // 创建数据模板
                DataTemplate dataTemplate = new DataTemplate();

                // 创建一个 FrameworkElementFactory，这里使用 TextBlock 作为示例
                FrameworkElementFactory ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
                ellipseFactory.SetValue(Ellipse.WidthProperty, 10.0);
                ellipseFactory.SetValue(Ellipse.HeightProperty, 10.0);
                ellipseFactory.SetBinding(Ellipse.FillProperty, new Binding($"[{i}].FillColor"));

                // 将 Ellipse 添加到数据模板的可视树中
                dataTemplate.VisualTree = ellipseFactory;

                // 将数据模板设置为 DataGridTemplateColumn 的 CellTemplate
                column.CellTemplate = dataTemplate;


                // 将列添加到 DataGrid 的列集合中
                dataGrid.Columns.Add(column);

            }
            dataGrid.LoadingRow += (sender, e) =>
            {
                e.Row.Header = e.Row.GetIndex() + 1;

            };

        }
        public void Updata()
        {
            
            try
            {
                shu_count = int.Parse(shu.Text);
                heng_count = int.Parse(heng.Text);
                // 使用 result 变量，它包含了从 TextBox 中解析得到的整数值
            }
            catch (FormatException)
            {
                // 处理无法转换为整数的情况，例如 TextBox 中的内容不是有效的数字
                MessageBox.Show("请输入整数");
                return;
            }
            SpecialModel.Clear();
            SpecialModel = null;
            dataGrid.Columns.Clear();
            CreatDatagrid(heng_count, shu_count, color, dataGrid);
           
        }
       
    }
    public partial class AddColour : Window
    {
        private static AddColour instance;
        public static AddColour Instance
        {
            get
            {
                if (instance == null || !instance.IsLoaded)
                {
                   
                    instance = new AddColour();
                }
                else
                {
                    instance.Activate();
                }
                return instance;
            }
        }


        AddColourModel[] a = new AddColourModel[5];
        public MyCommand ShowCommonLib { get; set; }
      
        public ObservableCollection<ObservableCollection<EllipseCellData>> SpecialModel;
        
        private AddColour()
        {
            InitializeComponent();
            InitializeTable();
            initial_AddColourModel();
            Closing += MyWindow_Closing;
        }
        void initial_AddColourModel()
        {
            a[0] = new AddColourModel(ModelDatagrid0, Heng0, Shu0);
            radio0.IsChecked = true;
            a[1] = new AddColourModel(ModelDatagrid1,Heng1,Shu1);
            radio1.IsChecked = true;
            a[2] = new AddColourModel(ModelDatagrid2, Heng2, Shu2);
            radio2.IsChecked = true;
            a[3] = new AddColourModel(ModelDatagrid3, Heng3, Shu3);
            radio3.IsChecked = true;
            a[4] = new AddColourModel(ModelDatagrid4, Heng4, Shu4);
            radio4.IsChecked = true;
        }
        private void InitializeTable()
        {
            this.DataContext = this;

            myFrame.Navigate(new Uri("myPage\\Page3.xaml", UriKind.Relative));
            
            ShowCommonLib = new MyCommand(CommonLibshow);
        }    

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobalVariables.GetInstance();
                for (int i = 0; i!= 5;i++)
                {
                    if (a[i].SpecialModel.Count == 0)
                    {
                        MessageBox.Show($"第{i + 1}个模板为空");
                        return;
                    }
                    EllipseGrid grid = extract_data(a[i].SpecialModel);
                    /*
                    if (grid.Ellipsetable.Count == 0 )
                    {
                        MessageBox.Show($"第{i+1}个模板为空");
                        return ;
                    }            
                    */
                    GlobalVariables.GetInstance().tmp_MyDotTable.Add(grid);
                }
               
             
                // 将数据序列化为 JSON 字符串
                string jsonString = JsonConvert.SerializeObject(GlobalVariables.GetInstance().tmp_MyDotTable, Formatting.Indented);
                int area = int.Parse(Area.Text);
                // 将 JSON 字符串写入文件
                File.WriteAllText($"data{area}.json", jsonString);

                MessageBox.Show("Data saved to data.json");

                object currentPageContent = myFrame.Content;

                if (currentPageContent is Page3 myPage)
                {
                    myPage.CreatAllEllipseDatagrid(GlobalVariables.GetInstance().tmp_MyDotTable.Count - 5);
                    myPage.CreatAllEllipseDatagrid(GlobalVariables.GetInstance().tmp_MyDotTable.Count - 4);
                    myPage.CreatAllEllipseDatagrid(GlobalVariables.GetInstance().tmp_MyDotTable.Count - 3);
                    myPage.CreatAllEllipseDatagrid(GlobalVariables.GetInstance().tmp_MyDotTable.Count - 2);
                    myPage.CreatAllEllipseDatagrid(GlobalVariables.GetInstance().tmp_MyDotTable.Count - 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
            
        }

        private GridPoint startPoint;
        private GridPoint currentPoint;
      
        private void DataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            int rowIndex;
            int colIndex;
            DataGrid datagrid = sender as DataGrid;
            // 获取点击的元素
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && !(dep is DataGridCell || dep is TextBlock ))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep is DataGridCell cell )
            {
                // 获取行和列的索引
                rowIndex = datagrid.Items.IndexOf(cell.DataContext);
                colIndex = cell.Column.DisplayIndex;
                startPoint = new GridPoint(rowIndex, colIndex);
            }
            else
            {
                startPoint = null;
                if(dep is TextBlock textblock)
                {
                    MessageBox.Show("fgfg");
                }
                return;
            }
            string datagridName = datagrid.Name.ToString();
            int i;
            switch (datagridName)
            {
                case "ModelDatagrid0":
                    i = 0;

                    break;
                case "ModelDatagrid1":
                    i = 1;

                    break;
                case "ModelDatagrid2":
                    i = 2;
                    break;
                case "ModelDatagrid3":
                    i = 3;
                    break;
                case "ModelDatagrid4":
                    i = 4;
                    break;
                default:
                    return;
            }
            EllipseCellData cellData = a[i].SpecialModel[rowIndex][colIndex];
            if (cellData.FillColor != null)
            {
                if (cellData.FillColor == Brushes.Blue)
                {
                    cellData.FillColor = Brushes.Red;
                }
                else
                {
                    cellData.FillColor = Brushes.Blue;
                }
            }

        }
        private void DataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            
            int rowIndex;
            int colIndex;
            DataGrid datagrid = sender as DataGrid;
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep is DataGridCell cells)
            {
                // 获取行和列的索引
                rowIndex = datagrid.Items.IndexOf(cells.DataContext);
                colIndex = cells.Column.DisplayIndex;
                currentPoint = new GridPoint(rowIndex, colIndex);

            }

        }

        private void DataGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            if (startPoint == null || currentPoint == null)
            {
                return;
            }
            
            
           /* foreach (var row in SpecialModel)
            {
                foreach (var cell in row)
                {
                    cell.FillColor = null;
                }
            }*/
            int rowIndex, columnIndex;
            DataGrid datagrid = sender as DataGrid;
            string datagridName = datagrid.Name.ToString();
            int i;
            switch (datagridName)
            {
                case "ModelDatagrid0":
                    i = 0;

                    break;
                case "ModelDatagrid1":
                    i = 1;

                    break;
                case "ModelDatagrid2":
                    i = 2;
                    break;
                case "ModelDatagrid3":
                    i = 3;
                    break;
                case "ModelDatagrid4":
                    i = 4;
                    break;
                default:
                    return;
            }
            if (startPoint.IsEqual(currentPoint))
            {
               
               /* EllipseCellData cellData = a[i].SpecialModel[startPoint.x][startPoint.y];
                if (cellData.FillColor != null)
                {
                    if (cellData.FillColor == Brushes.Blue)
                    {
                        cellData.FillColor = Brushes.Red;
                    }
                    else
                    {
                        cellData.FillColor = Brushes.Blue;
                    }
                }*/
                return;
            }
            foreach (DataGridCellInfo cellInfo in datagrid.SelectedCells)
            {
                rowIndex = datagrid.Items.IndexOf(cellInfo.Item);
                columnIndex = cellInfo.Column.DisplayIndex;
                if (a[i].SpecialModel[rowIndex][columnIndex].FillColor != null)
                {
                    a[i].SpecialModel[rowIndex][columnIndex].FillColor = a[i].color;
                }
               
                
            }
            
        }
       
        private EllipseGrid extract_data(ObservableCollection<ObservableCollection<EllipseCellData>> MyModel)
        {
            bool flag;
           
            EllipseGrid CurrentModel = new EllipseGrid();
           
            foreach (var row in MyModel)
            {
                var currentrow = new ObservableCollection<EllipseCellData>();
                flag = false;
                foreach (var cell in row)
                {
                    if (cell.FillColor != null)
                    {
                        currentrow.Add(cell);
                        flag = true;
                    }
                }
                if (flag)
                {
                    CurrentModel.Ellipsetable.Add(currentrow);
                }

            }
            return CurrentModel;
        }

        private void CommonLibshow()
        {
            CommonLib commonlib = new CommonLib();
            commonlib.Show();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            string radioName = radioButton.GroupName.ToString();
            int i;
            switch (radioName)
            {
                case "Gender0":
                    i = 0;

                    break;
                case "Gender1":
                    i = 1;

                    break;
                case "Gender2":
                    i = 2;
                    break;
                case "Gender3":
                    i = 3;
                    break;
                case "Gender4":
                    i = 4;
                    break;
                default:
                
                    return;
            }
            if (radioButton.IsChecked == true)
            {
              
                if (radioButton.Content.ToString() == "红")
                {
                    a[i].color = Brushes.Red;
                }
                else
                {
                    a[i].color = Brushes.Blue;
                }
            }
        }

        private void GetCount(object sender, RoutedEventArgs e)
        {

            Button clickedButton = sender as Button;
            string buttonName = clickedButton.Name.ToString();
            
            int i;
            switch (buttonName)
            {
                case "Confirm0": 
                    i = 0;
                  
                    break;
                case "Confirm1":
                    i = 1;
                  
                    break;
                case "Confirm2":
                    i = 2;
                    break;
                case "Confirm3":
                    i = 3;

                    break;
                case "Confirm4":
                    i = 4;
                    break;
                default:
                    return;
            }
           
          
            a[i].Updata();
        }
        public void AreaBack(object sender, RoutedEventArgs e)
        {
            int area = int.Parse(Area.Text);
            if(area == 1)
            {
                MessageBox.Show("已是第一个模区");
            }else 
            {
              
                area--;
                Area.Text = $"{area}";
                string jsonContent = File.ReadAllText($"data{area}.json");

                GlobalVariables.GetInstance().tmp_MyDotTable = JsonConvert.DeserializeObject<List<EllipseGrid>>(jsonContent);
                if (GlobalVariables.GetInstance().tmp_MyDotTable == null)
                {
                    GlobalVariables.GetInstance().tmp_MyDotTable = new List<EllipseGrid>();

                }
             
                object currentPageContent = myFrame.Content;

                if (currentPageContent is Page3 myPage)
                {
                   
                    myFrame.Refresh();
                }
            }
            
            
        }
        public void AreaForword(object sender, RoutedEventArgs e)
        {
            int area = int.Parse(Area.Text);
            if (area == 5)
            {
                MessageBox.Show("已是最后一个模区");
            }
            else
            {
                area++;
                Area.Text = $"{area}";
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText($"data{area}.json");
             
                GlobalVariables.GetInstance().tmp_MyDotTable = JsonConvert.DeserializeObject<List<EllipseGrid>>(jsonContent);
                if (GlobalVariables.GetInstance().tmp_MyDotTable == null)
                {
                    GlobalVariables.GetInstance().tmp_MyDotTable = new List<EllipseGrid>();

                }
              
                object currentPageContent = myFrame.Content;

                if (currentPageContent is Page3 myPage)
                {
                    //myPage.ClearEllipseDatagrid();
                    // myFrame.Navigate(new Uri("myPage\\Page3.xaml", UriKind.Relative));
                    myFrame.Refresh();
                }
            }
            
        }
        public void DeleteConfirm(object sender, RoutedEventArgs e)
        {
            int from, to;
            try
            {
                from= int.Parse(From.Text);
                to = int.Parse(To.Text);              
            }
            catch (FormatException)
            {
                From.Text = "";
                To.Text = "";
                MessageBox.Show("请输入整数");
                return;
            }
            if(from > GlobalVariables.GetInstance().tmp_MyDotTable.Count || to > GlobalVariables.GetInstance().tmp_MyDotTable.Count || to < 1 || from < 1)
            {
                MessageBox.Show("请输入正确模板数");
                return;
            }
            object currentPageContent = myFrame.Content;

            if (currentPageContent is Page3 myPage)
            {
                for ( ; from <= to;to--)
                {
                    GlobalVariables.GetInstance().tmp_MyDotTable.RemoveAt(to - 1);
                    myPage.DeleteEllipseDatagrid(to - 1);

                }
               
                for(;from <= GlobalVariables.GetInstance().tmp_MyDotTable.Count; from++)
                {
                    myPage.RefreshText(from - 1);
                }
            }
            if (GlobalVariables.GetInstance().tmp_MyDotTable.Count != 0)
            {

                // 将数据序列化为 JSON 字符串
                string jsonString = JsonConvert.SerializeObject(GlobalVariables.GetInstance().tmp_MyDotTable, Formatting.Indented);
                int area = int.Parse(Area.Text);
                // 将 JSON 字符串写入文件
                File.WriteAllText($"data{area}.json", jsonString);
            }
            From.Text = "";
            To.Text = "";
        }

        private void MyWindow_Closing(object sender, CancelEventArgs e)
        {
            // 阻止窗口关闭
            e.Cancel = true;

            // 隐藏窗口，而不是关闭
            Visibility = Visibility.Hidden;

           
        }
    }

}
