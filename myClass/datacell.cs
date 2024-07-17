using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_App.myClass
{

    
   public class DataCell: INotifyPropertyChanged
    {
        private int _value = 0;
        private SolidColorBrush _foreColor = null;
     
        public SolidColorBrush ForeColor
        {
            
           get { return _foreColor; }
            set
            {
                if (_foreColor != value)
                {
                    _foreColor = value;
                    OnPropertyChanged(nameof(ForeColor));
                }
            }
        }
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
        public DataCell()
        {

        }
        public DataCell(int a, SolidColorBrush b )
        {
            Value = a;
            ForeColor = b;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class EllipseCellData :INotifyPropertyChanged
    {
        private SolidColorBrush _fillColor = null;

        public SolidColorBrush FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                // 通过 PropertyChanged 通知界面更新
                OnPropertyChanged(nameof(FillColor));
            }
        }
        public int ColorToInt()
        {
            if (FillColor.Color == Brushes.Blue.Color)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class GridPoint
    {
        public int x;
        public int y;
        public GridPoint(int X,int Y)
        {
            x = X;
            y = Y;
        }
        public GridPoint()
        {

        }
        public bool IsEqual(GridPoint point)
        {
           
            if (point.x == this.x && point.y == this.y)
            {
                return true;
            }
           else
            {
                return false;
            }
        }
    }
   
    public class DigitalGridData
    {
        //public List<int> TableIndex = new List<int>();
        public DigitalGridData mother;
        public EllipseGrid motherDot;

        public List<int> Value = new List<int>();
        public List<int> ColumnDatas = new List<int>();
        //be used store every columns data counts add ,datas[0] represent OneData Count ,datas[4] represent ColumnDatas Greater and equal 2 
        public int[] Datas = new int[] { 0, 0, 0, 0, 0 };
        public int[] After4 = new int[] { 0, 0, 0 };
        public int After1 = 0;
        public int[] Before4 = new int[] { 0, 0, 0 };
        public int Before1 = 0;
        //第五层需要
        public int[] continuous = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        //直母需要
        public int[] zhimu_continuous = new int[] { 0,0,0,0};
        public DigitalGridData()
        {
            
        }
        /* public DigitalGridData(List<int> a, int i)
         {
             if(a != null)
             {
                 TableIndex.AddRange(a);

             }
             TableIndex.Add(i);
         }*/
        public DigitalGridData(DigitalGridData m, EllipseGrid mDot)
        {
            mother = m;
            motherDot = mDot;
        }
        public SolidColorBrush ToColour(int i)
        {
            if(Value[i] == 0)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.Blue;
            }
        }
        public void AddColumnDatas(bool flag)
        {
            //is or not new column
            if (flag)
            {
                ColumnDatas.Add(1);
                Datas[0]++;
            }
            else
            {
                if (ColumnDatas[ColumnDatas.Count - 1] < 4)
                {
                                  
                    Datas[ColumnDatas[ColumnDatas.Count - 1] -1]--;
                    
                    Datas[ColumnDatas[ColumnDatas.Count - 1]]++;
                   /* if (ColumnDatas[ColumnDatas.Count - 1] == 1)
                    {
                        Datas[4]++;
                    }*/
                }

                ColumnDatas[ColumnDatas.Count - 1]++;


            }
        }
        public void RemoveData()
        {
            Value.RemoveAt(Value.Count - 1);
            if (ColumnDatas[ColumnDatas.Count - 1] == 1)
            {
                ColumnDatas.RemoveAt(ColumnDatas.Count - 1);
                Datas[0]--;
            }
            else
            {
                if (ColumnDatas[ColumnDatas.Count - 1] < 4)
                {
                    Datas[ColumnDatas[ColumnDatas.Count - 1] - 1]--;
                    Datas[ColumnDatas[ColumnDatas.Count - 1] - 2]++;
                    if (ColumnDatas[ColumnDatas.Count - 1] == 1)
                    {
                        Datas[4]--;
                    }
                }
                ColumnDatas[ColumnDatas.Count - 1]--;

            }
        }
       public void Find4()
       {
            if(Datas[3] != 0)
            {
                for(int i = 0;i != ColumnDatas.Count; i++)
                {
                    if (ColumnDatas[ColumnDatas.Count - 1 - i] < 4)
                    {
                        After4[ColumnDatas[ColumnDatas.Count - 1 - i] - 1]++;
                    }
                    else
                    {
                        if(ColumnDatas[ColumnDatas.Count - 1 - i] == 4)
                        {
                            break;
                        }
                    }
                }

                Before4[0] = Datas[0] - After4[0];
                Before4[1] = Datas[1] - After4[1];
                Before4[2] = Datas[2] - After4[2];
                After4[0] = 0;
                After4[1] = 0;
                After4[2] = 0;
            }
            else
            {
                Before4[0] = 0;
                Before4[1] = 0;
                Before4[2] = 0;
            }
        }
       public void Find1()
        {
            if (Datas[0] != 0)
            {
                for (int i = 0; i != ColumnDatas.Count; i++)
                {
                    if (ColumnDatas[ColumnDatas.Count - 1 - i] > 1)
                    {
                        After1++;
                    }
                    else
                    {
                        break;
                        
                    }
                }
                Datas[4] = ColumnDatas.Count - Datas[0];
                Before1 = Datas[4] - After1;
                After1 = 0;
            }
            else
            {
                Before1 = 0;
            }
       }
       public void Sort(List<DigitalGridData> a ,List<DigitalGridData> b)
       {
            Find1();
            Find4();
            if (b.Count != GlobalVariables.N1)
            {
                BinarySearchInsert2(b, Before1);
                BinarySearchInsert1(a, Before4);
            }
            else
            {
                BinarySearchInsert2(b, Before1);
                BinarySearchInsert1(a, Before4);
               b.RemoveAt(b.Count - 1);
               a.RemoveAt(a.Count - 1);
            }
       }
        public void BinaryInsert_sixlayer1(List<DigitalGridData> sortedList, int a,int b,int max)
        {
            int left = 0;
            int right = sortedList.Count;
            int secondmax;
            if(max == 0)
            {
                secondmax = 1;
            }
            else
            {
                secondmax = 0;
            }
            if (sortedList.Count == 0)
            {
                sortedList.Insert(left, this);
                return;
            }

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid].Before4[max] <= a)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            while (left != sortedList.Count && sortedList[left].Before4[max] == a)
            {
                if (sortedList[left].Before4[secondmax] <= b)
                {
                    break;
                }
               
                left++;
            }
            sortedList.Insert(left, this);
            return;

        }
        public void BinaryInsert_sixlayer2(List<DigitalGridData> sortedList, int a, int b,int c ,int max,int second,int third)
        {
            int left = 0;
            int right = sortedList.Count;
            
            if (sortedList.Count == 0)
            {
                sortedList.Insert(left, this);
                return;
            }

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid].Before4[max] <= a)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            while (left != sortedList.Count && sortedList[left].Before4[max] == a)
            {
                if (sortedList[left].Before4[second] < b)
                {
                    break;
                }
                else
                {
                    if (sortedList[left].Before4[second] == b)
                    {
                        if (sortedList[left].Before4[third] <= c)
                        {
                            break;
                        }
                    }
                }

                left++;
            }
            sortedList.Insert(left, this);
            return;

        }
        private void BinarySearchInsert1(List<DigitalGridData> sortedList, int[] newValue)
        {
            int left = 0;
            int right = sortedList.Count;
            if (sortedList.Count == 0)
            {
                sortedList.Insert(left, this);
                return;
            }

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid].Before4[0] <= newValue[0])
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            
            while (left != sortedList.Count && sortedList[left].Before4[0] == newValue[0])
            {
                if(sortedList[left].Before4[1] < newValue[1])
                {
                    break;
                }
                else
                {
                    if (sortedList[left].Before4[1] == newValue[1])
                    {
                        if (sortedList[left].Before4[2] <= newValue[2])
                        {
                            break;
                        }
                    }
                }
                left++;
            }
            sortedList.Insert(left, this);
            return;
        }
        public void BinarySearchInsert2(List<DigitalGridData> sortedList, int newValue)
        {
            int left = 0;
            int right = sortedList.Count;
            if (sortedList.Count == 0)
            {
                sortedList.Insert(left, this);
                return ;
            }

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid].Before1 < newValue)
                {
                    right = mid;
                }
                else
                {
                   left = mid + 1;
                }
            }
            sortedList.Insert(left, this);
            return;
        }
      
        public void BinaryContinusInsert(List<DigitalGridData> sortedList, int newValue ,int index )
        {
            int left = 0;
            int right = sortedList.Count;
            if (sortedList.Count == 0)
            {
                sortedList.Insert(left, this);
                return;
            }

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid].continuous[index] < newValue)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            sortedList.Insert(left, this);
            return;
        }
        public void Continuous_Calculate()
        {
            for(int a = 0;a!= 8; a++)
            {
                continuous[a] = 0;
            }
            int i = ColumnDatas.Count - 1;
            while (i >= 0)
            {
                if (ColumnDatas[i] <= 2)
                {
                    i--;
                    continuous[0]++;
                }
                else
                {
                    break;
                }
            }
          
            i = ColumnDatas.Count - 1;
            while (i >= 0)
            {
                int final = ColumnDatas[i];
                if (ColumnDatas[i] <= 2 && ColumnDatas[i] == final)
                {
                    i--;
                    continuous[1]++;
                }
                else 
                {
                    break;
                }
            }

            i = ColumnDatas.Count - 1;
            while (i >= 0)
            {
                int final = ColumnDatas[i];
                if (ColumnDatas[i] <= 3 )
                {
                    i--;
                    continuous[2]++;
                }
                else
                {
                    break;
                }
            }
            i = ColumnDatas.Count - 1;
            while (i >= 0)
            {
               
                if (ColumnDatas[i] == 1)
                {
                    i--;
                    continuous[4]++;
                }
                else
                {
                    break;
                }
            }
            i = ColumnDatas.Count - 1;
            while (i >= 0)
            {

                if (ColumnDatas[i] >= 2)
                {
                    i--;
                    continuous[5]++;
                }
                else
                {
                    break;
                }
            }
            i = ColumnDatas.Count - 1;
            while (i >= 0)
            {

                if (ColumnDatas[i] >= 3)
                {
                    i--;
                    continuous[6]++;
                }
                else
                {
                    break;
                }
            }
            i = ColumnDatas.Count - 1;
            while (i >= 0)
            {

                if (ColumnDatas[i] >= 4)
                {
                    i--;
                    continuous[7]++;
                }
                else
                {
                    break;
                }
            }

        }
        public void zhimu_Calculate()
        {
            for (int a = 0; a != 4; a++)
            {
                zhimu_continuous[a] = 0;
            }
            int i = ColumnDatas.Count - 1;
            int flag = 1;
            while (i >= 0)
            {
                if (ColumnDatas[i] == 1)
                {
                    if (flag == 1)
                    {
                        zhimu_continuous[0]++;
                        if(i != 0)
                            zhimu_continuous[1] += ColumnDatas[i -1];
                    }
                    zhimu_continuous[2]++;
                    if (i != 0)
                        zhimu_continuous[3] += ColumnDatas[i - 1];
                    i -= 2;
                }
                else
                {
                    if (ColumnDatas[i] == 2 && flag == 1)
                    {
                        flag = 0;

                        zhimu_continuous[2]++;
                        if (i != 0)
                            zhimu_continuous[3] += ColumnDatas[i - 1];
                        i -= 2;
                    }
                    else
                        break;
                }
            }          
        }
        public void zhimu_BinarySearchInsert(List<DigitalGridData> sortedList, int newValue, int i)
        {
            int left = 0;
            int right = sortedList.Count;
            if (sortedList.Count == 0)
            {
                sortedList.Insert(left, this);
                return;
            }

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid].zhimu_continuous[i] < newValue)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            sortedList.Insert(left, this);
            return;
        }
        public  void test()
        {
            string a = "";
            int i = 0;

            while(i != 3)

            {
                a += 7;
                a += ' ';
                i++;
            }
            MessageBox.Show($"{a}");
        }
    }
    public class DigitalGrid
    {
        public ObservableCollection<ObservableCollection<DataCell>> digitaltable = new ObservableCollection<ObservableCollection<DataCell>>();
        public int rowindex = 0;
        public int columnindex = 0;
        public SolidColorBrush currentcolur ;
        public int ColumnHeadCount = 0;
        public List<GridPoint> gridPoints = new List<GridPoint>(); //当前digitaltable显示的数据
        public DigitalGridData digitalGridData = new DigitalGridData();
        public DigitalGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                var row = new ObservableCollection<DataCell>();
                for (int j = 0; j < 80; j++)
                {
                    row.Add(new DataCell());
                }
                digitaltable.Add(row);
            }
           
        }
        public DigitalGrid(DataGrid myDataGrid)
        {
            
            for (int i = 0; i < 6; i++)
            {
                var row = new ObservableCollection<DataCell>();
                for (int j = 0; j < GlobalVariables.MaxColumnCount; j++)
                {
                    row.Add(new DataCell());
                }
                digitaltable.Add(row);
            }
           
            myDataGrid.ItemsSource = digitaltable;
          
        }

        public void Connect(DataGrid myDataGrid)
        {
            myDataGrid.ItemsSource = digitaltable;

            

        }
        public void DisConnect(DataGrid myDataGrid)
        {
            myDataGrid.ItemsSource = null;
        }
        public void Flush(DigitalGridData data)
        {
           
            int i = 0;
            
            while(i != gridPoints.Count)
            {
                digitaltable[gridPoints[i].x][gridPoints[i].y].ForeColor = null;
                i++;
            }
            gridPoints.Clear();
            i = 0;
            rowindex = 0;
            columnindex = 0;
            ColumnHeadCount = 0;
            if (data.Value.Count == 0)
            {
                return;
            }
            SolidColorBrush color = data.ToColour(0);
            while (i != data.Value.Count)
            {
               
                if (rowindex != 6 && digitaltable[rowindex][columnindex].ForeColor == null)
                {
                    if(color == data.ToColour(i))
                    {
                        digitaltable[rowindex][columnindex].ForeColor = data.ToColour(i);
                        digitaltable[rowindex][columnindex].Value = i + 1;
                        gridPoints.Add(new GridPoint(rowindex, columnindex));
                        rowindex++;
                    }
                    else
                    {
                        color = data.ToColour(i);
                        rowindex = 0;

                        ColumnHeadCount++;
                        columnindex = ColumnHeadCount;
                        digitaltable[rowindex][columnindex].ForeColor = data.ToColour(i);
                        digitaltable[rowindex][columnindex].Value = i + 1;
                        gridPoints.Add(new GridPoint(rowindex, columnindex));
                        rowindex++;
                    }
                }
                else
                {
                    if(rowindex != 0)
                    {
                        rowindex--;
                        columnindex++;
                    }
                    else
                    {
                        while (digitaltable[rowindex][columnindex].ForeColor != null)
                        {
                            columnindex++;
                        }
                        columnindex--;
                        ColumnHeadCount = columnindex;
                    }
                    if( color == data.ToColour(i))
                    {
                        //此处有溢出
                      
                        digitaltable[rowindex][columnindex].ForeColor = data.ToColour(i);
                        digitaltable[rowindex][columnindex].Value = i + 1;
                        gridPoints.Add(new GridPoint(rowindex, columnindex));
                        rowindex++;
                    }
                    else
                    {
                        color = data.ToColour(i);
                        rowindex = 0;
                        ColumnHeadCount++;
                        columnindex = ColumnHeadCount;
                        digitaltable[rowindex][columnindex].ForeColor = data.ToColour(i);
                        digitaltable[rowindex][columnindex].Value = i + 1;
                        gridPoints.Add(new GridPoint(rowindex, columnindex));
                        rowindex++;
                    }
                }
                i++;
            }

            currentcolur = color;
            digitalGridData = data;
        }
      
    }

    public class EllipseGrid
    {
        public ObservableCollection<ObservableCollection<EllipseCellData>> Ellipsetable = new ObservableCollection<ObservableCollection<EllipseCellData>>();
       
        public EllipseGrid()
        {

           
        }
    }
}
