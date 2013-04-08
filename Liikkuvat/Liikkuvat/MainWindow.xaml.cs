using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Liikkuvat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Ikkuna : Window
    {
        public int x;
        public int y;
        public Ikkuna()
        {

           InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           x= (int)e.GetPosition(this).X;
           // label1.Content = x;
        }
        public void setLabel(int xs){
            label1.Content=xs;
            Console.Beep();
        }
    }
}
