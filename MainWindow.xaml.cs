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
namespace Wpf_App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }
        private void ChangPassword(object sender, RoutedEventArgs e)
        {
            
            // 处理点击事件，比如打开密码更改窗口
            MessageBox.Show("Password change requested.");
        }
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 用户按下了回车键，执行确定操作
                e.Handled = true; // 防止触发默认操作，如在某些情况下会导致换行符被插入到文本框中
                DoLogin(); // 这里调用你的确定操作函数，比如登录操作
            }
        }
        private void loading_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }
        private void DoLogin()
        {
            string pswd = password.Password;
            if (pswd == "123")
            {
                catalog window1 = new catalog();
                window1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }
        private async void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            //await Task.Run(() => GlobalVariables.GetInstance());
            await Application.Current.Dispatcher.InvokeAsync(() => GlobalVariables.GetInstance());
           
        }
    }
}
