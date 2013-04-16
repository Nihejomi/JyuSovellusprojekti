/*  KMKK-sovellusprojekti: Avointa Dataa hyödyntävä Zombie-selviytymispeli 
 *  versio 0.01
 *  Hahmotelmaa server-tyyppisestä pelin (logiikan)pääluokasta. 
 *  
 *  Tein aluksi kaiken samaan tiedostoon, tästä voi sitten leikata ja jatkaa kutakin luokkaa omaan tiedostoonsa jne. -joel
 *  
 */


using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;



// alustava pääluokka(hahmoitelma) pelin (keskus)järjestelmälle
namespace Peli
{

    // koordinaattiprimitiivi 
    public class Vektori
    {
        public Double x;
        public Double y;

        public Vektori(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public double getX()
        {
            return this.x;
        }
        public double getY()
        {
            return this.y;
        }        
    }


    public class Rakennus
    {
        String nimi;
        //int tyyppi;
        ArrayList vektorit; // Vektorit tulevat niin että ensimmäinen ja vika on samat, piirto todennäköisesti järjestyksessä pitkin. -joel
        public Rakennus(String nimi, ArrayList vektorit)
        {
            this.nimi = nimi;
            this.vektorit = new ArrayList(vektorit);          
        }

        // tarkistaa esim onko vektori rakennuksen sisällä / tai menossa sisälle (?)
        public bool tarkistaPiste(Vektori vektori){
            return false;
        }

        public void TulostaKonsoliin()
        {
            System.Console.WriteLine("Rakennuksen nimi: " + this.nimi + " vektorit: ");

            for (int i = 0; i < this.vektorit.Count; i++)
            {
                Vektori luettuvektori = (Vektori)this.vektorit[i];
                System.Console.WriteLine(i + " X: " + luettuvektori.getX() + " Y:" + luettuvektori.getY());
            }
        }
        public Vektori annaVektori(int i)
        {
            Vektori luettuvektori = (Vektori)this.vektorit[i];
            return luettuvektori;
        }



    }

    public class Vesi
    {
        String nimi;
        //int tyyppi;
        ArrayList vektorit; // Vektorit tulevat niin että ensimmäinen ja vika on samat, piirto todennäköisesti järjestyksessä pitkin. -joel
        public Vesi(String nimi, ArrayList vektorit)
        {
            this.nimi = nimi;
            this.vektorit = new ArrayList(vektorit);
        }

        // tarkistaa esim onko vektori rakennuksen sisällä / tai menossa sisälle (?)
        public bool tarkistaPiste(Vektori vektori)
        {
            return false;
        }

        public void TulostaKonsoliin()
        {
            System.Console.WriteLine("Veden nimi: " + this.nimi + " vektorit: ");

            for (int i = 0; i < this.vektorit.Count; i++)
            {
                Vektori luettuvektori = (Vektori)this.vektorit[i];
                System.Console.WriteLine(i + " X: " + luettuvektori.getX() + " Y:" + luettuvektori.getY());
            }
        }

        public Vektori annaVektori(int i)
        {
            Vektori luettuvektori = (Vektori)this.vektorit[i];
            return luettuvektori;
        }


    }


    public class Tie{
       // String nimi;
       // ArrayList vektorit;
       // int tyyppi;
    }


    public class Pelaaja{
     //   Vektori paikka;
     //   Vektori liike;
        
     //   String nimi;
    //    int hp;
    }

    public class Zombie{
     //   Vektori paikka;
     //   Vektori liike;

     //   int tila; // esim näkeekö pelaajan vai ei

       // public void scan();
        //public void move();
        // public bool tarkistaPiste(Vektori vektori);
        
    }


    class Peli

    {
        // Tänne kutakin pelikenttää/aluetta vastaavat oliot ja datat

        // kuva ?
        // säätila
        // väkiluku
   
        ArrayList Rakennukset;
        ArrayList Vedet;

         //ArrayList Tiet;


        // koordinaattivektoreiden muuntelua näillä parametreillä -joel
        double scalex;
        double scaley;
        double offsetx;
        double offsety;

        // debuggausta
        public void listaaDataKonsoliin()
        {

            for (int i = 0; i < Rakennukset.Count; i++)
            {
                Rakennus luetturakennus = (Rakennus)Rakennukset[i];
                luetturakennus.TulostaKonsoliin();
            }
            for (int i = 0; i < Vedet.Count; i++)
            {
                Vesi luettuvesi = (Vesi)Vedet[i];
                luettuvesi.TulostaKonsoliin();
            }
        }


        // Valmis osa: Siirtää rakennusten datan pelisysteemille nopeasti saataville -joel
        public void LataaData(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy, XMLData luettudata)
        {


            // muuntaa xml-koordinaatiston sopivaan pelikoordinaatti muotoon
            offsetx = -minlon;
            offsety = -minlat;
            scalex = (1 / (maxlon-minlon)) * resox;
            scaley = (1 / (maxlat-minlat)) * resoy;


            System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle...: rakennuksia yhteensä " + luettudata.annaRakennusLkm() );
            for (int i = 0; i < luettudata.annaRakennusLkm(); i++)
            {
                XMLRakennus temprakennus = (luettudata.annaRakennus(i));
                ArrayList tempvektorit = new ArrayList();

                for (int k = 0; k < temprakennus.annaNoodiLkm(); k++)
                {
                    // etsitään rakennuksen noodirefensseille vastaavuudet noodikokoelmasta 
                    double haettava = temprakennus.annaNoodiId(k);
                    
                    bool find = false;
                    int l = 0;                  
                    while (!find)
                    {
                        XMLNoodi tempnoodi = luettudata.annaNoodi(l);
                        if (tempnoodi.getId() == haettava)
                        {
                            // tallennetaan rakennusten luokkaan tällaisella kaavalla, TODO: oikeanlainen muunnos jotta saadaan järkevät koordinaatit käyttöön
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) *this.scalex, (tempnoodi.getLat() + this.offsety) *this.scaley);
                            tempvektorit.Add(tempvektori);

                            find = true;
                        }
                        l++;
                        if (l >= luettudata.annaNoodiLkm())
                            break;
                    }          
                }
                if (tempvektorit.Count != 0)
                {
                    Rakennus uusirakennus = new Rakennus(temprakennus.annaNimi(), tempvektorit);
                    Rakennukset.Add(uusirakennus);
                }
            }

            System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle..: vesiä yhteensä " + luettudata.annaVesiLkm());
            for (int i = 0; i < luettudata.annaVesiLkm(); i++)
            {
                XMLVesi tempvesi = (luettudata.annaVesi(i));
                ArrayList tempvektorit = new ArrayList();                
                for (int k = 0; k < tempvesi.annaNoodiLkm(); k++)
                {
                    // etsitään rakennuksen noodirefensseille vastaavuudet noodikokoelmasta 
                    double haettava = tempvesi.annaNoodiId(k);

                    bool find = false;
                    int l = 0;

                    while (!find)
                    {
                        XMLNoodi tempnoodi = luettudata.annaNoodi(l);
                        if (tempnoodi.getId() == haettava)
                        {
                            // tallennetaan rakennusten luokkaan tällaisella kaavalla, TODO: oikeanlainen muunnos jotta saadaan järkevät koordinaatit käyttöön
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) * this.scalex, (tempnoodi.getLat() + this.offsety) * this.scaley);
                            tempvektorit.Add(tempvektori);

                            find = true;
                        }
                        l++;
                        if (l >= luettudata.annaNoodiLkm())
                            break;

                    }
                }
                if (tempvektorit.Count != 0)
                {
                    Vesi uusivesi = new Vesi("vesi", tempvektorit);
                    Vedet.Add(uusivesi);
                }
            }


           
        }


        public int annaRakennusLkm()
        {
            return this.Rakennukset.Count;
        }
        public int annaVesiLkm()
        {
            return this.Vedet.Count;
        }



        // palauttaa yhden rakennus-olion Peli:stä, indeksin täytyy olla pienempi kuin Peli.annaRakennusLkm()
        public Rakennus annaRakennus(int i)
        {
            Rakennus luetturakennus = (Rakennus)Rakennukset[i];
            return luetturakennus;
        }
        // palauttaa yhden vesi-olion Peli:stä, indeksin täytyy olla pienempi kuin Peli.annaVesiLkm()
        public Rakennus annaVesi(int i)
        {
            Rakennus luettuvesi = (Rakennus)Vedet[i];
            return luettuvesi;
        }

        // luo peliolion (kokoelman) kordinaattien perusteella netistä apin avulla
        public Peli(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy)
        {
            System.Console.WriteLine("ladataan xml openstreetmapin apista...");
            OpenStreetMapClient lataaja = new OpenStreetMapClient();
          lataaja.downloadOSMfile(minlat, minlon, maxlat, maxlon, "temp.osm");

               XMLData luettudata = XMLLukija.LueXML("temp.osm");
                
                Rakennukset = new ArrayList();
                Vedet = new ArrayList();

                LataaData(minlat, minlon, maxlat, maxlon, resox, resoy, luettudata);
                System.Console.WriteLine("Koko Data ladattu sisälle.");
                //System.Console.WriteLine("Enteriä ja listataan kaikkien rakennusten data ");
              // System.Console.ReadLine();
                // debuggausta
               // listaaDataKonsoliin();
                //System.Console.ReadLine();
              
        }
        // luo peliolion(kokoelman) jonkin osm-tiedoston perusteella, eikä netistä apista, min/max koordinaatit pitäisi kuitenkin tietää itse, jotta muuntuvat oikein.
        public Peli(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy, string filename)
        {
            
            XMLData luettudata = XMLLukija.LueXML(filename);

            Rakennukset = new ArrayList();
            Vedet = new ArrayList();

            LataaData(minlat, minlon, maxlat, maxlon, resox, resoy, luettudata);
           // System.Console.WriteLine("Data ladattu sisälle.");
            //System.Console.WriteLine("Enteriä ja listataan kaikkien rakennusten data ");
            //System.Console.ReadLine();
            // debuggausta
            //listaaDataKonsoliin();
           // System.Console.ReadLine();

        }


        // keskeisimmät pelilogiikan metodit, joita kutsutaan jollain timerillä luupissa, nämä voisi myös kirjoittaa tähän tiedostoon, vai (?) -joel
        public void update() { }
        public void scan() { }
        public void move() { }


        // testiohjelma, ilman viimeistä paramaetriä (tiedostoa), latautuu koko jyväskylän alue (14 megaa), jonka koordinaatit tulevat oikein talteen.
        
        static void Main(string[] args)
        {
                    // esimerkiksi jkl:n: kokoinen alue, resoluutiolle 1000x1000
           Peli testipeli = new Peli(62.19, 25.648, 62.268, 25.852, 1000, 1000, "jkl.osm");

        }  

    }

}
