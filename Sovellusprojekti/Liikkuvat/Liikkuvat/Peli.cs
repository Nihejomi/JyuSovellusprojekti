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

         //ArrayList Tiet;


        // koordinaattivektoreiden muuntelua näillä parametreillä -joel
        double scale = 100000;
        double offset = 0;


        // debuggausta
        public void listaaRakennuksetKonsoliin()
        {

            for (int i = 0; i < Rakennukset.Count; i++)
            {
                Rakennus luetturakennus = (Rakennus)Rakennukset[i];
                luetturakennus.TulostaKonsoliin();
            }
        }

        // Valmis osa: Siirtää rakennusten datan pelisysteemille nopeasti saataville -joel
        public void LataaRakennukset(XMLData luettudata)
        {
            System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle: rakennuksia yhteensä " + luettudata.annaRakennusLkm() );
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
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() * this.scale)+this.offset, (tempnoodi.getLat() * this.scale)+this.offset);
                            tempvektorit.Add(tempvektori);

                            find = true;
                        }
                        l++;
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

        // keskeisimmät pelilogiikan metodit, joita kutsutaan jollain timerillä luupissa, nämä voisi myös kirjoittaa tähän tiedostoon, vai (?) -joel
        public void update() { }
        public void scan() { }
        public void move() { }


        // testiohjelma, lutakon alueella. voi koittaa myös jkl.osm, jolloin tulee yli 18 tuhatta noodia ja melkein tuhat rakennusta 
        static void Mainfff(string[] args)
        {
            Peli testipeli = new Peli("lutakko.osm");

        }  

    }

}
