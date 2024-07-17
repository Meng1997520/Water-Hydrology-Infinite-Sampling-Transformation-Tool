using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Wpf_App.myClass;
using Wpf_App.myPage;
namespace Wpf_App
{
    /// <summary>
    /// catalog.xaml 的交互逻辑
    /// </summary>
    public partial class catalog : Window
    {

       
        public catalog()
        {
            InitializeComponent();
            
            DataContext = this;
           Closing += MyWindow_Closing;
        }
        public void Guan_show(object sender, RoutedEventArgs e)
        {

             /* object currentPageContent = mainFrame.Content;

              if (currentPageContent is Page4 myPage)
              {
                  Width = 215;
                  mainFrame.Navigate(null);
              }
              else
              {

                  mainFrame.Navigate(new Uri("myPage\\Page4.xaml", UriKind.Relative));
                  Width = 1000;
              }*/
            Button clickedButton = sender as Button;
            string buttonName = clickedButton.Content.ToString();

            int fristdimension = int.Parse( buttonName.Substring(1));
           
            Guan a = new Guan(fristdimension - 1);
            a.Show();
            
        }
        private void SetUp_Click(object sender, RoutedEventArgs e)
        {
            object currentPageContent = mainFrame.Content;

            if (currentPageContent is Setup myPage)
            {
                Width = 215;
                mainFrame.Navigate(null);
            }
            else
            {
                mainFrame.Navigate(new Uri("myPage\\Setup.xaml", UriKind.Relative));
                Width = 800;
            }
           
           

        }
        private void TotalMotherRoad_Click(object sender, RoutedEventArgs e)
        {
            /*object currentPageContent = mainFrame.Content;

            if (currentPageContent is MainLoad myPage)
            {
                Width = 215;
                mainFrame.Navigate(null);
            }
            else
            {
                mainFrame.Navigate(new Uri("myPage\\MainLoad.xaml", UriKind.Relative));
                Width = 1215;
            }*/
            LiveTarget.Instance.Show();
          

        }
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }
        private void MyWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定要关闭程序吗？", "关闭提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
            {
                // 取消关闭窗口
                e.Cancel = true;
            }
            else
            {
                // 用户点击确定，继续关闭窗口
                
            }
        }
        public void test(object sender, RoutedEventArgs e)
        {
           
            MessageBox.Show($"{ GlobalVariables.GetInstance().TwoLayerSortInt1.Count}");
            
        }

        private void Zhi_show(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string buttonName = clickedButton.Content.ToString();

            

            Zhi a = new Zhi(0);
            a.Show();
        }
    }
}
