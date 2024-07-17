using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Wpf_App
{
    class MainViewModel : INotifyPropertyChanged
    {
       public MainViewModel()
       {
           
       }
        

       public event PropertyChangedEventHandler PropertyChanged;
      
       
    }
}