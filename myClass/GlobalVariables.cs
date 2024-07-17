using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Wpf_App.myClass
{
    public class GlobalVariables
    {

        public static int N1 = 20;
        public const int MaxRowCount = 6;
        public const int MaxColumnCount = 40;
        private static GlobalVariables Instance;
        public static List<DigitalGrid> MyDigitalTable = new List<DigitalGrid>();
        public static List<EllipseGrid> MyDotTable ;
        public List<EllipseGrid> tmp_MyDotTable;
        public List<EllipseGrid> special_MyDot;
        public List<EllipseGrid> zhimu_MyDot;
        //
        public List<List<EllipseGrid>> Model_Area = new List<List<EllipseGrid>>();
       
        //

        public static List<DigitalGridData> MyDigitalGridData = new List<DigitalGridData>();
        public  List<DigitalGridData> OneLayerSortInt1 = new List<DigitalGridData>();
        public List<DigitalGridData> OneLayerSortInt2 = new List<DigitalGridData>();
        public static List<DigitalGridData> MyTwoLayerData = new List<DigitalGridData>();
        public List<DigitalGridData> TwoLayerSortInt1 = new List<DigitalGridData>();
      
        public List<DigitalGridData> TwoLayerSortInt2 = new List<DigitalGridData>();

        public static List<DigitalGridData> FourLayerData = new List<DigitalGridData>();//invalid
        public List<DigitalGridData> ThreeLayerSortInt1 = new List<DigitalGridData>();

        public List<DigitalGridData> ThreeLayerSortInt2 = new List<DigitalGridData>();
        public List<DigitalGridData> FourLayerSortInt1 = new List<DigitalGridData>();

        public List<DigitalGridData> FourLayerSortInt2 = new List<DigitalGridData>();
        public List<List<DigitalGridData>> FiveLayerSort = new List<List<DigitalGridData>>();
        public List<List<DigitalGridData>> SixLayerSort = new List<List<DigitalGridData>>();
        public List<List<DigitalGridData>> Guan = new List<List<DigitalGridData>>();

        public List<List<DigitalGridData>> Zhi_pre = new List<List<DigitalGridData>>();
        public List<List<DigitalGridData>> Zhi = new List<List<DigitalGridData>>();

        public static GlobalVariables GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (Instance == null)
            {
                Instance = new GlobalVariables();
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText("data1.json");
                
                MyDotTable = JsonConvert.DeserializeObject<List<EllipseGrid>>(jsonContent);
                if(MyDotTable == null)
                {
                    MyDotTable = new List<EllipseGrid>();
                  
                }
                Instance.tmp_MyDotTable = MyDotTable;
                Instance.special_MyDot = MyDotTable;
                Instance.zhimu_MyDot = MyDotTable;
                //initialize FiveLayerSort
                for (int i = 0; i != 8; i++)
                {
                    List<DigitalGridData> a = new List<DigitalGridData>();
                    Instance.FiveLayerSort.Add(a);
                }
                //initialize SixLayerSort
                for (int i = 0; i != 16; i++)
                {
                    List<DigitalGridData> a = new List<DigitalGridData>();
                    Instance.SixLayerSort.Add(a);
                }
                //initialize Gun
                for (int i = 0; i != 16; i++)
                {
                    List<DigitalGridData> a = new List<DigitalGridData>();
                    Instance.Guan.Add(a);
                }
                for (int i = 0; i != 24; i++)
                {
                    List<DigitalGridData> a = new List<DigitalGridData>();
                    Instance.Zhi.Add(a);
                }
                for (int i = 0; i != 6; i++)
                {
                    List<DigitalGridData> a = new List<DigitalGridData>();
                    Instance.Zhi_pre.Add(a);
                }
                /*  for (int i = 0; i != 10; i++)
                  {
                      for (int j = 0; j != 30; j++)
                      {
                          MyDotTable.Add(MyDotTable[j]);
                      }
                  }*/
                for (int i = 0; i != MyDotTable.Count; i++)
                {
                    MyDigitalGridData.Add(new DigitalGridData(null, MyDotTable[i]));
                   /* for(int j = 0; j != MyDotTable.Count; j++)
                    {
                        MyTwoLayerData.Add(new DigitalGridData(MyDigitalGridData[i].TableIndex,j));
                    }*/
                }
               // ThreeLayorInitial();
            }
            return Instance;
        }
        private static void ThreeLayorInitial()
        {
            for (int i = 0; i != MyDotTable.Count; i++)
            {
                for (int j = 0; j != N1; j++)
                {
                    DigitalGridData a = new DigitalGridData();
                    FourLayerData.Add(a);
                }
                for (int j = 0; j != N1; j++)
                {
                    DigitalGridData a = new DigitalGridData();
                    FourLayerData.Add(a);
                }
            }
        }
        public void OneLayerCalculate()
        {
            OneLayerSortInt1.Clear();
            OneLayerSortInt2.Clear();
            for (int i = 0; i != MyDotTable.Count; i++)
            {
                MyDigitalGridData[i].Sort(OneLayerSortInt1, OneLayerSortInt2);

            }
        }
        public void TwoLayerCalculate()
        {
            TwoLayerSortInt1.Clear();
            TwoLayerSortInt2.Clear();
            for (int i = 0; i != MyDotTable.Count; i++)
            {
                for (int j = 0; j != OneLayerSortInt1.Count; j++)
                {
                    DigitalGridData temp = new DigitalGridData(OneLayerSortInt1[j], MyDotTable[i]);
                    CalculateTest(MyDotTable[i], OneLayerSortInt1[j], temp);
                    temp.Sort(TwoLayerSortInt1, TwoLayerSortInt2);
                }
                for (int j = 0; j != OneLayerSortInt2.Count; j++)
                {
                    DigitalGridData temp = new DigitalGridData(OneLayerSortInt2[j], MyDotTable[i]);
                    CalculateTest(MyDotTable[i], TwoLayerSortInt2[j], temp);
                    temp.Sort(TwoLayerSortInt1, TwoLayerSortInt2);
                }
            }
        }
        public void ThreeLayerCalculate()
        {
            ThreeLayerSortInt1.Clear();
            ThreeLayerSortInt2.Clear();
            for (int i = 0; i != MyDotTable.Count; i++)
            {
                for (int j = 0; j != TwoLayerSortInt1.Count; j++)
                {
                    DigitalGridData temp = new DigitalGridData(TwoLayerSortInt1[j], MyDotTable[i]);
                    CalculateTest(MyDotTable[i],TwoLayerSortInt1[j], temp);
                    temp.Sort(ThreeLayerSortInt1, ThreeLayerSortInt2);
                }
                for(int j = 0; j != TwoLayerSortInt2.Count; j++)
                {
                    DigitalGridData temp = new DigitalGridData(TwoLayerSortInt2[j], MyDotTable[i]);
                    CalculateTest(MyDotTable[i], TwoLayerSortInt2[j], temp);
                    temp.Sort(ThreeLayerSortInt1, ThreeLayerSortInt2);
                }
            }
        }
        public void FourLayerCalculate()
        {
            FourLayerSortInt1.Clear();
            FourLayerSortInt2.Clear();
            for (int i = 0; i != MyDotTable.Count; i++)
            {
                for (int j = 0; j != ThreeLayerSortInt1.Count; j++)
                {
                    DigitalGridData temp = new DigitalGridData(ThreeLayerSortInt1[j], MyDotTable[i]);
                    CalculateTest(MyDotTable[i], ThreeLayerSortInt1[j], temp);
                    temp.Sort(FourLayerSortInt1, FourLayerSortInt2);
                }
                for (int j = 0; j != ThreeLayerSortInt2.Count; j++)
                {
                    DigitalGridData temp = new DigitalGridData(ThreeLayerSortInt2[j], MyDotTable[i]);
                    CalculateTest(MyDotTable[i], ThreeLayerSortInt2[j], temp);
                    temp.Sort(FourLayerSortInt1, FourLayerSortInt2);
                }
            }
            
        }
        //copy data from b to a
        private void Data_Copy(List<DigitalGridData> a, List<DigitalGridData> b)
        {
            for(int i = 0;i!= b.Count; i++)
            {

               /* DigitalGridData temp = new DigitalGridData();
                temp.mother = b[i].mother;
                temp.motherDot = b[i].motherDot;
                for(int j = 0; j != b[i].Value.Count; j++)
                {
                    temp.Value.Add(b[i].Value[j]);
                }
                for (int j = 0; j != b[i].ColumnDatas.Count; j++)
                {
                    temp.ColumnDatas.Add(b[i].ColumnDatas[j]);
                }
                for(int j = 0;j!= 5; j++)
                {
                    temp.Datas[j] = b[i].Datas[j];
                }
                for (int j = 0; j != 3; j++)
                {
                    temp.After4[j] = b[i].After4[j];
                    temp.Before4[j] = b[i].Before4[j];
                }
                temp.After1 = b[i].After1;
                temp.Before1 = b[i].Before1;*/
                a.Add(b[i]);
            }

        }
        public void FiveLayerCalculate()
        {
           // MessageBox.Show($"{ FourLayerSortInt1.Count}");
            for (int j = 0; j != 8; j++)
            {
                FiveLayerSort[j].Clear();

            }
           // MessageBox.Show($"{ FourLayerSortInt1.Count}");
            for (int i = 0;i != FourLayerSortInt1.Count; i++)
            {
                FourLayerSortInt1[i].Continuous_Calculate();
                for(int j = 0;j!= 8; j++)
                {
                    if (j == 3)
                    {
                        j++;
                    }
                       
                    FourLayerSortInt1[i].BinaryContinusInsert(FiveLayerSort[j], FourLayerSortInt1[i].continuous[j],j);

                }

            }
            for (int i = 0; i != FourLayerSortInt2.Count; i++)
            {
                FourLayerSortInt2[i].Continuous_Calculate();
                for (int j = 0; j != 8; j++)
                {
                    if (j == 3)
                        j++;
                    FourLayerSortInt2[i].BinaryContinusInsert(FiveLayerSort[j], FourLayerSortInt2[i].continuous[j],j);
                }
            }
            //sort again 
           
            for (int i = 0; i != SixLayerSort.Count; i++)
            {
                SixLayerSort[i].Clear();
            }
           for (int i = 0;i != FiveLayerSort[0].Count; i++)
           {
                FiveLayerSort[0][i].BinaryInsert_sixlayer1(SixLayerSort[0], FiveLayerSort[0][i].Datas[0], FiveLayerSort[0][i].Datas[1],0);
                FiveLayerSort[0][i].BinaryInsert_sixlayer1(SixLayerSort[1], FiveLayerSort[0][i].Datas[1], FiveLayerSort[0][i].Datas[0], 1);
              
           }
            for (int i = 0; i != FiveLayerSort[1].Count; i++)
            {
                FiveLayerSort[1][i].BinaryInsert_sixlayer1(SixLayerSort[2], FiveLayerSort[1][i].Datas[0], FiveLayerSort[1][i].Datas[1], 0);
                FiveLayerSort[1][i].BinaryInsert_sixlayer1(SixLayerSort[3], FiveLayerSort[1][i].Datas[1], FiveLayerSort[1][i].Datas[0], 1);
            }
            for (int i = 0; i != FiveLayerSort[2].Count; i++)
            {          
                FiveLayerSort[2][i].BinaryInsert_sixlayer2(SixLayerSort[4], FiveLayerSort[2][i].Datas[0], FiveLayerSort[2][i].Datas[1], FiveLayerSort[2][i].Datas[2],0,1,2 );
                FiveLayerSort[2][i].BinaryInsert_sixlayer2(SixLayerSort[5], FiveLayerSort[2][i].Datas[1], FiveLayerSort[2][i].Datas[0], FiveLayerSort[2][i].Datas[2], 1, 0, 2);
                FiveLayerSort[2][i].BinaryInsert_sixlayer2(SixLayerSort[6], FiveLayerSort[2][i].Datas[2], FiveLayerSort[2][i].Datas[1], FiveLayerSort[2][i].Datas[0], 2, 1, 0);
                FiveLayerSort[2][i].BinaryInsert_sixlayer2(SixLayerSort[7], FiveLayerSort[2][i].Datas[1], FiveLayerSort[2][i].Datas[2], FiveLayerSort[2][i].Datas[0], 1, 2, 0);

            }
            //FiveLayerSort[3] = FourLayerSortInt1;
            Data_Copy(FiveLayerSort[3],FourLayerSortInt1);
            for(int j = 0;j!= FourLayerSortInt2.Count; j++)
            {
                FiveLayerSort[3].Add(FourLayerSortInt2[j]);
            }
            for (int i = 0; i != FiveLayerSort[3].Count; i++)
            {
                FiveLayerSort[3][i].BinaryInsert_sixlayer2(SixLayerSort[8], FiveLayerSort[3][i].Datas[0], FiveLayerSort[3][i].Datas[1], FiveLayerSort[3][i].Datas[2], 0, 1, 2);
                FiveLayerSort[3][i].BinaryInsert_sixlayer2(SixLayerSort[9], FiveLayerSort[3][i].Datas[1], FiveLayerSort[3][i].Datas[0], FiveLayerSort[3][i].Datas[2], 1, 0, 2);
                FiveLayerSort[3][i].BinaryInsert_sixlayer2(SixLayerSort[10], FiveLayerSort[3][i].Datas[2], FiveLayerSort[3][i].Datas[1], FiveLayerSort[3][i].Datas[0], 2, 1, 0);
                FiveLayerSort[3][i].BinaryInsert_sixlayer2(SixLayerSort[11], FiveLayerSort[3][i].Datas[1], FiveLayerSort[3][i].Datas[2], FiveLayerSort[3][i].Datas[0], 1, 2, 0);

            }
            //SixLayerSort[12] = FiveLayerSort[4];
            //SixLayerSort[13] = FiveLayerSort[5];
            //SixLayerSort[14] = FiveLayerSort[6];
            //SixLayerSort[15] = FiveLayerSort[7];
            Data_Copy(SixLayerSort[12], FiveLayerSort[4]);
            Data_Copy(SixLayerSort[13], FiveLayerSort[5]);
            Data_Copy(SixLayerSort[14], FiveLayerSort[6]);
            Data_Copy(SixLayerSort[15], FiveLayerSort[7]);
            // MessageBox.Show($"{ FiveLayerSort[7].Count}");
            //input special model and straight mother simple model
            for (int i = 0; i != 16; i++)
            {
               Guan[i].Clear();
            }
            for (int j = 0; j != 16; j++)
            {
                for (int i = 0; i != SixLayerSort[j].Count; i++)
                {
                    for(int k = 0;k!= special_MyDot.Count; k++)
                    {
                        DigitalGridData temp = new DigitalGridData(SixLayerSort[j][i], special_MyDot[k]);

                        CalculateTest(special_MyDot[k], SixLayerSort[j][i], temp);
                        Guan[j].Add(temp);
                        
                    }
                  
                }

            }
            
        }
        public void ZhiLayerCalculate()
        {
            for (int i = 0; i != 24; i++)
            {
                Zhi[i].Clear();
            }
            //
            for(int i = 0; i != 6; i++)
            {
                Zhi_pre[i].Clear();
            }
            Zhi_pre[0].AddRange(SixLayerSort[0]);
            Zhi_pre[0].AddRange(SixLayerSort[2]);
            Zhi_pre[1].AddRange(SixLayerSort[1]);
            Zhi_pre[1].AddRange(SixLayerSort[3]);

            for(int i = 4;i!= 8; i++)
            {
                Zhi_pre[i - 2].AddRange(SixLayerSort[i]);
                Zhi_pre[i - 2].AddRange(SixLayerSort[i+4]);
            }
            //
            for(int index = 0;index != 6; index++)
            {
                for (int i = 0;i!= Zhi_pre[index].Count; i++)
                {
                    for(int j =0;j!= zhimu_MyDot.Count; j++)
                    {
                        DigitalGridData temp = new DigitalGridData(Zhi_pre[index][i], zhimu_MyDot[j]);
                        CalculateTest(zhimu_MyDot[j], Zhi_pre[index][i], temp);
                        temp.zhimu_Calculate();
                        for(int k =0;k!= 4; k++)
                        {
                            temp.zhimu_BinarySearchInsert(Zhi[4*index+k], temp.zhimu_continuous[k], k);
                        }
                    }
                }
            }      
            
        }
        public void Calculate(EllipseGrid ellipseGrid, DigitalGridData digitalInput, DigitalGridData digitalOutput)
        {
            int dotRow, dotColumn;

            if (digitalInput.ColumnDatas[digitalInput.ColumnDatas.Count - 1] == 1)
            {
                if (digitalOutput.Value.Count == 0)
                {
                    if (ellipseGrid.Ellipsetable[0][0].FillColor == Brushes.Red)
                    {
                        digitalOutput.Value.Add(0);
                    }
                    else
                    {
                        digitalOutput.Value.Add(1);
                    }
                    digitalOutput.ColumnDatas.Add(1);
                    digitalOutput.Datas[0]++;
                    return;
                }
                dotRow = digitalInput.ColumnDatas[digitalInput.ColumnDatas.Count - 2];
                dotRow = dotRow % ellipseGrid.Ellipsetable.Count;
                dotColumn = digitalInput.ColumnDatas.Count - 2;
                dotColumn = dotColumn % ellipseGrid.Ellipsetable[0].Count;
                if (ellipseGrid.Ellipsetable[dotRow][dotColumn].FillColor.Color == digitalOutput.ToColour(digitalOutput.Value.Count - 1).Color)
                {
                    if (digitalOutput.Value[digitalOutput.Value.Count - 1] == 0)
                    {
                        digitalOutput.Value.Add(1);
                    }
                    else
                    {
                        digitalOutput.Value.Add(0);
                    }
                    digitalOutput.AddColumnDatas(true);
                }
                else
                {
                    digitalOutput.Value.Add(digitalOutput.Value[digitalOutput.Value.Count - 1]);
                    digitalOutput.AddColumnDatas(false);
                }
            }
            else
            {
                dotRow = digitalInput.ColumnDatas[digitalInput.ColumnDatas.Count - 1] - 1;
                dotRow = dotRow % ellipseGrid.Ellipsetable.Count;
                dotColumn = (digitalInput.ColumnDatas.Count - 1) % ellipseGrid.Ellipsetable[0].Count;
                if (ellipseGrid.Ellipsetable[dotRow][dotColumn].FillColor.Color == digitalOutput.ToColour(digitalOutput.Value.Count - 1).Color)
                {
                    digitalOutput.Value.Add(digitalOutput.Value[digitalOutput.Value.Count - 1]);
                    digitalOutput.AddColumnDatas(false);
                }
                else
                {
                    if (digitalOutput.Value[digitalOutput.Value.Count - 1] == 0)
                    {
                        digitalOutput.Value.Add(1);
                    }
                    else
                    {
                        digitalOutput.Value.Add(0);
                    }
                    digitalOutput.AddColumnDatas(true);
                }
            }
            return;
        }

        private void CalculateTest(EllipseGrid ellipseGrid, DigitalGridData digitalInput, DigitalGridData digitalOutput)
        {
            digitalOutput.mother = digitalInput;
            int dotRow = 1;
            int dotColumn = 0;
            int i = 1;
            int currentcolour;
            if (ellipseGrid.Ellipsetable[0][0].FillColor.Color == Brushes.Red.Color)
            {

                digitalOutput.Value.Add(0);
                currentcolour = 0;
                digitalOutput.ColumnDatas.Add(1);
                digitalOutput.Datas[0]++;
            }
            else
            {
                digitalOutput.Value.Add(1);
                currentcolour = 1;
                digitalOutput.ColumnDatas.Add(1);
                digitalOutput.Datas[0]++;
            }

            while (i != digitalInput.Value.Count)
            {
                dotColumn = dotColumn % ellipseGrid.Ellipsetable[0].Count;
                dotRow = dotRow % ellipseGrid.Ellipsetable.Count;
                if (digitalInput.Value[i] == digitalInput.Value[i - 1])
                {
                    if (ellipseGrid.Ellipsetable[dotRow][dotColumn].ColorToInt() == currentcolour)
                    {
                        digitalOutput.Value.Add(currentcolour);
                        digitalOutput.AddColumnDatas(false);
                        dotRow++;

                    }
                    else
                    {
                        if (currentcolour == 1)
                        {
                            currentcolour = 0;
                        }
                        else
                        {
                            currentcolour = 1;
                        }
                        // currentcolour = ellipseGrid.Ellipsetable[dotRow][dotColumn].ColorToInt();
                        // dotRow = 1;
                        // dotColumn++;
                        digitalOutput.AddColumnDatas(true);
                        digitalOutput.Value.Add(currentcolour);
                        dotRow++;
                    }
                }
                else
                {
                    if (ellipseGrid.Ellipsetable[dotRow][dotColumn].ColorToInt() == currentcolour)
                    {
                        if (currentcolour == 1)
                        {
                            currentcolour = 0;
                        }
                        else
                        {
                            currentcolour = 1;
                        }
                        //currentcolour = ellipseGrid.Ellipsetable[dotRow][dotColumn].ColorToInt();
                        dotRow = 1;
                        dotColumn++;
                        digitalOutput.AddColumnDatas(true);
                        digitalOutput.Value.Add(currentcolour);
                    }
                    else
                    {
                        digitalOutput.Value.Add(currentcolour);
                        digitalOutput.AddColumnDatas(false);
                        dotRow = 1;
                        dotColumn++;
                    }
                }
                i++;
            }
        }

    }
}
