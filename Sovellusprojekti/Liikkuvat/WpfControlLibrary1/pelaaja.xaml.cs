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

namespace WpfControlLibrary1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class pelaaja : UserControl
    {
        public int vektorinpituus=4;
        public pelaaja()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        public int[] liikuta(int l, int k, double pelaajakulma)
        {
            int[] palautus = new int[2];
            palautus[0] =(int) (l + vektorinpituus * Math.Cos(pelaajakulma));
            palautus[1] = (int)(k + vektorinpituus * Math.Sin(pelaajakulma));
            return palautus;
        }
    }
}
