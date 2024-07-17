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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Wpf_App.myClass;


namespace Wpf_App.myPage
{
    /// <summary>
    /// MainLoad.xaml 的交互逻辑
    /// </summary>
    public partial class MainLoad : Page
    {
       
        DigitalGrid mainDigitalGrid;
        private int AddCount, RowIndex, ColumnIndex;
        int redcount = 0;
        int bluecount = 0;
        int slashcount = 0;
        int transversecount = 0;
        int vertialacount = 0;
        const int MaxRowCount = 6;
        const int MaxColumnCount = 40;
        //private int ColumnHeadCount = 0;
        public MyCommand showBlue { get; set; }
        public MyCommand showRed { get; set; }
        public MyCommand showSlash { get; set; }
        public MyCommand showTransverse { get; set; }
        public MyCommand showVertical { get; set; }
        public MyCommand showUndo { get; set; }
        public MyCommand showClear { get; set; }
        public MyCommand showrefresh { get; set; }
        public MainLoad()
        {
            GlobalVariables.GetInstance();
            InitializeComponent();
            RowIndex = 0;
            ColumnIndex = 0;
            AddCount = 0;
            showBlue = new MyCommand(BlueClick);
            showRed = new MyCommand(RedClick);
            showSlash = new MyCommand(slashClick);
            showTransverse = new MyCommand(TransverseClick);
            showVertical = new MyCommand(verticalClick);
            showUndo = new MyCommand(UndoClick);
            showClear = new MyCommand(ClearClick);
            //showrefresh = new MyCommand(refreshClick);

            this.DataContext = this;
            mainDigitalGrid = new DigitalGrid(dataGrid);

            var row = new ObservableCollection<DataCell>();
            for (int j = 0; j < MaxColumnCount; j++)
            {
                row.Add(new DataCell());
            }
            mainDigitalGrid.digitaltable.Add(row);

            for (int i = 0; i != MaxColumnCount; i += 5)
            {
                mainDigitalGrid.digitaltable[MaxRowCount][i].Value = i;
                mainDigitalGrid.digitaltable[MaxRowCount][i].ForeColor = Brushes.Purple;
            }

            // dataGrid.DataContext = tableData;GlobalVariables.MyDigitalTable
            myFrame.Navigate(new Uri("myPage\\Page1.xaml", UriKind.Relative));

            // 设置表格数据源
            //dataGrid.ItemsSource = tableData;
        }


        private void BlueClick()
        {
            AddCount++;
            bluecount++;
            BlueCount.Text = bluecount.ToString();
            if (mainDigitalGrid.currentcolur == null)
            {
                mainDigitalGrid.currentcolur = Brushes.Blue;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].Value = AddCount;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor = Brushes.Blue;
                dataGrid.Columns[0].Header = "0";
                mainDigitalGrid.gridPoints.Add(new GridPoint { x = RowIndex, y = ColumnIndex });

                flash();
                return;
            }
            if (mainDigitalGrid.currentcolur == Brushes.Red)
            {
                mainDigitalGrid.ColumnHeadCount++;
                ColumnIndex = mainDigitalGrid.ColumnHeadCount;
                RowIndex = 0;
                for (; mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor != null; ColumnIndex++)
                {
                }
                mainDigitalGrid.currentcolur = Brushes.Blue;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].Value = AddCount;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor = Brushes.Blue;
                mainDigitalGrid.gridPoints.Add(new GridPoint { x = RowIndex, y = ColumnIndex });
                for (int i = mainDigitalGrid.ColumnHeadCount; i >= 0; i--)
                {
                    dataGrid.Columns[mainDigitalGrid.ColumnHeadCount - i].Header = $"{i}";
                }
                flash();
                return;
            }

            if (RowIndex < MaxRowCount)
            {
                RowIndex++;
                if (mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor != null || RowIndex == MaxRowCount)
                {
                    RowIndex--;
                    ColumnIndex++;
                }
            }
            else
            {
                RowIndex--;
                ColumnIndex++;
            }
            mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].Value = AddCount;
            mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor = Brushes.Blue;
            mainDigitalGrid.gridPoints.Add(new GridPoint { x = RowIndex, y = ColumnIndex });
            flash();
        }


        private void RedClick()
        {
            AddCount++;
            redcount++;
            RedCount.Text = redcount.ToString();
            if (mainDigitalGrid.currentcolur == null)
            {
                mainDigitalGrid.currentcolur = Brushes.Red;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].Value = AddCount;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor = Brushes.Red;
                dataGrid.Columns[0].Header = "0";
                mainDigitalGrid.gridPoints.Add(new GridPoint { x = RowIndex, y = ColumnIndex });
                flash();
                return;
            }
            if (mainDigitalGrid.currentcolur == Brushes.Blue)
            {
                mainDigitalGrid.ColumnHeadCount++;
                ColumnIndex = mainDigitalGrid.ColumnHeadCount;
                RowIndex = 0;
                for (; mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor != null; ColumnIndex++)
                {
                }
                mainDigitalGrid.currentcolur = Brushes.Red;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].Value = AddCount;
                mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor = Brushes.Red;
                mainDigitalGrid.gridPoints.Add(new GridPoint { x = RowIndex, y = ColumnIndex });
                for (int i = mainDigitalGrid.ColumnHeadCount; i >= 0; i--)
                {
                    dataGrid.Columns[mainDigitalGrid.ColumnHeadCount - i].Header = $"{i}";
                }
                flash();
                return;
            }

            if (RowIndex < MaxRowCount)
            {
                RowIndex++;
                if (mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor != null || RowIndex == MaxRowCount)
                {
                    RowIndex--;
                    ColumnIndex++;
                }
            }
            else
            {
                RowIndex--;
                ColumnIndex++;
            }
            mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].Value = AddCount;
            mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor = Brushes.Red;
            mainDigitalGrid.gridPoints.Add(new GridPoint { x = RowIndex, y = ColumnIndex });

            flash();
        }


        private void slashClick()
        {

            if (mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor == null)
            {
                return;
            }
            DataGridTemplateColumn tempColumn = dataGrid.Columns[ColumnIndex] as DataGridTemplateColumn;

            FrameworkElement a = dataGrid.Columns[ColumnIndex].GetCellContent(dataGrid.Items[RowIndex]);

            Canvas cv = tempColumn.CellTemplate.FindName("canvas", a) as Canvas;
            foreach (Line line in cv.Children.OfType<Line>().ToList())
            {
                if (line.Stroke == Brushes.Orange)
                {
                    return;
                }
            }
            Line myLine = new Line
            {
                X1 = a.ActualWidth,
                Y1 = 0,
                X2 = 0,
                Y2 = a.ActualHeight,
                Stroke = Brushes.Orange,
                StrokeThickness = 2
            };
            cv.Children.Add(myLine);

            slashcount++;
            SlashCount.Text = slashcount.ToString();
        }
        private void TransverseClick()
        {
            if (mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor == null)
            {
                return;
            }
            DataGridTemplateColumn tempColumn = dataGrid.Columns[ColumnIndex] as DataGridTemplateColumn;

            FrameworkElement a = dataGrid.Columns[ColumnIndex].GetCellContent(dataGrid.Items[RowIndex]);

            Canvas cv = tempColumn.CellTemplate.FindName("canvas", a) as Canvas;
            foreach (Line line in cv.Children.OfType<Line>().ToList())
            {
                if (line.Stroke == Brushes.Green)
                {
                    return;
                }
            }
            Line myLine = new Line
            {
                X1 = 0,
                Y1 = a.ActualHeight / 2,
                X2 = a.ActualWidth,
                Y2 = a.ActualHeight / 2,
                Stroke = Brushes.Green,
                StrokeThickness = 2
            };
            cv.Children.Add(myLine);
            transversecount++;
            TransverseCount.Text = transversecount.ToString();
        }

        private void verticalClick()
        {
            if (mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor == null)
            {
                return;
            }
            DataGridTemplateColumn tempColumn = dataGrid.Columns[ColumnIndex] as DataGridTemplateColumn;

            FrameworkElement a = dataGrid.Columns[ColumnIndex].GetCellContent(dataGrid.Items[RowIndex]);

            Canvas cv = tempColumn.CellTemplate.FindName("canvas", a) as Canvas;
            foreach (Line line in cv.Children.OfType<Line>().ToList())
            {
                if (line.Stroke == Brushes.Aqua)
                {
                    return;
                }
            }
            Line myLine = new Line
            {
                X1 = a.ActualWidth / 2,
                Y1 = 0,
                X2 = a.ActualWidth / 2,
                Y2 = a.ActualHeight,
                Stroke = Brushes.Aqua,
                StrokeThickness = 2
            };
            cv.Children.Add(myLine);
            vertialacount++;
            VertialaCount.Text = vertialacount.ToString();
        }

        private void UndoClick()
        {

            if (mainDigitalGrid.gridPoints.Count > 0)
            {
                AddCount--;
                int x = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 1].x;
                int y = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 1].y;
                DataCell lastDataCell;
                lastDataCell = mainDigitalGrid.digitaltable[x][y]; //TablePoint[TablePoint.Count - 1];
                if (lastDataCell.ForeColor == Brushes.Blue)
                {
                    bluecount--;
                    BlueCount.Text = bluecount.ToString();
                }
                else
                {
                    redcount--;
                    RedCount.Text = redcount.ToString();
                }
                lastDataCell.ForeColor = null;
                lastDataCell.Value = 0;
                mainDigitalGrid.gridPoints.RemoveAt(mainDigitalGrid.gridPoints.Count - 1);
                if (mainDigitalGrid.gridPoints.Count > 0)
                {
                    RowIndex = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 1].x;
                    ColumnIndex = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 1].y;
                }
                if (x == 0)
                {
                    if (mainDigitalGrid.currentcolur != mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor)
                    {
                        dataGrid.Columns[mainDigitalGrid.ColumnHeadCount].Header = "";
                        if (mainDigitalGrid.ColumnHeadCount != 0)
                        {
                            mainDigitalGrid.ColumnHeadCount--;
                            for (int i = mainDigitalGrid.ColumnHeadCount; i >= 0; i--)
                            {
                                dataGrid.Columns[mainDigitalGrid.ColumnHeadCount - i].Header = $"{i}";
                            }
                        }
                        mainDigitalGrid.currentcolur = mainDigitalGrid.digitaltable[RowIndex][ColumnIndex].ForeColor;
                    }


                }
                DataGridTemplateColumn tempColumn = dataGrid.Columns[y] as DataGridTemplateColumn;

                FrameworkElement a = dataGrid.Columns[y].GetCellContent(dataGrid.Items[x]);

                Canvas cv = tempColumn.CellTemplate.FindName("canvas", a) as Canvas;
                foreach (Line line in cv.Children.OfType<Line>().ToList())
                {
                    if (line.Stroke == Brushes.Orange)
                    {
                        slashcount--;
                        SlashCount.Text = slashcount.ToString();

                    }
                    else
                    if (line.Stroke == Brushes.Green)
                    {
                        transversecount--;
                        TransverseCount.Text = transversecount.ToString();
                    }
                    else
                    if (line.Stroke == Brushes.Aqua)
                    {
                        vertialacount--;
                        VertialaCount.Text = vertialacount.ToString();
                    }
                    cv.Children.Remove(line);
                }

                UndoDataReFresh();
            }
        }

        private void UndoDataReFresh()
        {
            for (int i = 0; i != GlobalVariables.MyDigitalTable.Count; i++)
            {

                int x = GlobalVariables.MyDigitalTable[i].gridPoints[GlobalVariables.MyDigitalTable[i].gridPoints.Count - 1].x;
                int y = GlobalVariables.MyDigitalTable[i].gridPoints[GlobalVariables.MyDigitalTable[i].gridPoints.Count - 1].y;
                GlobalVariables.MyDigitalTable[i].digitaltable[x][y].ForeColor = null;
                if (x == 0)
                {
                    GlobalVariables.MyDigitalTable[i].ColumnHeadCount--;
                }
                GlobalVariables.MyDigitalTable[i].gridPoints.RemoveAt(GlobalVariables.MyDigitalTable[i].gridPoints.Count - 1);
                if (GlobalVariables.MyDigitalTable[i].gridPoints.Count != 0)
                {

                    x = GlobalVariables.MyDigitalTable[i].gridPoints[GlobalVariables.MyDigitalTable[i].gridPoints.Count - 1].x;
                    y = GlobalVariables.MyDigitalTable[i].gridPoints[GlobalVariables.MyDigitalTable[i].gridPoints.Count - 1].y;
                    GlobalVariables.MyDigitalTable[i].currentcolur = GlobalVariables.MyDigitalTable[i].digitaltable[x][y].ForeColor;
                    GlobalVariables.MyDigitalTable[i].rowindex = x;
                    GlobalVariables.MyDigitalTable[i].columnindex = y;
                }

            }
            for (int i = 0; i != GlobalVariables.MyDigitalGridData.Count; i++)
            {
                GlobalVariables.MyDigitalGridData[i].Value.RemoveAt(GlobalVariables.MyDigitalGridData[i].Value.Count - 1);
            }
        }
        private void ClearClick()
        {
            while (mainDigitalGrid.gridPoints.Count != 0)
            {
                UndoClick();
            }
        }
        public void refreshClick(object sender, RoutedEventArgs e)
        {

            foreach (DigitalGrid table in GlobalVariables.MyDigitalTable)
            {
                table.digitaltable[1][0].ForeColor = Brushes.Black;
                table.digitaltable[1][0].Value = 8;
            }
            MessageBox.Show($"{ GlobalVariables.MyDotTable.Count}");
        }



        private void flash()
        {

            for (int i = 0; i != GlobalVariables.MyDigitalTable.Count; i++)
            {
                myTableReFresh(GlobalVariables.MyDotTable[i], GlobalVariables.MyDigitalTable[i]);
            }
            for (int i = 0; i != GlobalVariables.MyDigitalGridData.Count; i++)
            {
                myDigitalDataReFresh(GlobalVariables.MyDotTable[i], GlobalVariables.MyDigitalGridData[i]);
            }
        }
        private void myDigitalDataReFresh(EllipseGrid ellipseCellData, DigitalGridData digitalGridData)
        {
            int dotRow, dotColumn;

            if (RowIndex == 0)
            {
                if (digitalGridData.Value.Count == 0)
                {
                    digitalGridData.Value.Add(ellipseCellData.Ellipsetable[0][0].FillColor);
                    return;
                }
                dotRow = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 2].x + 1;
                dotColumn = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 2].y;
                dotColumn = dotColumn % ellipseCellData.Ellipsetable[0].Count;
                if (ellipseCellData.Ellipsetable[dotRow][dotColumn].FillColor.Color == digitalGridData.Value[digitalGridData.Value.Count - 1].Color)
                {
                    if (digitalGridData.Value[digitalGridData.Value.Count - 1] == Brushes.Red)
                    {
                        digitalGridData.Value.Add(Brushes.Blue);
                    }
                    else
                    {
                        digitalGridData.Value.Add(Brushes.Red);
                    }
                }
                else
                {
                    digitalGridData.Value.Add(digitalGridData.Value[digitalGridData.Value.Count - 1]);
                }
            }
            else
            {
                dotColumn = ColumnIndex % ellipseCellData.Ellipsetable[0].Count;
                if (ellipseCellData.Ellipsetable[RowIndex][dotColumn].FillColor.Color == digitalGridData.Value[digitalGridData.Value.Count - 1].Color)
                {
                    digitalGridData.Value.Add(digitalGridData.Value[digitalGridData.Value.Count - 1]);
                }
                else
                {
                    if (digitalGridData.Value[digitalGridData.Value.Count - 1] == Brushes.Red)
                    {
                        digitalGridData.Value.Add(Brushes.Blue);
                    }
                    else
                    {
                        digitalGridData.Value.Add(Brushes.Red);
                    }
                }
            }
            return;
        }
        private void myTableReFresh(EllipseGrid ellipseCellData, DigitalGrid digitalTable)
        {
            int dotRow, dotColumn;
            if (RowIndex == 0)
            {
                if (ColumnIndex == 0)
                {
                    digitalTable.currentcolur = ellipseCellData.Ellipsetable[RowIndex][ColumnIndex].FillColor;
                }
                else
                {
                    dotRow = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 2].x + 1;
                    dotColumn = mainDigitalGrid.gridPoints[mainDigitalGrid.gridPoints.Count - 2].y;
                    dotColumn = dotColumn % ellipseCellData.Ellipsetable[0].Count;
                    if (ellipseCellData.Ellipsetable[dotRow][dotColumn].FillColor.Color == digitalTable.currentcolur.Color)
                    {
                        digitalTable.rowindex = 0;
                        digitalTable.ColumnHeadCount++;
                        digitalTable.columnindex = digitalTable.ColumnHeadCount;
                        if (digitalTable.currentcolur == Brushes.Red)
                        {
                            digitalTable.currentcolur = Brushes.Blue;
                        }
                        else
                        {
                            digitalTable.currentcolur = Brushes.Red;
                        }
                    }
                    else
                    {
                        digitalTable.rowindex = digitalTable.gridPoints[digitalTable.gridPoints.Count - 1].x + 1;
                        digitalTable.columnindex = digitalTable.gridPoints[digitalTable.gridPoints.Count - 1].y;

                    }
                }

            }
            else
            {
                dotColumn = ColumnIndex % ellipseCellData.Ellipsetable[0].Count;
                if (digitalTable.currentcolur.Color == ellipseCellData.Ellipsetable[RowIndex][dotColumn].FillColor.Color)
                {
                    digitalTable.rowindex++;

                }

                else
                {
                    digitalTable.ColumnHeadCount++;
                    digitalTable.columnindex = digitalTable.ColumnHeadCount;
                    digitalTable.rowindex = 0;
                    if (digitalTable.currentcolur.Color == Brushes.Red.Color)
                    {
                        digitalTable.currentcolur = Brushes.Blue;
                    }
                    else
                    {
                        digitalTable.currentcolur = Brushes.Red;
                    }

                }

            }
            if (digitalTable.rowindex == 6 || digitalTable.digitaltable[digitalTable.rowindex][digitalTable.columnindex].ForeColor != null)
            {

                digitalTable.rowindex--;
                digitalTable.columnindex++;

                if (digitalTable.rowindex == -1)
                {
                    digitalTable.rowindex = 0;
                    while (digitalTable.digitaltable[digitalTable.rowindex][digitalTable.columnindex].ForeColor != null)
                    {
                        digitalTable.columnindex++;
                    }
                }
            }

            digitalTable.digitaltable[digitalTable.rowindex][digitalTable.columnindex].ForeColor =
                    digitalTable.currentcolur;

            digitalTable.digitaltable[digitalTable.rowindex][digitalTable.columnindex].Value = AddCount;
            digitalTable.gridPoints.Add(new GridPoint { x = digitalTable.rowindex, y = digitalTable.columnindex });

        }

        public void UpPage(object sender, RoutedEventArgs e)
        {
            object currentPageContent = myFrame.Content;

            if (currentPageContent is Page1 myPage)
            {
                myPage.UpPage();
            }
        }
        public void DownPage(object sender, RoutedEventArgs e)
        {
            object currentPageContent = myFrame.Content;

            if (currentPageContent is Page1 myPage)
            {
                myPage.DownPage();
            }
        }
    }
}
