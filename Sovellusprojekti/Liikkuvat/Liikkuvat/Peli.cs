/*  KMKK-sovellusprojekti: Avointa Dataa hyödyntävä Zombie-selviytymispeli 
 *  versio 0.06
 *  Luokat xml-datasta luetuille objekteilla pelimaailmaan
 *  
 *  Lisäsin metodit Maailmakordinaattien maksimien palautukselle Peli-oliosta.
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
        //lisäsin törmäys tarkistuksen optimointia varten
        public double minx=0;
        public double maxx=0;
        public double miny=0;
        public double maxy=0;

        int tyyppi;
        String nimi;
        //int tyyppi;
        ArrayList vektorit; // Vektorit tulevat niin että ensimmäinen ja vika on samat, piirto todennäköisesti järjestyksessä pitkin. -joel
        public Rakennus(String nimi, ArrayList vektorit, int tyyppi)
        {
            this.tyyppi = tyyppi;
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
        public int annaVektoriLkm(){
               return this.vektorit.Count;
        }
        public int annaTyyppi()
        {
            return this.tyyppi;
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


    public class Tie{
       String nimi;
       ArrayList vektorit;
       int tyyppi;

         public Tie(String nimi, ArrayList vektorit, int tyyppi)
        {
            this.nimi = nimi;
            this.vektorit = new ArrayList(vektorit);
            this.tyyppi = tyyppi;
        }
         public int annaTyyppi()
         {
             return this.tyyppi;
         }
         public bool tarkistaPiste(Vektori vektori)
         {
             return false;
         }

         public void TulostaKonsoliin()
         {
             System.Console.WriteLine("Tien nimi: " + this.nimi + " vektorit: ");

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
         public int annaVektoriLkm()
         {
             return this.vektorit.Count;
         }

    }

    public class Ruoho
    {
        String nimi;
        ArrayList vektorit;
        int tyyppi;  // 0 = ruohikko, 1 = metsä, 2 = suo

        public Ruoho(String nimi, ArrayList vektorit, int tyyppi)
        {
            this.nimi = nimi;
            this.vektorit = new ArrayList(vektorit);
            this.tyyppi = tyyppi;
        }

        public int annaTyyppi()
        {
            return this.tyyppi;
        }

        public bool tarkistaPiste(Vektori vektori)
        {
            return false;
        }

        public void TulostaKonsoliin()
        {
            System.Console.WriteLine("Ruohon nimi: " + this.nimi + " vektorit: ");

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
        public int annaVektoriLkm()
        {
            return this.vektorit.Count;
        }

    }


    class Peli

    {
        // Tänne kutakin pelikenttää/aluetta vastaavat oliot ja datat

        // kuva ?
        // säätila
        // väkiluku
   
        ArrayList Rakennukset;
        ArrayList Vedet;
        ArrayList Tiet;
        ArrayList Ruohot;

         //ArrayList Tiet;

        // clienti, jolla ladataan apista xml-tiedostoja.
        OpenStreetMapClient lataaja = new OpenStreetMapClient();

        // koordinaattivektoreiden muuntelua näillä parametreillä -joel
        double scalex;
        double scaley;
        double offsetx;
        double offsety;


        int xreso;
        int yreso;

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
        public void LataaData(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy, XMLData luettudata, bool testaus)
        {

            this.xreso = resox;
            this.yreso = resoy;
            // muuntaa xml-koordinaatiston sopivaan pelikoordinaatti muotoon
            offsetx = -minlon;
            offsety = -minlat;
            scalex = (1 / (maxlon-minlon)) * resox;
            scaley = (1 / (maxlat-minlat)) * resoy;



            System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle...: rakennuksia yhteensä " + luettudata.annaRakennusLkm() );
            for (int i = 0; i < luettudata.annaRakennusLkm(); i++)
            {
                int tyyppi;
                XMLRakennus temprakennus = (luettudata.annaRakennus(i));
                ArrayList tempvektorit = new ArrayList();
                tyyppi = temprakennus.annaTyyppi();
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
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) *this.scalex, resoy-((tempnoodi.getLat() + this.offsety) *this.scaley));
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
                    Rakennus uusirakennus = new Rakennus(temprakennus.annaNimi(), tempvektorit, tyyppi);
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
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) *this.scalex, resoy-((tempnoodi.getLat() + this.offsety) *this.scaley));
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
            if (testaus == false)
            {
                System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle..: teitä yhteensä " + luettudata.annaTieLkm());
                for (int i = 0; i < luettudata.annaTieLkm(); i++)
                {
                    XMLTie temptie = (luettudata.annaTie(i));
                    int tyyppi = 0;
                    ArrayList tempvektorit = new ArrayList();
                    for (int k = 0; k < temptie.annaNoodiLkm(); k++)
                    {
                        // etsitään rakennuksen noodirefensseille vastaavuudet noodikokoelmasta 
                        double haettava = temptie.annaNoodiId(k);

                        bool find = false;
                        int l = 0;
                        while (!find)
                        {

                            XMLNoodi tempnoodi = luettudata.annaNoodi(l);

                            if (tempnoodi.getId() == haettava)
                            {
                                // tallennetaan rakennusten luokkaan tällaisella kaavalla, TODO: oikeanlainen muunnos jotta saadaan järkevät koordinaatit käyttöön
                                Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) * this.scalex, resoy - ((tempnoodi.getLat() + this.offsety) * this.scaley));
                                tempvektorit.Add(tempvektori);

                                find = true;
                            }
                            l++;
                            if (l >= luettudata.annaNoodiLkm())
                            {
                                break;
                            }

                        }
                    }

                    if (tempvektorit.Count != 0)
                    {
                        tyyppi = temptie.annaTyyppi();
                        Tie uusitie = new Tie("tie", tempvektorit, tyyppi);
                        Tiet.Add(uusitie);
                    }
                }
            }
            System.Console.WriteLine("XML->Peli: Muunnetaan tiedot pelin sisälle..: ruohoalueita yhteensä " + luettudata.annaRuohoLkm());
            for (int i = 0; i < luettudata.annaRuohoLkm(); i++)
            {
                XMLRuoho tempruoho = (luettudata.annaRuoho(i));
                int tyyppi = tempruoho.annaTyyppi();
                ArrayList tempvektorit = new ArrayList();
                for (int k = 0; k < tempruoho.annaNoodiLkm(); k++)
                {
                    // etsitään rakennuksen noodirefensseille vastaavuudet noodikokoelmasta 
                    double haettava = tempruoho.annaNoodiId(k);
                    
                    bool find = false;
                    int l = 0;
                    while (!find)
                    {

                        XMLNoodi tempnoodi = luettudata.annaNoodi(l);

                        if (tempnoodi.getId() == haettava)
                        {
                   
                            // tallennetaan rakennusten luokkaan tällaisella kaavalla, TODO: oikeanlainen muunnos jotta saadaan järkevät koordinaatit käyttöön
                            Vektori tempvektori = new Vektori((tempnoodi.getLon() + this.offsetx) * this.scalex, resoy - ((tempnoodi.getLat() + this.offsety) * this.scaley));
                            tempvektorit.Add(tempvektori);

                            find = true;
                        }
                        l++;
                        if (l > luettudata.annaNoodiLkm() -1)
                        {
                   
                            break;
                        }

                    }
                }

                if (tempvektorit.Count != 0)
                {
                   
                    Ruoho uusiruoho = new Ruoho("ruoho", tempvektorit, tyyppi);
                    Ruohot.Add(uusiruoho);
                }
            }
           
        }
        public int annaResoX()
        {
            return this.xreso;
        }
        public int annaResoY()
        {
            return this.yreso;
        }

        public int annaRakennusLkm()
        {
            return this.Rakennukset.Count;
        }
        public int annaTieLkm()
        {
            return this.Tiet.Count;
        }
        public int annaRuohoLkm()
        {
            return this.Ruohot.Count;
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
        public Tie annaTie(int i)
        {
            Tie luettutie = (Tie)Tiet[i];
            return luettutie;
        }
        public Ruoho annaRuoho(int i)
        {
            Ruoho luetturuoho = (Ruoho)Ruohot[i];
            return luetturuoho;
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
        /// palauttaa listan parametrien rajaaman alueen tie-olioista
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public ArrayList annaAlueTiet(double minX, double minY, double maxX, double maxY)
        {
            System.Console.WriteLine("ladataan tiet alueelta " + minX + "," + minY + " - " + maxX + "," + maxY + "..");
            ArrayList palautus = new ArrayList();

            for (int i = 0; i < Tiet.Count; i++)
            {
                Tie tsekattava = (Tie)Tiet[i];
                int k = 0;
                while (k < tsekattava.annaVektoriLkm())
                {
                    Vektori temp = (Vektori)tsekattava.annaVektori(k);
                    if (temp.getX() > minX && temp.getX() < maxX && temp.getY() > minY && temp.getY() < maxY)
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
        /// palauttaa listan parametrien rajaaman alueen ruoho-olioista
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public ArrayList annaAlueRuohot(double minX, double minY, double maxX, double maxY)
        {
            System.Console.WriteLine("ladataan ruohot alueelta " + minX + "," + minY + " - " + maxX + "," + maxY + "..");
            ArrayList palautus = new ArrayList();

            for (int i = 0; i < Ruohot.Count; i++)
            {
                Ruoho tsekattava = (Ruoho)Ruohot[i];
                int k = 0;
                while (k < tsekattava.annaVektoriLkm())
                {
                    Vektori temp = (Vektori)tsekattava.annaVektori(k);
                    if (temp.getX() > minX && temp.getX() < maxX && temp.getY() > minY && temp.getY() < maxY)
                    {
                        palautus.Add(tsekattava);
                        break;
                    }
                    k++;
                }
            }
            return palautus;
        }


        public Peli(string kaupunki, int resox, int resoy, bool testaus)
        {
            System.Console.WriteLine("ladataa kaupungin node openstreetmap apista...");
            lataaja.downloadOSMFile(kaupunki, "city.osm");
            //parsi kordinaatit
            double[] kord = lataaja.calculateBBox(0, 0, 0.5);

            lataaja.downloadOSMFile(kord[0], kord[1], kord[2], kord[3], "temp.osm");
            
            XMLData luettudata = XMLLukija.LueXML("temp.osm");

            Rakennukset = new ArrayList();
            Vedet = new ArrayList();
            Tiet = new ArrayList();
            Ruohot = new ArrayList();

            LataaData(kord[0], kord[1], kord[2], kord[3], resox, resoy, luettudata, testaus);
            System.Console.WriteLine("Koko Data ladattu sisälle.");
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
        /// <param name="testaus"> onko testaus vai ei (jättää tiet lataamatta) </param>
        public Peli(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy, bool testaus)
        {
            System.Console.WriteLine("ladataan xml openstreetmapin apista...");
         lataaja.downloadOSMFile(minlat, minlon, maxlat, maxlon, "temp.osm");
          //lataaja.downloadMapPic(minlat + (maxlat - minlat)/2, minlon + (maxlon - minlon)/2, "testi.png");

               XMLData luettudata = XMLLukija.LueXML("temp.osm");
                
                Rakennukset = new ArrayList();
                Vedet = new ArrayList();
                Tiet = new ArrayList();
                Ruohot = new ArrayList();

                LataaData(minlat, minlon, maxlat, maxlon, resox, resoy, luettudata, testaus);
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
        /// <param name="testaus"> onko testaus vai ei (jättää tiet lataamatta) </param>
        
        public Peli(double minlat, double minlon, double maxlat, double maxlon, int resox, int resoy, string filename, bool testaus)
        {
            
            XMLData luettudata = XMLLukija.LueXML(filename);

            Rakennukset = new ArrayList();
            Vedet = new ArrayList();
            Tiet = new ArrayList();
            Ruohot = new ArrayList();

            LataaData(minlat, minlon, maxlat, maxlon, resox, resoy, luettudata, testaus);
           System.Console.WriteLine("Koko Data ladattu sisälle.");
          
        }

       
    }

}
