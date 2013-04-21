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
using System.Threading;
using System.Collections;
using Zombi;

using WpfControlLibrary1;

namespace Liikkuvat
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public double k;
        public double l;
        public double kohdex;
        public double kohdey;
        RotateTransform r = new RotateTransform();
        public double pelaajakulma;
        public double zombikulma;
        
       ArrayList liikuta = new ArrayList();
       ArrayList Viivat = new ArrayList();
       ArrayList seinat = new ArrayList();
        public MainWindow()
        {

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0,100);
           

            InitializeComponent();
            UserControl1 z = new UserControl1(); 
            canvas1.Children.Add(z);
            //z.Name = "testiZombi";
            Canvas.SetTop(z, 100);
            Canvas.SetLeft(z, 100);
            liikuta.Add(z);
            piirraViiva(50, 50, 100, 100);
            dispatcherTimer.Start();
            
        }

        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           
            //Console.Beep();
            k = Canvas.GetTop((UIElement)this.pelaaja1);
            l = Canvas.GetLeft((UIElement)this.pelaaja1);
            double xsuunta= kohdex-l;
            double ysuunta= kohdey-k;
            label1.Content = xsuunta + ":" + ysuunta + "::" + pelaajakulma;
            pelaajakulma = laskeKulma(xsuunta, ysuunta);

      
           /*     
            r.Angle = pelaajakulma;
            label2.Content = pelaajakulma / Math.PI * 2 * 360;
            pelaaja1.RenderTransform = r;
            */
           kaikkiliikkuu();
        }
        /// <summary>
        /// Piirtää viivan. viiva liataan Viivat- arraylistaan ja Canvas1 lapsiin.
        /// </summary>
        /// <param name="x1">viivan ekapisteen x</param>
        /// <param name="x2">viivan tokan pisteen x</param>
        /// <param name="y1">viivan eka pisteen y</param>
        /// <param name="y2">viivan toka pisteen y</param>
        private void piirraViiva(double x1, double y1, double x2, double y2)
        {

            Line viiva = new Line();
            viiva.Visibility = Visibility.Visible;
            viiva.StrokeThickness = 1;
            viiva.X1 = x1;
            viiva.Y1 = y1;
            viiva.X2 = x2;
            viiva.Y2 = y2;
            viiva.Stroke = System.Windows.Media.Brushes.Black;
            canvas1.Children.Add(viiva);
            
            Viivat.Add(viiva);
        }
        /// <summary>
        /// kesken
        /// </summary>
        /// <param name="x1">kohteen alkuperäisen sijainnin x</param>
        /// <param name="y1">kohteen alkuperäisen sijainnin y</param>
        /// <param name="x2">kohteen uuden sijainnin x</param>
        /// <param name="y2">kohteen uuden sijainnin y</param>
        /// <returns>mennäämkö seinän läpi</returns>
        private bool tarkistaSeinat(double x1, double y1, double x2, double y2) {
           foreach(Line s in seinat){
              // siirretään piste s.x1 s.y1 origoksi ja otetaan ristitulo vektoreille s.x1 s.y1-> x2 y2
              //jos haluaisi jättää else puoliskon pois, pitäisi jana käydä aina samoin päin.
              //Nyt sillä ei pitäisi olla merkitystä.
               if (ristitulo(x2 - s.X1, y2 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) > 0)
               {
                   if (ristitulo(x1 - s.X1, y1 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) < 0) {
                       //tehdään vastaava tarkastelu toisesta viivasta käsin
                       if (ristitulo(s.X1-x1,s.Y1-y1, x2-x1, y2- y1) < 0)
                       {
                           if (ristitulo(s.X2-x1, s.Y2-y1, x2 - x1, y2 - y1) > 0)
                           {
                               return true;
                           }
                       }
                   }
               }
               else { 
                   //todo tapaus toisin päin
                   if(ristitulo(0,0,0,0)<0){
                       if (ristitulo(0, 0, 0, 0) > 0) {
                           return true;
                       }
                   }
               }
           
           }
      //todo
            return false;
        }
        /// <summary>
        /// palauttaa kahden vektorin välisen ristitulon 3 komponentin
        /// Z komponentti on merkattu nollaksi koska ollaan 2 ulotteisessa avaruudessa
        /// tämäntakia palauttaa yhden doublen
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"</param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public double ristitulo(double x1, double y1, double x2, double y2 ) {
            // z komponentit on merkatt unolliksi

            return x1*y2-y1*x2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xsuunta"></param>
        /// <param name="ysuunta"></param>
        /// <returns></returns>
        private double laskeKulma(double xsuunta, double ysuunta)
        {
            double kulma;
            if (xsuunta < 0)
            {
                //-x ja-y
                if (ysuunta < 0)
                {

                    kulma = Math.Acos(-xsuunta / Math.Sqrt(xsuunta * xsuunta + ysuunta * ysuunta)) + Math.PI;
                }
                //-x ja+y
                else
                {
                   kulma = Math.Asin(-xsuunta / Math.Sqrt(ysuunta * ysuunta + xsuunta * xsuunta)) + Math.PI / 2;

                }
            }
            else
            {
                //+x ja +y
                if (ysuunta > 0)
                {

                kulma = Math.Asin(ysuunta / Math.Sqrt(xsuunta * xsuunta + ysuunta * ysuunta));
                }
                //+X ja -y
                else
                {
                   kulma = Math.Asin(xsuunta / Math.Sqrt(xsuunta * xsuunta + ysuunta * ysuunta)) + (Math.PI * 3) / 2;
                }

            }
            return kulma;
        }
        /// <summary>
        /// Liikuttaa kaikkia siihen liikkuvat listan komponentteja sekä kaikkia pelaajaa
        /// </summary>
        private void kaikkiliikkuu()
        {
            double zkorkeutta = 0;
            double zleveytta = 0;
            double korkeutta = Canvas.GetTop((UIElement)this.pelaaja1);
            double leveytta = Canvas.GetLeft((UIElement)this.pelaaja1);
            if (tarkistaetaisyys(korkeutta, leveytta, kohdex, kohdey, pelaaja1.vektorinpituus))
            {
                //pelaaja1.pyorita(pelaajakulma);
                Canvas.SetLeft((UIElement)this.pelaaja1, this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[0]);


                Canvas.SetTop((UIElement)this.pelaaja1, this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[1]);
                RotateTransform r = new RotateTransform();
                
                r.Angle = pelaajakulma/ (Math.PI * 2) * 360+90;
                label2.Content = pelaajakulma;
                pelaaja1.RenderTransform = r;
              //  pelaaja1.RenderTransform.SetValue
    
            }
            foreach (liikkuva testi in liikuta)
            {

                zkorkeutta = Canvas.GetTop(testi as UIElement);
                zleveytta = Canvas.GetLeft(testi as UIElement);
                //label2.Content = leveytta + ":" + korkeutta; 
                if (tarkistaetaisyys(zkorkeutta, zleveytta, korkeutta, leveytta, testi.getvectorinpituus()))
                {
                    zombikulma = laskeKulma(leveytta - zleveytta, korkeutta - zkorkeutta);
                    Canvas.SetLeft(testi as UIElement, testi.liikuta(zleveytta, zkorkeutta, zombikulma)[0]);


                    Canvas.SetTop(testi as UIElement, testi.liikuta(zleveytta, zkorkeutta, zombikulma)[1]);
                }
                //label2.Content = testi.liikuta(leveytta, korkeutta, pelaajakulma)[0] + ":" + pelaajakulma; 
            }
        }
        /// <summary>
        /// Mahtuuko liikkumaan?
        /// </summary>
        /// <param name="korkeutta"></param>
        /// <param name="leveytta"></param>
        /// <param name="kohdex"></param>
        /// <param name="kohdey"></param>
        /// <param name="vektorinpituus"></param>
        /// <returns></returns>
        private bool tarkistaetaisyys(double korkeutta, double leveytta,double kohdex, double kohdey, double vektorinpituus)
        {

            if (((kohdex - leveytta) * (kohdex - leveytta) + (kohdey - korkeutta) * (kohdey - korkeutta)) > (vektorinpituus * vektorinpituus+2))
            {
                
                return true;
            }
           
            return false;
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
     
            kohdex = e.GetPosition(canvas1).X;
            kohdey = e.GetPosition(canvas1).Y;
        }
    }
}
