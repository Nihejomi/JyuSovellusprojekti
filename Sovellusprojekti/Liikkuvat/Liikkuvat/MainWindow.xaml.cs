﻿using System;
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
namespace Liikkuvat
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public int k;
        public int l;
        public int kohdex;
        public int kohdey;
        public double pelaajakulma;
        
       ArrayList liikuta = new ArrayList();
        public MainWindow()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0,120);
 
            liikuta.Add(pelaaja1);
            InitializeComponent();

            dispatcherTimer.Start();
            
        }

        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           
            //Console.Beep();
            k = (int)Canvas.GetTop((UIElement)this.pelaaja1);
            l = (int)Canvas.GetLeft((UIElement)this.pelaaja1);
            int xsuunta= kohdex-l;
            int ysuunta= kohdey-k;
            label1.Content = xsuunta + ":" + ysuunta + "::" + pelaajakulma;
            pelaajakulma = laskeKulma(xsuunta, ysuunta);
           


            kaikkiliikkuu();
        }

        private double laskeKulma(int xsuunta, int ysuunta)
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

        private void kaikkiliikkuu()
        {
        int  korkeutta = (int)Canvas.GetTop((UIElement)this.pelaaja1);
          int  leveytta = (int)Canvas.GetLeft((UIElement)this.pelaaja1);
            if(tarkistaetaisyys(korkeutta, leveytta, kohdex,kohdey,pelaaja1.vektorinpituus)){
                //pelaaja1.pyorita(pelaajakulma);
                Canvas.SetLeft((UIElement)this.pelaaja1, this.pelaaja1.liikuta(l, k, pelaajakulma)[0]);
            

            Canvas.SetTop((UIElement)this.pelaaja1, this.pelaaja1.liikuta(l, k, pelaajakulma)[1]);  
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
        private bool tarkistaetaisyys(int korkeutta, int leveytta,int kohdex, int kohdey, int vektorinpituus)
        {

            if (((kohdex - leveytta) * (kohdex - leveytta) + (kohdey - korkeutta) * (kohdey - korkeutta)) > (vektorinpituus * vektorinpituus+2))
            {
                
                return true;
            }
           
            return false;
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
     
            kohdex = (int)e.GetPosition(canvas1).X;
            kohdey = (int)e.GetPosition(canvas1).Y;
        }
    }
}
