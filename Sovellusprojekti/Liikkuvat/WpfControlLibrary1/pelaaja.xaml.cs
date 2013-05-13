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

namespace Pelaaja
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class pelaaja : UserControl
    {
        public double vektorinpituus=1;
        //double jakojaannosx;
        //double jakojaannosy;
        public pelaaja()
        {
          //  jakojaannosx = 0;
           // jakojaannosy = 0;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        public double[] liikuta(double l, double k, double pelaajakulma)
        {
           
         double[] palautus = new double[2];
          //+x ja +y
            if (pelaajakulma < Math.PI/2)
            {
                double apux = (vektorinpituus * Math.Cos(pelaajakulma)); //+jakojaannosx;

                //jakojaannosx = apux -  (int)Math.Round(apux);
                palautus[0] = l+apux;
                double apuy = (vektorinpituus * Math.Sin(pelaajakulma)); //+jakojaannosy;
                //jakojaannosy = apuy - (int)Math.Round(apuy);
                palautus[1] = k+apuy;
            }
            else
            {
                
                //-x ja +y
                if (pelaajakulma < Math.PI)

                {
                    double apux = -(vektorinpituus * Math.Sin(pelaajakulma - Math.PI / 2));// +jakojaannosx;

                   // jakojaannosx = apux - (int)Math.Round(apux);
                    palautus[0] =l+ apux;
                    double apuy =(vektorinpituus * Math.Cos(pelaajakulma - Math.PI / 2));//+jakojaannosy);
                    //jakojaannosy = apuy - (int)Math.Round(apuy);
                    palautus[1] = k + apuy;
                
                  //   palautus[0]=l;
                  //  palautus[1]=k;
                }
                else
                {
                   //-x ja -y
                    if (pelaajakulma < 3 * Math.PI / 2)
                    {
                       
                        double apux = -(vektorinpituus * Math.Cos(pelaajakulma - Math.PI ));// +jakojaannosx;

                        //jakojaannosx = apux - (int)Math.Round(apux);
                        palautus[0] = l+apux;
                        double apuy = -(vektorinpituus * Math.Sin(pelaajakulma - Math.PI ));// +jakojaannosy;
                      //  jakojaannosy = apuy - (int)(int)Math.Round(apuy);
                        palautus[1] = k+apuy;
                    

                    }
                        //x ja -y
                    else {

                        double apux = (vektorinpituus * Math.Sin(pelaajakulma - 3*Math.PI/2));// + jakojaannosx;

                        //jakojaannosx = apux - (int)Math.Round(apux);
                        palautus[0] = l + apux;
                        double apuy = -(vektorinpituus * Math.Cos(pelaajakulma - 3*Math.PI/2));// + jakojaannosy;
                        //jakojaannosy = apuy - (int)(int)Math.Round(apuy);
                        palautus[1] = k + apuy;
                    }
                
                }  
            }

            return palautus;
        }
    }
}
