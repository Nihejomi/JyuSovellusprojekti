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
using Undying;
using Peli;
using SoundSystem;

using Pelaaja;

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
        public string kaupunki;
        bool eteen = false;
        bool taakse = false;
        private double zoomi = 1;
        
        RotateTransform r = new RotateTransform();
       
        public double pelaajakulma;
        public double zombikulma;
        Peli.Peli testi;
        ArrayList rakennukset = new ArrayList();
       ArrayList liikuta = new ArrayList();
       ArrayList Viivat = new ArrayList();
       ArrayList seinat = new ArrayList();


        Soundsystem soundsystem;

        public MainWindow()
        {

           
            
        }
        /// <summary>
        /// Alustetaan kaupunki jossa zombit riehuu, zombien määrä kaupungin nimen mukaan.
        /// Todo hae pakka nimen mukaan
        /// </summary>
        /// <param name="p"></param>
        private void alusta(string p)
        {
            // resot jotka voidaan myöhemmin sitoa johonkin muuttujaan.
            int resox = 1000;
            int resoy = 1000;
            int zombeja;
            HTMLParser.Parsinta tietoja = new HTMLParser.Parsinta();
            tietoja.Alusta(p);
            zombeja = tietoja.zombieMaara/30;
                        
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //label2.Content = zombeja;

            InitializeComponent();

            Canvas.SetTop(pelaaja1, this.Height  / 2 + pelaaja1.ActualHeight / 2);
            Canvas.SetLeft(pelaaja1, this.Left / 2 + pelaaja1.ActualWidth / 2);
            testi = new Peli.Peli(62.23407, 25.73577, 62.24372, 25.76086, 2000, 2000, true);  // lisäsin viimeisen parametrin teiden poistoon latauksen nopeuttamiseksi (true = ei teitä, false = tiet mukaan)
            //testi = new Peli.Peli(62.24, 25.73, 62.26, 25.75, 2000, 2000);//
            // testi= new Peli.Peli(62.2330, 25.733, 62.2335, 25.7335,(int)this.Width,(int)this.Height);

            //rakennukset = testi.annaAlueRakennukset(62.2330, 25.733, 62.2335, 25.7335);


            soundsystem = new Soundsystem();

            piirraRuohot();
            piirraTiet();
            piirraRakennukset();
            piirraVesistot();
            // Lisataan zombit
            Zombit lauma = new Zombit();
            int zombejasisalla = 0;
            for (int i = 0; i < zombeja; i++)
            {

                int[] ominaisuudet = lauma.arvoZombi(resox, resoy);
                if (!(OnkoRakennuksessa(ominaisuudet[0], ominaisuudet[1])))
                {

                    Zombi z = new Zombi(new Vector(ominaisuudet[0], ominaisuudet[1]));

                    Canvas.SetZIndex(z, 9000 + i);
                    //z.Name = "testiZombi";
                    Canvas.SetTop(z, ominaisuudet[1]);
                    Canvas.SetLeft(z, ominaisuudet[0]);
                    (z as UIElement).RenderTransformOrigin = new Point(0.5, 0.5);
                    canvas1.Children.Add(z);
                    liikuta.Add(z);
                }
                else
                {
                    zombejasisalla++;


                }
            }
            label2.Content = zombejasisalla;
            dispatcherTimer.Start();
           
        }
        

        private void piirraVesistot()
        {
            SolidColorBrush sininen = new SolidColorBrush();
            sininen.Color = Colors.Blue;
            for (int f = 0; f < testi.annaVesiLkm() - 1; f++)
            {
                Peli.Vesi kohde = testi.annaVesi(f);

                PointCollection pisteet = new PointCollection();
                for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                {
                    pisteet.Add(new Point(kohde.annaVektori(i).x, kohde.annaVektori(i).y));
                    piirraSeina(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                    //  piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                }

                Polygon talo = new Polygon();
                talo.Points = pisteet;

                talo.Fill = sininen;

                canvas1.Children.Add(talo);


                /*  for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                  {
                    
                      piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                  }*/
            }
        }
        /// <summary>
        /// tässä vaiheessa piirtää testimielessä rakennukset, mutta ei tee niistä vielä seiniä.
        /// </summary>
        private void piirraRakennukset()
        {
            SolidColorBrush harmaa = new SolidColorBrush();
            harmaa.Color = Colors.Gray;
            double maxy;
            double miny;
            double maxx;
            double minx;
           for (int f = 0; f < testi.annaRakennusLkm()-1; f++)
            {

                Peli.Rakennus kohde = testi.annaRakennus(f);

               PointCollection pisteet = new PointCollection();
               maxx = kohde.annaVektori(0).x;
               minx = kohde.annaVektori(0).x;
               maxy = kohde.annaVektori(0).y;
               miny = kohde.annaVektori(0).y;
               for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                     {
                   //mitataan hitbox optimointia varten
                         if (kohde.annaVektori(i).x > maxx) {
                             maxx = kohde.annaVektori(i).x;
                         }
                         if (kohde.annaVektori(i).x < minx)
                         {
                             minx = kohde.annaVektori(i).x;
                         }
                         if (kohde.annaVektori(i).y > maxy)
                         {
                             maxy = kohde.annaVektori(i).y;
                         }
                         if (kohde.annaVektori(i).y < miny)
                         {
                             miny = kohde.annaVektori(i).y;
                         }

    pisteet.Add(new Point(kohde.annaVektori(i).x,kohde.annaVektori(i).y));
    //piirraSeina(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
     //  piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
   }
               kohde.maxx = maxx;
               kohde.minx = minx;
               kohde.maxy = maxy;
               kohde.miny = miny;

                  Polygon talo = new Polygon();
                  talo.Points = pisteet;

                  talo.Fill = harmaa;
                  rakennukset.Add(kohde);
                  canvas1.Children.Add(talo);
              

              /*  for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                {
                    
                    piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                }*/
            }
        }

        private void piirraRuohot()
        {
            SolidColorBrush vihrea = new SolidColorBrush();
            vihrea.Color = Colors.ForestGreen;
            SolidColorBrush tummavihrea = new SolidColorBrush();
            tummavihrea.Color = Colors.DarkGreen;
            SolidColorBrush suovihrea = new SolidColorBrush();
            suovihrea.Color = Colors.DarkOliveGreen;


            for (int f = 0; f < testi.annaRuohoLkm() - 1; f++)
            {
                Peli.Ruoho kohde = testi.annaRuoho(f);

                PointCollection pisteet = new PointCollection();

                for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                {
                    pisteet.Add(new Point(kohde.annaVektori(i).x, kohde.annaVektori(i).y));
                    //piirraSeina(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                    //  piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                }

                Polygon ruoho = new Polygon();
                ruoho.Points = pisteet;
                if (kohde.annaTyyppi() == 0)
                    ruoho.Fill = vihrea;
                if (kohde.annaTyyppi() == 1)
                    ruoho.Fill = tummavihrea;
                if (kohde.annaTyyppi() == 2)
                    ruoho.Fill = suovihrea;



                canvas1.Children.Add(ruoho);


                /*  for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                  {
                    
                      piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                  }*/
            }
        }

        private void piirraTiet()
        {
            SolidColorBrush harmaa = new SolidColorBrush();
            harmaa.Color = Colors.DarkGray;
            for (int f = 0; f < testi.annaTieLkm() - 1; f++)
            {
                Peli.Tie kohde = testi.annaTie(f);

                PointCollection pisteet = new PointCollection();

                for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                {
                    //  
                    //piirraSeina(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                  // if(kohde.annaTyyppi() == 0)
                piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                 //  if(kohde.annaTyyppi() == 1)
                   //     pisteet.Add(new Point(kohde.annaVektori(i).x, kohde.annaVektori(i).y));
                }
                /*
                if (kohde.annaTyyppi() == 1)
                {
                    System.Console.WriteLine("tiepolygoni");
                    Polygon tie = new Polygon();
                    tie.Points = pisteet;
                    tie.Fill = harmaa;
                    canvas1.Children.Add(tie);
              }

                */
                /*  for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                  {
                    
                      piirraViiva(kohde.annaVektori(i).x, kohde.annaVektori(i).y, kohde.annaVektori(i + 1).x, kohde.annaVektori(i + 1).y);
                  }*/
            }
        }
 

        /// <summary>
        /// tämä ei tällä hetkellä piirrä seinää, mutta tekee sen törmäystarkistusta varten.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private void piirraSeina(double x1, double y1, double x2, double y2)
        {
            Line viiva = new Line();
            viiva.X1 = x1;
            viiva.Y1 = y1;
            viiva.X2 = x2;
            viiva.Y2 = y2;
            seinat.Add(viiva);
        }
        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
         
           if(Keyboard.IsKeyDown(Key.Add))
           {
               if (zoomi < 5) 
               {
                 
                   
                       zoomi = zoomi + 0.05;
                     
                 
            
               }
           }
           if (Keyboard.IsKeyDown(Key.Subtract))
           {
               if (zoomi > 1)
               {
                
                       zoomi = zoomi - 0.05;
         
               }
           }
           if (Keyboard.IsKeyDown(Key.A))
           {
               pelaajakulma = pelaajakulma - 0.05;
           }
           if (Keyboard.IsKeyDown(Key.D))
           {
               pelaajakulma = pelaajakulma + 0.05;
           }
           if (Keyboard.IsKeyDown(Key.W))
           {
               eteen = true;
           }
           else { eteen = false; }
           if (Keyboard.IsKeyDown(Key.S))
           {
               eteen = false;
               taakse = true;
           }
           else {
               taakse = false;
           }
            //Key.Add.
            //Console.Beep();
            k = Canvas.GetTop((UIElement)this.pelaaja1);
            l = Canvas.GetLeft((UIElement)this.pelaaja1);
            double xsuunta= kohdex-l;
            double ysuunta= kohdey-k;
          
           // pelaajakulma = laskeKulma(xsuunta, ysuunta);

      
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
        private bool tarkistaSeinat(double x1, double y1, double x2, double y2)
        {
            foreach (Rakennus R in rakennukset)
            {
                if((R.maxx>=x2)&&(x2>=R.minx)&&(R.maxy>=y2)&&(y2>=R.miny)){
                for (int i = 0; i+1 < R.annaVektoriLkm(); i++)
                {
                    Line s = new Line();
                    s.X1=R.annaVektori(i).x;
                    s.Y1=R.annaVektori(i).y;
                    s.X2=R.annaVektori(i+1).x;
                    s.Y2=R.annaVektori(i+1).y;
                  
                    // siirretään piste s.x1 s.y1 origoksi ja otetaan ristitulo vektoreille s.x1 s.y1-> x2 y2
                    //jos haluaisi jättää else puoliskon pois, pitäisi jana käydä aina samoin päin.
                    //Nyt sillä ei pitäisi olla merkitystä.
                    if (ristitulo(x2 - s.X1, y2 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) > 0)
                    {
                        if (ristitulo(x1 - s.X1, y1 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) < 0)
                        {
                            //tehdään vastaava tarkastelu toisesta viivasta käsin
                            if (ristitulo(s.X1 - x1, s.Y1 - y1, x2 - x1, y2 - y1) > 0)
                            {

                                if (ristitulo(s.X2 - x1, s.Y2 - y1, x2 - x1, y2 - y1) < 0)
                                {
                                    //Console.Beep();
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ristitulo(x1 - s.X1, y1 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) > 0)
                        {

                            //tehdään vastaava tarkastelu toisesta viivasta käsin
                            if (ristitulo(s.X1 - x1, s.Y1 - y1, x2 - x1, y2 - y1) < 0)
                            {

                                if (ristitulo(s.X2 - x1, s.Y2 - y1, x2 - x1, y2 - y1) > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                //tehdään viimeinnen linja for silmukan ulkopuolella niin säästyy yksi iffi
                Line v = new Line();
                v.X1 = R.annaVektori(R.annaVektoriLkm()-1).x;
                v.Y1 = R.annaVektori(R.annaVektoriLkm()-1).y;
                v.X2 = R.annaVektori(0).x;
                v.Y2 = R.annaVektori(0).y;

                if (ristitulo(x2 - v.X1, y2 - v.Y1, v.X2 - v.X1, v.Y2 - v.Y1) > 0)
                {
                    if (ristitulo(x1 - v.X1, y1 - v.Y1, v.X2 - v.X1, v.Y2 - v.Y1) < 0)
                    {
                        //tehdään vastaava tarkastelu toisesta viivasta käsin
                        if (ristitulo(v.X1 - x1, v.Y1 - y1, x2 - x1, y2 - y1) > 0)
                        {

                            if (ristitulo(v.X2 - x1, v.Y2 - y1, x2 - x1, y2 - y1) < 0)
                            {
                                //Console.Beep();
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (ristitulo(x1 - v.X1, y1 - v.Y1, v.X2 - v.X1, v.Y2 - v.Y1) > 0)
                    {

                        //tehdään vastaava tarkastelu toisesta viivasta käsin
                        if (ristitulo(v.X1 - x1, v.Y1 - y1, x2 - x1, y2 - y1) < 0)
                        {

                            if (ristitulo(v.X2 - x1, v.Y2 - y1, x2 - x1, y2 - y1) > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                }
            }
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
            // z komponentit on merkattu nolliksi

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
        /// Kertoo onko piste rakennuksen sisalla.
        /// Todo: voi joutua tarkistelemaan reunaehtoja
        /// </summary>
        /// <param name="x2">tarkisettavan pisteen x</param>
        /// <param name="y2">tarkistettavan pisteen y</param>
        /// <returns></returns>
        private bool OnkoRakennuksessa(double x2, double y2) {
           

            foreach (Rakennus R in rakennukset)
            {
                int seinia = 0;
                double y1 = R.maxy+1;
                double x1 = R.maxx+1;
                if ((R.maxx >= x2) && (x2 >= R.minx) && (R.maxy >= y2) && (y2 >= R.miny))
                {
                    for (int i = 0; i + 1 < R.annaVektoriLkm(); i++)
                    {
                        Line s = new Line();
                        s.X1 = R.annaVektori(i).x;
                        s.Y1 = R.annaVektori(i).y;
                        s.X2 = R.annaVektori(i + 1).x;
                        s.Y2 = R.annaVektori(i + 1).y;

                        // siirretään piste s.x1 s.y1 origoksi ja otetaan ristitulo vektoreille s.x1 s.y1-> x2 y2
                        //jos haluaisi jättää else puoliskon pois, pitäisi jana käydä aina samoin päin.
                        //Nyt sillä ei pitäisi olla merkitystä.
                        if (ristitulo(x2 - s.X1, y2 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) >= 0)
                        {
                            if (ristitulo(x1 - s.X1, y1 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) <= 0)
                            {
                                //tehdään vastaava tarkastelu toisesta viivasta käsin
                                if (ristitulo(s.X1 - x1, s.Y1 - y1, x2 - x1, y2 - y1) >= 0)
                                {

                                    if (ristitulo(s.X2 - x1, s.Y2 - y1, x2 - x1, y2 - y1) <= 0)
                                    {
                                        //Console.Beep();
                                        seinia++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ristitulo(x1 - s.X1, y1 - s.Y1, s.X2 - s.X1, s.Y2 - s.Y1) >= 0)
                            {

                                //tehdään vastaava tarkastelu toisesta viivasta käsin
                                if (ristitulo(s.X1 - x1, s.Y1 - y1, x2 - x1, y2 - y1) <= 0)
                                {

                                    if (ristitulo(s.X2 - x1, s.Y2 - y1, x2 - x1, y2 - y1) >= 0)
                                    {
                                        seinia++;
                                    }
                                }
                            }
                        }
                    }
                    //tehdään viimeinnen linja for silmukan ulkopuolella niin säästyy yksi iffi
                    Line v = new Line();
                    v.X1 = R.annaVektori(R.annaVektoriLkm() - 1).x;
                    v.Y1 = R.annaVektori(R.annaVektoriLkm() - 1).y;
                    v.X2 = R.annaVektori(0).x;
                    v.Y2 = R.annaVektori(0).y;

                    if (ristitulo(x2 - v.X1, y2 - v.Y1, v.X2 - v.X1, v.Y2 - v.Y1) >= 0)
                    {
                        if (ristitulo(x1 - v.X1, y1 - v.Y1, v.X2 - v.X1, v.Y2 - v.Y1) <= 0)
                        {
                            //tehdään vastaava tarkastelu toisesta viivasta käsin
                            if (ristitulo(v.X1 - x1, v.Y1 - y1, x2 - x1, y2 - y1) >= 0)
                            {

                                if (ristitulo(v.X2 - x1, v.Y2 - y1, x2 - x1, y2 - y1) <= 0)
                                {
                                    //Console.Beep();
                                    seinia++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ristitulo(x1 - v.X1, y1 - v.Y1, v.X2 - v.X1, v.Y2 - v.Y1) >= 0)
                        {

                            //tehdään vastaava tarkastelu toisesta viivasta käsin
                            if (ristitulo(v.X1 - x1, v.Y1 - y1, x2 - x1, y2 - y1) <= 0)
                            {

                                if (ristitulo(v.X2 - x1, v.Y2 - y1, x2 - x1, y2 - y1) >= 0)
                                {
                                    seinia++;
                                }
                            }
                        }
                    }
                }
                if (seinia % 2 != 0)
                {
                    return true;
                }
            }
        
            return false;
        }
        /// <summary>
        /// Liikuttaa kaikkia siihen liikkuvat listan komponentteja sekä kaikkia pelaajaa
        /// </summary>
        private void kaikkiliikkuu()
        {
            zoomaa();
            double korkeutta = Canvas.GetTop((UIElement)this.pelaaja1);
            double leveytta = Canvas.GetLeft((UIElement)this.pelaaja1);
            RotateTransform r = new RotateTransform(pelaajakulma / (Math.PI * 2) * 360 + 90, pelaaja1.ActualHeight / 2, pelaaja1.ActualWidth / 2);
            pelaaja1.RenderTransform = r;
            if (eteen)
            {
                if (tarkistaSeinat(Canvas.GetLeft(this.pelaaja1)+pelaaja1.ActualWidth/2, Canvas.GetTop(this.pelaaja1)+pelaaja1.ActualHeight/2, pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[0]+pelaaja1.ActualWidth/2, this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[1]+pelaaja1.ActualHeight/2))
                { }
                else
                {
                    
                   //pelaaja1.pyorita(pelaajakulma);

                    leveytta = this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[0];
                    Canvas.SetLeft((UIElement)this.pelaaja1, leveytta);

                    korkeutta = this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[1];
                    Canvas.SetTop((UIElement)this.pelaaja1, korkeutta);

                    // pitäs saada noiden 10 paikalle user controllin leveys ja korkeus, mutta jostain syystä ei toimi oikein... ottaakohan kuvan korkeuden? Kuva on isompi kuin kontrollli.
             


                   
                    
                    //  pelaaja1.RenderTransform.SetValue
                }
            }
            if (taakse)
            {
                if (tarkistaSeinat(Canvas.GetLeft(this.pelaaja1) + pelaaja1.ActualWidth / 2, Canvas.GetTop(this.pelaaja1) + pelaaja1.ActualHeight / 2, pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma - Math.PI)[0] + pelaaja1.ActualWidth / 2, this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma - Math.PI)[1] + pelaaja1.ActualHeight / 2))
                { }
                else
                {

                    //pelaaja1.pyorita(pelaajakulma);

                    leveytta = this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma - Math.PI)[0];
                    Canvas.SetLeft((UIElement)this.pelaaja1, leveytta);

                    korkeutta = this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma - Math.PI)[1];
                    Canvas.SetTop((UIElement)this.pelaaja1, korkeutta);

                }
            }
            foreach (liikkuva zombie in liikuta) 
            {
                UIElement uiZombie = zombie as UIElement;
                Vector zombiePos = zombie.getPosition();

                    //Console.Beep();

                Vector playerPos = new Vector(leveytta, korkeutta);
                Vector newPos = zombie.possibleMove(playerPos);
                zombikulma = laskeKulma(leveytta - newPos.X, korkeutta - newPos.Y);
                
                //pitäs saada dynäämisesti tuo zombin leveys otettua, vois laskea kun tietää canvas elementin koon ja leftin ja rightin, topin ja bottomin
                if (tarkistaSeinat(zombiePos.X+15, zombiePos.Y+15, newPos.X+15, newPos.Y+15)) { }
                    else
                    {
                        //pelaaja mitat = testi as pelaaja;
                        zombie.move(newPos);
                    
                        Canvas.SetLeft(uiZombie, newPos.X);
                        Canvas.SetTop(uiZombie, newPos.Y);
                        //pitäs saada tämän korkeus jotenkin.
                    }
                 RotateTransform f = new RotateTransform(zombikulma / (Math.PI * 2) * 360 + 90);
                 uiZombie.RenderTransform = f;
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
            if (e.ChangedButton == MouseButton.Left)
            {
                kohdex = e.GetPosition(canvas1).X;
                kohdey = e.GetPosition(canvas1).Y;
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                Ammu(50, e.GetPosition(canvas1).X, e.GetPosition(canvas1).Y);

            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// zoomaa keskelle lomaketta. Käyttää sliderin asettamaa zoomiarvoa.
        /// </summary>
        private void zoomaa()
        {
          
           ScaleTransform scaletransform = new ScaleTransform();
           TranslateTransform translatetransform = new TranslateTransform();

            TransformGroup yhdiste = new TransformGroup();

            //scaletransform.CenterX = this.Width / 2;// (Canvas.GetLeft(this.pelaaja1)); // + pelaaja1.ActualWidth / 2);
            //scaletransform.CenterY = this.Height/2;//Canvas.GetTop(this.pelaaja1);// +pelaaja1.ActualHeight / 2;
            scaletransform.ScaleX = zoomi;
            scaletransform.ScaleY = zoomi;

            translatetransform.Y = scaletransform.CenterY - Canvas.GetTop((UIElement)this.pelaaja1) +this.Height / 2-pelaaja1.ActualHeight/2;
            double apu = scaletransform.CenterX - Canvas.GetLeft((UIElement)this.pelaaja1) + this.Width / 2-pelaaja1.ActualWidth/2;
            translatetransform.X = apu; // / zoomi);

           
            yhdiste.Children.Add(translatetransform);
            yhdiste.Children.Add(scaletransform);

            canvas1.RenderTransform = yhdiste;
          
        }



        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter)) {
                kaupunki= syötelokero.Text;
                syötelokero.Visibility = Visibility.Collapsed;
                Syöeselite.Visibility = Visibility.Collapsed;
                //Console.Beep();
                alusta(kaupunki);
            }
        }
        /// <summary>
        /// Korjataan ui elementtien sijaintia jos ikkunan koko muuttuu. Tällä hetkellä vain korkeussuunnassa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
          //  Canvas.SetTop(slider1, 0);
            //Canvas.SetTop(canvas1,Canvas.GetBottom(slider1));
            //canvas1.Width = this.Width;
            //canvas1.Height = this.Height-slider1.Height;

        }

        Ellipse CreateEllipse(double width, double height, double desiredCenterX, double desiredCenterY)
        {

            SolidColorBrush osumavari = new SolidColorBrush();
            osumavari.Color = Colors.Red;
            Ellipse ellipse = new Ellipse { Width = width, Height = height };
            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height / 2);
            ellipse.Fill = osumavari;
            ellipse.Margin = new Thickness(left, top, 0, 0);
            return ellipse;
        }

        private void Ammu(double force, double kohdex, double kohdey)
        {

            // mistä luoti lähtee liikkeelle
            double alkukorkeutta = Canvas.GetTop((UIElement)this.pelaaja1) + this.pelaaja1.ActualWidth/2;
            double alkuleveytta = Canvas.GetLeft((UIElement)this.pelaaja1) + this.pelaaja1.ActualHeight/2;
            double korkeutta = alkukorkeutta;
            double leveytta = alkuleveytta;
            
            // vielä liikkumassa
            bool moving = true;
            int movement=0;

            // luodin suuntakulma
            double ammuskulma = laskeKulma(kohdex - leveytta, kohdey - korkeutta);

            // luoti liikkuu pelaaja-oliona.. 

            pelaaja luoti = new pelaaja();

            soundsystem.PlaySound("fire1", 1.0f);
            // kun luoti vielä liikkuu
           while(moving)
            {
              movement ++;  // liikkuu forcen verran eteenpäin
               if (movement > force)
                   break;
                // paikka muuttuu näin
               leveytta = luoti.liikuta(leveytta, korkeutta, ammuskulma)[0];
               korkeutta = luoti.liikuta(leveytta, korkeutta, ammuskulma)[1];


               //System.Console.WriteLine("luoti x:" + leveytta + " luoti y:" + korkeutta);
                 // jos osuu seinään, niin piiretään kohdalle punainen pallo
               if (tarkistaSeinat(alkuleveytta, alkukorkeutta, leveytta, korkeutta) == true)
               {
                   Ellipse pallo = CreateEllipse(10, 10, leveytta, korkeutta);  
                   canvas1.Children.Add(pallo);

                   soundsystem.PlaySound("hit-glass", 1.0f);  //System.Console.WriteLine("luoti osui seinään");
                   break;
               }
               // piiretään luodin kulkulinja
               Line viiva = new Line();
               viiva.Visibility = Visibility.Visible;
               viiva.StrokeThickness = 1;
               viiva.X1 = alkuleveytta;
               viiva.Y1 = alkukorkeutta;
               viiva.X2 = leveytta;
               viiva.Y2 = korkeutta;
               viiva.Stroke = System.Windows.Media.Brushes.Black;
               canvas1.Children.Add(viiva);
               
            }
          

        }


    }
}
