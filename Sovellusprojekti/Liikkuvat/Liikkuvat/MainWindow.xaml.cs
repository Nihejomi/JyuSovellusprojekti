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
using System.Timers;

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
        public bool paused;
        public bool pelaajakuoli = false;
        public bool Inputmenu;
        public bool pausepohjassa;
        public bool ohjepaalla;
        //kaupungin luomiseen käytetty reso
        public int kaupunginResoX;
        public int kaupunginResoY;
        public Key eteennappi = Key.W;
        public Key taaksenappi = Key.S;
        public Key oikeallenappi = Key.D;
        public Key vasemmallenappi = Key.A;
        public Key latausnappi = Key.R;
        bool eteen = false;
        bool taakse = false;
        bool zoomataan = false;
        private double zoomi = 1;
        int kaantyminen=5;
        
        int lipas = 8;
        bool justshot = false;
        RotateTransform r = new RotateTransform();
       
        public double pelaajakulma;
        public double zombikulma;
        Peli.Peli testi;
        ArrayList rakennukset = new ArrayList();
        ArrayList rakennuksetVasenYla = new ArrayList();
        ArrayList rakennuksetVasenAla = new ArrayList();
        ArrayList rakennuksetOikeaYla = new ArrayList();
        ArrayList rakennuksetOikeaAla = new ArrayList();
        ArrayList liikuta = new ArrayList();
        ArrayList Viivat = new ArrayList();
        ArrayList seinat = new ArrayList();
        ArrayList tiet = new ArrayList();// Tähän tiet jos tehdään liikkumis nopeutta käsittelevä metodi joskus
        ArrayList Muut = new ArrayList();// tänne kaikki grafiikka jolla ei mitään muuta käyttöä
        ArrayList tulivanat = new ArrayList();
        ArrayList osumat = new ArrayList();

        ArrayList luotiviivat = new ArrayList();

        Polygon pohja = new Polygon();
        

           Soundsystem soundsystem;
           System.Timers.Timer soundcheck;
           System.Timers.Timer walksoundtimer;
           System.Timers.Timer zombiesoundtimer;



           int hitpoints;

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
            Ohje.Visibility = Visibility.Collapsed;
            pelaaja1.Visibility = Visibility.Visible;
            pelaajakuoli = false;
            // resot jotka voidaan myöhemmin sitoa johonkin muuttujaan.
            int resox = 4000;
            int resoy = 4000;
            kaupunginResoX = resox;
            kaupunginResoY = resoy;
            int zombeja;
            HTMLParser.Parsinta tietoja = new HTMLParser.Parsinta();
            tietoja.Alusta(p);
            zombeja =  tietoja.zombieMaara / 100;

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
           //label2.Content = zombeja;

            InitializeComponent();

            Canvas.SetTop(pelaaja1, this.Height  / 2 + pelaaja1.ActualHeight / 2);
            Canvas.SetLeft(pelaaja1, this.Left / 2 + pelaaja1.ActualWidth / 2);
                                                                                 // lisäsin viimeisen parametrin teiden poistoon latauksen nopeuttamiseksi (true = ei teitä, false = tiet mukaan)
            //testi = new Peli.Peli(62.23407, 25.73577, 62.24372, 25.76086, 2000, 2000, false); // jkl

           //testi = new Peli.Peli(61.48035, 23.77334, 61.48891, 23.7254, 2000, 2000, false); // tre

      //testi = new Peli.Peli(62.22418, 25.76424, 62.22893, 25.77725, resox, resoy, false);  // jkl:n kuokkala (nopeasti latautuva pieni testialue)
      testi = new Peli.Peli(kaupunki, resox, resoy, 1, false);

    //   testi = new Peli.Peli(62.24605, 25.69715, 62.25452, 25.71957, resox, resoy, false); // kortepohja

          //testi = new Peli.Peli(62.2243, 25.6666, 62.2623, 25.7708, resox, resoy, false); // koko jyväskylä (älä edes yritä)
          
            //rakennukset = testi.annaAlueRakennukset(62.2330, 25.733, 62.2335, 25.7335);


            soundsystem = new Soundsystem();
            
            // tarkistaa 5 sekunnin välein jos ambienssiääni/musiikki on loppunut, voisi tehdä niin että esim ambienssi vaihtuu alueittan
            soundcheck = new System.Timers.Timer(5000);
            soundcheck.Elapsed += new ElapsedEventHandler(soundcheckEvent);
            soundcheck.Enabled = true;
            soundcheck.Start();

            // kävelysoundi 500ms, voisi muuttua juostessa nopeammaksi yms
            walksoundtimer = new System.Timers.Timer(500);
            walksoundtimer.Elapsed += new ElapsedEventHandler(walksoundEvent);
            walksoundtimer.Enabled = true;
            walksoundtimer.Start();

            // zombien ölinöitä enintään kerran 5. sekunnissa
            zombiesoundtimer = new System.Timers.Timer(5000);
            zombiesoundtimer.Elapsed += new ElapsedEventHandler(zombiesoundEvent);
            zombiesoundtimer.Enabled = true;
            zombiesoundtimer.Start();
            
            PiirraPohja();
            piirraRuohot();
            piirraTiet();
            piirraRakennukset();
            piirraVesistot();

            //klipataan alue


   
            

            // Lisataan zombit
            Zombit lauma = new Zombit();
            int zombejasisalla = 0;
            int zombinvuoro = 0;
            for (int i = 0; i < zombeja; i++)
            {

                int[] ominaisuudet = lauma.arvoZombi(resox, resoy);
                if (!(OnkoRakennuksessa(ominaisuudet[0], ominaisuudet[1])))
                {
                    if (zombinvuoro == 5)
                    { zombinvuoro = 0; }
                    else
                    {
                    zombinvuoro++;
                    }
                    Zombi z = new Zombi(new Vector(ominaisuudet[0], ominaisuudet[1]));

                    Canvas.SetZIndex(z, 9000 + i);
                    //z.Name = "testiZombi";
                    Canvas.SetTop(z, ominaisuudet[1]);
                    Canvas.SetLeft(z, ominaisuudet[0]);
                    z.Opacity = 1;
                    z.kaannosvuoro = zombinvuoro;
                    
                    z.ghost();
                    //z.die();
                    //z.nope();
                    
                    ///zombivuoro=0;
                    //}
                    (z as UIElement).RenderTransformOrigin = new Point(0.5, 0.5);
                    canvas1.Children.Add(z);
                    liikuta.Add(z);
                }
                else
                {
                    zombejasisalla++;


                }
            }

            hitpoints = 100;
            hitBar.Maximum = hitpoints;
            hitBar.Value = hitpoints;

            ammoBar.Maximum = lipas;
            ammoBar.Value = lipas;

            //label2.Content = zombejasisalla;
            dispatcherTimer.Start();
           
        }

        private void soundcheckEvent(object source, ElapsedEventArgs e)
        {            
            soundsystem.check_ambience();
            soundsystem.check_drone();
        }
        private void walksoundEvent(object source, ElapsedEventArgs e)
        {            
            if(eteen || taakse)
            soundsystem.PlaySound("walk", 1.0f);
            if (justshot)
                justshot = false;
        }

        private void zombiesoundEvent(object source, ElapsedEventArgs e)
        {
            int aanietaisyys= 400;
            Random rand = new Random();
            int arpa = rand.Next(0, 10);
           
            double pelaajax=0;
            double pelaajay=0;
           

            Dispatcher.Invoke((Action)(() =>
        {

            pelaajax = Canvas.GetTop((UIElement)pelaaja1); 
            pelaajay = Canvas.GetLeft((UIElement)pelaaja1); 
        }));

            foreach (liikkuva zombie in liikuta)
            {
                
                UIElement uiZombie = zombie as UIElement;
                
                Vector zombipos = zombie.getPosition();

                double distancex = Math.Abs(zombipos.X - pelaajax);
                double distancey = Math.Abs(zombipos.Y - pelaajay);                               
                if((zombie.isAlive() ) && (distancex < aanietaisyys) && (distancey <aanietaisyys)) 
                soundsystem.PlaySound("zombi", 0.5f);
                
            }
                           
        }

        private void PiirraPohja()
        {
            SolidColorBrush taustavari = new SolidColorBrush();
            taustavari.Color = Colors.DarkSlateGray;
            PointCollection pisteet = new PointCollection();
            pisteet.Add(new Point(0, 0));
            pisteet.Add(new Point(0, kaupunginResoX));
            pisteet.Add(new Point(kaupunginResoY, kaupunginResoX));
            pisteet.Add(new Point(kaupunginResoY, 0));
            pisteet.Add(new Point(0, 0));       
            pohja.Points = pisteet;
            pohja.Fill = taustavari;
            canvas1.Children.Add(pohja);
            Muut.Add(pohja);
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

            SolidColorBrush sairaala = new SolidColorBrush();
            sairaala.Color = Colors.Red;

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
    
   }
               kohde.maxx = maxx;
               kohde.minx = minx;
               kohde.maxy = maxy;
               kohde.miny = miny;

                  Polygon talo = new Polygon();
                  talo.Points = pisteet;
                
                    if(kohde.annaTyyppi() == 0)
                  talo.Fill = harmaa;
                    if (kohde.annaTyyppi() == 1) // sairaala
                        talo.Fill = sairaala;



                  rakennukset.Add(kohde);
                  if ((kohde.maxx <= kaupunginResoX / 2) && (kohde.maxy <= kaupunginResoY / 2))
                  {
                      rakennuksetVasenYla.Add(kohde);
                  }
                  else
                  {
                      if ((kohde.minx <= kaupunginResoX / 2) && (kohde.miny <= kaupunginResoY / 2))
                      {
                          rakennuksetVasenYla.Add(kohde);
                      }
                      else 
                      {
                          if ((kohde.minx <= kaupunginResoX / 2) && (kohde.maxy <= kaupunginResoY / 2))
                          {
                              rakennuksetVasenYla.Add(kohde);
                          }
                          else 
                          {
                              if ((kohde.maxx <= kaupunginResoX / 2) && (kohde.miny <= kaupunginResoY / 2))
                              {
                                  rakennuksetVasenYla.Add(kohde);
                              }
                          }
                      }
                  }


                  if ((kohde.maxx <= kaupunginResoX / 2) && (kohde.maxy > kaupunginResoY / 2))
                  {
                      rakennuksetVasenAla.Add(kohde);
                  }
                  else 
                  {
                      if ((kohde.minx <= kaupunginResoX / 2) && (kohde.miny > kaupunginResoY / 2))
                      {
                          rakennuksetVasenAla.Add(kohde);
                      }
                      else 
                      {
                          if ((kohde.minx <= kaupunginResoX / 2) && (kohde.maxy > kaupunginResoY / 2))
                          {
                              rakennuksetVasenAla.Add(kohde);
                          }
                          else 
                          {
                              if ((kohde.maxx <= kaupunginResoX / 2) && (kohde.miny > kaupunginResoY / 2))
                              {
                                  rakennuksetVasenAla.Add(kohde);
                              }
                          }
                      }
                  }



                  if ((kohde.maxx > kaupunginResoX / 2) && (kohde.maxy <= kaupunginResoY / 2))
                  {
                      rakennuksetOikeaYla.Add(kohde);
                  }
                  else 
                  {
                      if ((kohde.minx > kaupunginResoX / 2) && (kohde.miny <= kaupunginResoY / 2))
                      {
                          rakennuksetOikeaYla.Add(kohde);
                      }
                      else 
                      {
                          if ((kohde.maxx > kaupunginResoX / 2) && (kohde.miny <= kaupunginResoY / 2))
                          {
                              rakennuksetOikeaYla.Add(kohde);
                          }
                          else 
                          {
                              if ((kohde.minx > kaupunginResoX / 2) && (kohde.maxy <= kaupunginResoY / 2))
                              {
                                  rakennuksetOikeaYla.Add(kohde);
                              }
                          }
                      }
                  }



                  if ((kohde.maxx > kaupunginResoX / 2) && (kohde.maxy > kaupunginResoY / 2))
                  {
                      rakennuksetOikeaAla.Add(kohde);
                  }
                  else 
                  {
                      if ((kohde.minx > kaupunginResoX / 2) && (kohde.miny > kaupunginResoY / 2))
                      {
                          rakennuksetOikeaAla.Add(kohde);
                      }
                      else 
                      {
                          if ((kohde.maxx > kaupunginResoX / 2) && (kohde.miny > kaupunginResoY / 2))
                          {
                              rakennuksetOikeaAla.Add(kohde);
                          }
                          else 
                          {
                              if ((kohde.minx > kaupunginResoX / 2) && (kohde.maxy > kaupunginResoY / 2))
                              {
                                  rakennuksetOikeaAla.Add(kohde);
                              }
                          }
                      }
                  }

                    
                  Point nimikohta = pisteet[0];
               
                  Label talonnimi = new Label();
                  talonnimi.Content = kohde.annaNimi();
                  Muut.Add(talonnimi);
                  Canvas.SetLeft(talonnimi, nimikohta.X);
                  Canvas.SetTop(talonnimi, nimikohta.Y);
                  
                  
                   

                  canvas1.Children.Add(talo);
                  Muut.Add(talo);
                  if (kohde.annaNimi().Length > 2)
                  canvas1.Children.Add(talonnimi);
                  Muut.Add(talonnimi);
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
            SolidColorBrush pihavari = new SolidColorBrush();
            pihavari.Color = Colors.PaleGreen;
            SolidColorBrush parkkivari = new SolidColorBrush();
            parkkivari.Color = Colors.DarkGray;


            for (int f = 0; f < testi.annaRuohoLkm() - 1; f++)
            {
                Peli.Ruoho kohde = testi.annaRuoho(f);

                PointCollection pisteet = new PointCollection();

                for (int i = 0; i < kohde.annaVektoriLkm() - 1; i++)
                {
                    pisteet.Add(new Point(kohde.annaVektori(i).x, kohde.annaVektori(i).y));                 
                }

                Polygon ruoho = new Polygon();
                ruoho.Points = pisteet;
                if (kohde.annaTyyppi() == 0)
                    ruoho.Fill = vihrea;
                if (kohde.annaTyyppi() == 1)
                    ruoho.Fill = tummavihrea;
                if (kohde.annaTyyppi() == 2)
                    ruoho.Fill = suovihrea;
                if (kohde.annaTyyppi() == 3)
                    ruoho.Fill = pihavari;
                if (kohde.annaTyyppi() == 4)
                    ruoho.Fill = parkkivari;

                canvas1.Children.Add(ruoho);
                Muut.Add(ruoho);


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
                
                        pisteet.Add(new Point(kohde.annaVektori(i).x, kohde.annaVektori(i).y));
                }
                

                    Polyline tie = new Polyline();
                    tie.Points = pisteet;
                    tie.StrokeThickness = 10.0;
                    tie.Visibility = System.Windows.Visibility.Visible;
                    if(kohde.annaTyyppi() == 0)
                    tie.Stroke = harmaa;
                    if(kohde.annaTyyppi() == 1 )
                    tie.Fill = harmaa;
                    canvas1.Children.Add(tie);
                    tiet.Add(tie);

                    Point nimikohta = pisteet[0];

                    Label tiennimi = new Label();
                    tiennimi.Content = kohde.annaNimi();

                    Canvas.SetLeft(tiennimi, nimikohta.X);
                    Canvas.SetTop(tiennimi, nimikohta.Y);
                    if (kohde.annaNimi().Length > 2)
                    canvas1.Children.Add(tiennimi);
                    Muut.Add(tiennimi);
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
               if (zoomi > -3)
               {
                   

                       zoomi = zoomi - 0.05;
                       
         
               }
           }

           if (Keyboard.IsKeyDown(Key.Escape))
           {
               if (pausepohjassa) { }
               else {
                   pausepohjassa = true;
                   if (paused)
                   {
                     
                       paused = false;
                       Ohje.Visibility = Visibility.Collapsed;
                       menunappi.Visibility = Visibility.Collapsed;
                       LiikkumisAsetukset.Visibility = Visibility.Collapsed;
                       ohjenappi.Visibility = Visibility.Collapsed;
                       lopetusnappi.Visibility = Visibility.Collapsed;
                       Inputmenu = false;
                   }
                   else
                   {
              
                       paused = true;
                       ohjenappi.Visibility = Visibility.Visible;
                       lopetusnappi.Visibility = Visibility.Visible;   
                       menunappi.Visibility = Visibility.Visible;
                       ohjenappi.Visibility = Visibility.Visible;
                   }
               }
           }
           else 
           {
               pausepohjassa = false;
           }

           if (Keyboard.IsKeyDown(latausnappi))
           {
               if (ammoBar.Value < lipas)
               {
                   ammoBar.Value = lipas;
                   soundsystem.PlaySound("load", 1.0f);
               }
           }
           if (Keyboard.IsKeyDown(vasemmallenappi))
           {
               pelaajakulma = pelaajakulma - 0.05;
           }
           if (Keyboard.IsKeyDown(oikeallenappi))
           {
               pelaajakulma = pelaajakulma + 0.05;
           }
           if (Keyboard.IsKeyDown(eteennappi))
           {
           
               eteen = true;
           }
           else { eteen = false;  }
           if (Keyboard.IsKeyDown(taaksenappi))
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
            if (hitBar.Value > 0)
            {
                kaikkiliikkuu();
            }
            else {
          
                    peliohi();
                    pelaajakuoli = true;
                    dispatcherTimer.Stop();
            
                //dispatcherTimer.Stop();
            }
        }
        /// <summary>
        /// Tässä olis tarkoitus poistella viime pelin oliota kuten zombeja ja grafiikkaa.
        /// Tämän jälkeen roskien keruu toivottavasti hävittää nämä oliot kun niihin ei enää ole viitteitä
        /// </summary>
        private void peliohi()
        {
            soundcheck.Stop();
            walksoundtimer.Stop();
            zombiesoundtimer.Stop();
           foreach (object z in liikuta){
              canvas1.Children.Remove((UIElement)z);
               
           
           }
           //Huom! rakennusten mukaan piirretyt polygonit poistettaan tässä loopissa;   
           foreach (object z in Muut)
           {
               canvas1.Children.Remove((UIElement)z);


           }
           foreach (object z in tiet)
           {
               canvas1.Children.Remove((UIElement)z);


           }

           liikuta = new ArrayList();
           rakennukset = new ArrayList();
           Muut = new ArrayList();
           tiet = new ArrayList();
           rakennuksetVasenYla = new ArrayList();
           rakennuksetVasenAla = new ArrayList();
           rakennuksetOikeaYla = new ArrayList();
           rakennuksetOikeaAla = new ArrayList();
            syötelokero.Visibility = Visibility.Visible;
            menunappi.Visibility = Visibility.Visible;
            ohjenappi.Visibility = Visibility.Visible;
            lopetusnappi.Visibility = Visibility.Visible;
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
            bool palautus=false;
            if((x2<= kaupunginResoX/2) &&  (y2 <= kaupunginResoY/2)){
                palautus=tarkistaSeinat(x1, y1, x2, y2, rakennuksetVasenYla);
            }
            if ((x2 <= kaupunginResoX / 2) && (y2 > kaupunginResoY / 2) && (palautus== false))
            {
                palautus=tarkistaSeinat(x1, y1, x2, y2, rakennuksetVasenAla);
            }
            if ((x2 > kaupunginResoX / 2) && (y2 <= kaupunginResoY / 2) && (palautus == false))
            {
                palautus=tarkistaSeinat(x1, y1, x2, y2, rakennuksetOikeaYla);
            }
            if ((x2 > kaupunginResoX / 2) && (y2 > kaupunginResoY / 2) && (palautus == false))
            {
                palautus=tarkistaSeinat(x1, y1, x2, y2, rakennuksetOikeaAla);
            }
            
       
            return palautus;
        }
        /// <summary>
        /// Arraylista kohtainen TarkistaSeinät metodi.
        /// </summary>
        /// <param name="x1">vanhax</param>
        /// <param name="y1">vanhay</param>
        /// <param name="x2">uusix</param>
        /// <param name="y2">uusiy</param>
        /// <param name="rakennukset"></param>
        /// <returns></returns>
        private bool tarkistaSeinat(double x1, double y1, double x2, double y2, ArrayList rakennukset)
        {



            foreach (Rakennus R in rakennukset)
            {
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
                    v.X1 = R.annaVektori(R.annaVektoriLkm() - 1).x;
                    v.Y1 = R.annaVektori(R.annaVektoriLkm() - 1).y;
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
        ///Tarkistaa osuuko piirretty viiva taloon. Ei huomio hitboxeja joten soveltuu paremmin pitkien viivojen kuten lineofsightin tai ammusten törmäystarkistusta varten.
        ///Arrayliskohtainen.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="rakennukset"></param>
        /// <returns></returns>
        private bool tarkistaSeinatIlmanhiboxia(double x1, double y1, double x2, double y2, ArrayList rakennukset)
        {



            foreach (Rakennus R in rakennukset)
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
                    v.X1 = R.annaVektori(R.annaVektoriLkm() - 1).x;
                    v.Y1 = R.annaVektori(R.annaVektoriLkm() - 1).y;
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
            if (paused) { }
            else
            {

                zoomaa();
                // skrollaa();

                double korkeutta = Canvas.GetTop((UIElement)this.pelaaja1);
                double leveytta = Canvas.GetLeft((UIElement)this.pelaaja1);
                Vector playerPos = new Vector(leveytta, korkeutta);
                foreach (Zombi l in liikuta)
                {
                    if (l.getKaantovuoro() == kaantyminen)
                    {
                        if (l.getDistance(playerPos) < 500)
                            if (tarkistaSeinatIlmanhiboxia(leveytta + 10, korkeutta + 10, l.getPosition().X + 15, l.getPosition().Y + 15, rakennukset))
                            {
                                l.Opacity = 0; // l.Visibility= Visibility.Hidden;
                            }
                            else
                            {
                                l.Opacity = 1;
                                // l.Visibility = Visibility.Visible;
                            }
                        else
                        {
                            l.Opacity = 1;
                            //l.Visibility = Visibility.Hidden;
                        }
                    }
                }
                if (luotiviivat.Count != 0)
                {
                    System.Console.WriteLine("luotiviivoja: " + luotiviivat.Count);
                    for (int i = 0; i < luotiviivat.Count - 1; i++)
                    {
                        // DependencyObject parent = VisualTreeHelper.GetParent(control);
                        canvas1.Children.Remove((UIElement)luotiviivat[i]);
                    }
                    luotiviivat.Clear();
                    // System.Console.WriteLine("luotiviivoja: " + luotiviivat.Capacity);
                }

                RotateTransform r = new RotateTransform(pelaajakulma / (Math.PI * 2) * 360 + 90, pelaaja1.ActualHeight / 2, pelaaja1.ActualWidth / 2);
                pelaaja1.RenderTransform = r;
                if (eteen)
                {
                    if (tarkistaSeinat(Canvas.GetLeft(this.pelaaja1) + pelaaja1.ActualWidth / 2, Canvas.GetTop(this.pelaaja1) + pelaaja1.ActualHeight / 2, pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[0] + pelaaja1.ActualWidth / 2, this.pelaaja1.liikuta(leveytta, korkeutta, pelaajakulma)[1] + pelaaja1.ActualHeight / 2))
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

                kaantyminen--;

                foreach (liikkuva zombie in liikuta)
                {

                    if (zombie.isDead()) continue;


                    if (zombie.getDistance(playerPos) > 300) continue;

                    UIElement uiZombie = zombie as UIElement;
                    Vector zombiePos = zombie.getPosition();


                    Vector newPos = zombie.possibleMove(playerPos);
                    zombikulma = laskeKulma(leveytta - newPos.X, korkeutta - newPos.Y);

                    if (zombie.isNope()) continue;
                    if (kaantyminen == zombie.getKaantovuoro())
                    {

                        RotateTransform f = new RotateTransform(zombikulma / (Math.PI * 2) * 360 + 90);
                        uiZombie.RenderTransform = f;

                    }

                    //pitäs saada dynäämisesti tuo zombin leveys otettua, vois laskea kun tietää canvas elementin koon ja leftin ja rightin, topin ja bottomin
                    if (!zombie.isGhost() | tarkistaSeinat(zombiePos.X + 15, zombiePos.Y + 15, newPos.X + 15, newPos.Y + 15)) { }
                    else
                    {
                        //pelaaja mitat = testi as pelaaja;
                        zombie.move(newPos);

                        Canvas.SetLeft(uiZombie, newPos.X);
                        Canvas.SetTop(uiZombie, newPos.Y);
                        //pitäs saada tämän korkeus jotenkin.
                    }

                    double pelaajax = Canvas.GetTop((UIElement)this.pelaaja1) + this.pelaaja1.ActualWidth / 2;
                    double pelaajay = Canvas.GetLeft((UIElement)this.pelaaja1) + this.pelaaja1.ActualHeight / 2;
                    if (zombie.getDistance(new Vector(leveytta, korkeutta)) < 10)
                    {
                        hitBar.Value--;
                    }
                }
                if (kaantyminen == 0) { kaantyminen = 6; }

            }
        }
/*
        private bool tarkistaSeinatIlmanhiboxia(double x1, double y1, double x2, double y2)
        {
            bool palautus = false;
            if ((x2 <= kaupunginResoX / 2) && (y2 <= kaupunginResoY / 2))
            {
                palautus = tarkistaSeinatIlmanhiboxia(x1, y1, x2, y2, rakennuksetVasenYla);
            }
            if ((x2 <= kaupunginResoX / 2) && (y2 > kaupunginResoY / 2) && (palautus == false))
            {
                palautus = tarkistaSeinatIlmanhiboxia(x1, y1, x2, y2, rakennuksetVasenAla);
            }
            if ((x2 > kaupunginResoX / 2) && (y2 <= kaupunginResoY / 2) && (palautus == false))
            {
                palautus = tarkistaSeinatIlmanhiboxia(x1, y1, x2, y2, rakennuksetOikeaYla);
            }
            if ((x2 > kaupunginResoX / 2) && (y2 > kaupunginResoY / 2) && (palautus == false))
            {
                palautus = tarkistaSeinatIlmanhiboxia(x1, y1, x2, y2, rakennuksetOikeaAla);
            }


            return palautus;
        }*/
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
            if (paused)
            {}
            else{
                if (e.ChangedButton == MouseButton.Left)
                {
                    kohdex = e.GetPosition(canvas1).X;
                    kohdey = e.GetPosition(canvas1).Y;
                }

                if (e.ChangedButton == MouseButton.Right)
                {

                    if (ammoBar.Value == 0)
                    {
                        soundsystem.PlaySound("empty", 1.0f);
                    }
                    if (ammoBar.Value > 0 && !justshot)
                    {
                        Ammu(50, e.GetPosition(canvas1).X, e.GetPosition(canvas1).Y);
                        ammoBar.Value--;
                        justshot = true;
                    }


                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pelaaja1.Visibility = Visibility.Collapsed;
            Ohje.Visibility = Visibility.Collapsed;
            LiikkumisAsetukset.Visibility = Visibility.Collapsed;
            
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
            //if (zoomataan)
           // {
                
                yhdiste.Children.Add(scaletransform);
                
           // }

            canvas1.RenderTransform = yhdiste;
            
          
        }



   


        /// <summary>
        /// Tämä tehdääm jos joku painaa entteria kun syöttölokerolla on focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter)) {
                kaupunki= syötelokero.Text;
                syötelokero.Visibility = Visibility.Collapsed;
                Syöeselite.Visibility = Visibility.Collapsed;
                ohjenappi.Visibility = Visibility.Collapsed;
                LiikkumisAsetukset.Visibility = Visibility.Collapsed;
                lopetusnappi.Visibility = Visibility.Collapsed;
                //Console.Beep();
                Inputmenu = false;
                menunappi.Visibility = Visibility.Collapsed;
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

               foreach (liikkuva zombie in liikuta)
               {
                   if (zombie.isDead()) continue;
                   UIElement uiZombie = zombie as UIElement;
                   Vector zombiePos = zombie.getPosition();

                   if (leveytta > zombiePos.X && 
                       leveytta < zombiePos.X + uiZombie.RenderSize.Width && 
                       korkeutta > zombiePos.Y && 
                       korkeutta < zombiePos.Y + uiZombie.RenderSize.Height && moving)
                   {
                       System.Console.WriteLine("osui zombiin");
                       soundsystem.PlaySound("hit-flesh", 1.0f);
                       zombie.die();
                       moving = false;
                       
                   }


               }

               if (tarkistaSeinat(alkuleveytta, alkukorkeutta, leveytta, korkeutta) == true)
               {
                   Ellipse pallo = CreateEllipse(10, 10, leveytta, korkeutta);  
                   canvas1.Children.Add(pallo);


                   Random rand = new Random();

                   int arpa = rand.Next(0, 3);
                   switch (arpa)
                   {
                       case 0:
                           soundsystem.PlaySound("hit-glass", 1.0f);
                           break;
                       case 1:
                           soundsystem.PlaySound("hit-metal", 1.0f);
                           break;
                       case 2:
                           soundsystem.PlaySound("hit-rock", 1.0f);
                           break;

                   }
                   
                   break;
               }

               // piiretään luodin kulkulinja
               
            }/*
           Polyline luotiviiva;
           PointCollection pisteet = new PointCollection();
            pisteet.Add(new Point(alkuleveytta, alkukorkeutta));
            pisteet.Add(new Point(leveytta, korkeutta));
                
            luotiviiva = new Polyline();
            luotiviiva.Visibility = Visibility.Visible;
            luotiviiva.StrokeThickness = 1;
            luotiviiva.Points = pisteet;            
            luotiviiva.Stroke = System.Windows.Media.Brushes.Black;
            
            luotiviivat.Add(luotiviiva);

            canvas1.Children.Add(luotiviiva);
              */
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ylösboksi.Text = e.Key.ToString();
            eteennappi = e.Key;
        }

        private void TextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            alasboksi.Text = e.Key.ToString();
            taaksenappi = e.Key;
        }

        private void vasemmalleboksi_KeyUp(object sender, KeyEventArgs e)
        {
            vasemmalleboksi.Text = e.Key.ToString();
            vasemmallenappi = e.Key;
        }

        private void oikealleboksi_KeyUp(object sender, KeyEventArgs e)
        {
            oikealleboksi.Text = e.Key.ToString();
            oikeallenappi = e.Key;
        }

        private void TextBox_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void menunappi_Click(object sender, RoutedEventArgs e)
        {
            
            if (Inputmenu)
            {
                Inputmenu = false;
                LiikkumisAsetukset.Visibility = Visibility.Collapsed;
                Ohje.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (ohjepaalla)
                {
                    Ohje.Visibility = Visibility.Collapsed;
                }
                Inputmenu = true;
                LiikkumisAsetukset.Visibility = Visibility.Visible;
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ohjenappi_Click(object sender, RoutedEventArgs e)
        {
            Ohje.Visibility = Visibility.Visible;
            LiikkumisAsetukset.Visibility = Visibility.Collapsed;
            Inputmenu = false;
            ohjenappi.Visibility = Visibility.Visible;
        }

        private void lataaboksi_KeyUp(object sender, KeyEventArgs e)
        {

            lataaboksi.Text = e.Key.ToString();
            latausnappi = e.Key;

        }

       




    }
}
