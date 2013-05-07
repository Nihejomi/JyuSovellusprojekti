/*  KMKK-sovellusprojekti: Avointa Dataa hyödyntävä Zombie-selviytymispeli 
<<<<<<< HEAD
 *  versio 0.03
 *  Luokat xml-datasta luetuille objekteilla pelimaailmaan
=======
 *  versio 0.01
 *  Hahmotelmaa server-tyyppisestä pelin (logiikan)pääluokasta. 
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
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
<<<<<<< HEAD
        public Vektori annaVektori(int i)
        {
            Vektori luettuvektori = (Vektori)this.vektorit[i];
            return luettuvektori;
        }
        public int annaVektoriLkm(){
               return this.vektorit.Count;
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
         public int annaVektoriLkm(){
               return this.vektorit.Count;
        }

    }


=======



    }
   
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
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
<<<<<<< HEAD
        ArrayList Vedet;
=======
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769

         //ArrayList Tiet;


        // koordinaattivektoreiden muuntelua näillä parametreillä -joel
<<<<<<< HEAD
        double scalex;
        double scaley;
        double offsetx;
        double offsety;

        // debuggausta
        public void listaaDataKonsoliin()
=======
        double scale = 100000;
        double offset = 0;


        // debuggausta
        public void listaaRakennuksetKonsoliin()
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
        {

            for (int i = 0; i < Rakennukset.Count; i++)
            {
                Rakennus luetturakennus = (Rakennus)Rakennukset[i];
                luetturakennus.TulostaKonsoliin();
            }
<<<<<<< HEAD
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
=======
        }

        // Valmis osa: Siirtää rakennusten datan pelisysteemille nopeasti saataville -joel
        public void LataaRakennukset(XMLData luettudata)
        {
            System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle: rakennuksia yhteensä " + luettudata.annaRakennusLkm() );
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
            for (int i = 0; i < luettudata.annaRakennusLkm(); i++)
            {
                XMLRakennus temprakennus = (luettudata.annaRakennus(i));
                ArrayList tempvektorit = new ArrayList();

                for (int k = 0; k < temprakennus.annaNoodiLkm(); k++)
                {
                    // etsitään rakennuksen noodirefensseille vastaavuudet noodikokoelmasta 
                    double haettava = temprakennus.annaNoodiId(k);
                    
                    bool find = false;
<<<<<<< HEAD
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
=======
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
                    int l = 0;

                    while (!find)
                    {
                        XMLNoodi tempnoodi = luettudata.annaNoodi(l);
                        if (tempnoodi.getId() == haettava)
                        {
                            // tallennetaan rakennusten luokkaan tällaisella kaavalla, TODO: oikeanlainen muunnos jotta saadaan järkevät koordinaatit käyttöön
<<<<<<< HEAD
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) * this.scalex, (tempnoodi.getLat() + this.offsety) * this.scaley);
=======
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() * this.scale)+this.offset, (tempnoodi.getLat() * this.scale)+this.offset);
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
                            tempvektorit.Add(tempvektori);

                            find = true;
                        }
                        l++;
<<<<<<< HEAD
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
        public Vesi annaVesi(int i)
        {
           Vesi luettuvesi = (Vesi)Vedet[i];
            return luettuvesi;
        }

        /// <summary>
        /// Palauttaa listan parametrien rajaaman alueen rakennus-olioista
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public ArrayList annaAlueRakennukset(double minX, double minY, double maxX, double maxY)
        {
        
            ArrayList palautus = new ArrayList();
            System.Console.WriteLine("ladataan rakennukseta alueelta " + minX + "," + minY + " - " + maxX + "," + maxY + "..");
            for (int i = 0; i < Rakennukset.Count; i++)
            {
                Rakennus tsekattava = (Rakennus)Rakennukset[i];
                int k = 0;
                while (k < tsekattava.annaVektoriLkm())
                {
                    Vektori temp = (Vektori)tsekattava.annaVektori(k);
                   // System.Console.WriteLine("vektori: " + temp.getX() + " " + temp.getY());
                    if (temp.getX() > minX && temp.getX() < maxX && temp.getY() > minY && temp.getY() < maxY)
                    {
                       // System.Console.WriteLine("rakennus rajoissa!");
                        palautus.Add(tsekattava);
                        break;
                    }
                    k++;
                }
            }
            return palautus;        
        }
        /// <summary>
        /// palauttaa listan parametrien rajaaman alueen vesi-olioista
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public ArrayList annaAlueVedet(double minX, double minY, double maxX, double maxY)
        {
            System.Console.WriteLine("ladataan vedet alueelta " + minX + "," + minY + " - " + maxX + "," + maxY + "..");
            ArrayList palautus = new ArrayList();

            for (int i = 0; i < Vedet.Count; i++)
            {
                Vesi tsekattava = (Vesi)Vedet[i];
                int k = 0;
                while (k < tsekattava.annaVektoriLkm())
                {
                    Vektori temp = (Vektori)tsekattava.annaVektori(k);
                    if (temp.getX() > minX && temp.getX() < maxX &&  temp.getY() > minY && temp.getY() < maxY)
                    {
                        palautus.Add(tsekattava);
                        break;
                    }
                    k++;
                }
            }
            return palautus;
        }

    
        /// <summary>
        /// luo pelioliokokoelman kordinaattien perusteella netistä apin avulla
        /// </summary>
        /// <param name="minlat"></param>
        /// <param name="minlon"></param>
        /// <param name="maxlat"></param>
        /// <param name="maxlon"></param>
        /// <param name="resox">luotavan maailman kordinaattien maksimi x</param>
        /// <param name="resoy">luotavan maailman kordinaattien maksimi y</param>
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
                
                      
        }
        /// <summary>
        /// luo pelioliokokoelman kordinaattien perusteella xml tiedostosta
        /// </summary>
        /// <param name="minlat"></param>
        /// <param name="minlon"></param>
        /// <param name="maxlat"></param>
        /// <param name="maxlon"></param>
        /// <param name="resox">luotavan maailman kordinaattien maksimi x</param>
        /// <param name="resoy">luotavan maailman kordinaattien maksimi y</param>
        /// <param name="filename">xml tiedoston nimi, ajettavan exen hakemistossa</param>
        
        public Peli(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy, string filename)
        {
           
            XMLData luettudata = XMLLukija.LueXML(filename);

            Rakennukset = new ArrayList();
            Vedet = new ArrayList();

            LataaData(minlat, minlon, maxlat, maxlon, resox, resoy, luettudata);
           System.Console.WriteLine("Koko Data ladattu sisälle.");
          
        }


=======
                    }          
                }
                Rakennus uusirakennus = new Rakennus(temprakennus.annaNimi(), tempvektorit);
                Rakennukset.Add(uusirakennus);
            }

        }




        // itse systeemin konstruktori voisi säilyä kuitenkin tässä tiedostossa -joel
        public Peli(String filename)
        {

                XMLData luettudata = XMLLukija.LueXML(filename);
                
                Rakennukset = new ArrayList();

                LataaRakennukset(luettudata);
                System.Console.WriteLine("Enteriä ja listataan kaikkien rakennusten data ");
                System.Console.ReadLine();
                // debuggausta
                listaaRakennuksetKonsoliin();
                System.Console.ReadLine();
              
        }

>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769
        // keskeisimmät pelilogiikan metodit, joita kutsutaan jollain timerillä luupissa, nämä voisi myös kirjoittaa tähän tiedostoon, vai (?) -joel
        public void update() { }
        public void scan() { }
        public void move() { }


<<<<<<< HEAD
        // testiohjelma: ilman viimeistä paramaetriä (tiedostoa), latautuu jyväskylän keskustan alue. tiedosto mukana..
        
        static void EntinenMaini(string[] args)
        {
                    // esimerkiksi jkl:n: keskustan alue, resoluutiolle 1000x1000. ilman viimeistä parametriä lataa saman alueen suoraan api:sta 
           Peli testipeli = new Peli(62.23339, 25.71007, 62.25034, 25.75491, 1000, 1000, "keskusta.osm");
           // ladataan vain alueella 0-300, 0-300 olevat kamat
           ArrayList talot = testipeli.annaAlueRakennukset(0, 0, 300, 300);
           ArrayList vedet = testipeli.annaAlueVedet(0, 0, 300, 300);
           // ja tulostellaan konsoliin 
           System.Console.WriteLine("alueella " + talot.Count + " rakennusta");
           for (int i = 0; i < talot.Count; i++)
           {
               Rakennus yksi = (Rakennus)talot[i];
            yksi.TulostaKonsoliin();
           }
           System.Console.WriteLine("alueella " + vedet.Count + " vettä");
           for (int i = 0; i < vedet.Count; i++)
           {
               Vesi yksi = (Vesi)vedet[i];
               yksi.TulostaKonsoliin();
           }

           System.Console.ReadLine();
=======
        // testiohjelma, lutakon alueella. voi koittaa myös jkl.osm, jolloin tulee yli 18 tuhatta noodia ja melkein tuhat rakennusta 
        static void Mainfff(string[] args)
        {
            Peli testipeli = new Peli("lutakko.osm");
>>>>>>> c6c511e293055e641244ee4e4a56238a9aba9769

        }  

    }

}
