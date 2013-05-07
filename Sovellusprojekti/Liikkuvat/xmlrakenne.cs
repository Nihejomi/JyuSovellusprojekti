/*  KMKK-sovellusprojekti: Avointa Dataa hy�dynt�v� Zombie-selviytymispeli 
 *  
 *  XML-Parseri lukemaan xml-tiedostosta peliss� k�ytett�v� karttadata
 *  Palauttaa kaiken karttadatan XMLData tyyppisen� oliona
 *  versio 0.03
 *  Joel Kivel� joaakive@student.jyu.fi
 */

using System;
using System.Collections;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Peli
{
    // yhden noodiolion luokka
    public class XMLNoodi
    {
        // tunniste
        double id; 
 
        // koordinaatit
        double lat; 
        double lon; 

        // konstruktori
        public XMLNoodi(double id, double lat, double lon)
        {
            this.id = id;
            this.lat = lat;
            this.lon = lon;
        }
        
        // getterit ja setterit
        public double getId()
        {
            return this.id;
        }
        public double getLat()
        {
            return this.lat;
        }
        public double getLon()
        {
            return this.lon;
        }
        // debuggaukseen
        public void printtaa()
        {
            System.Console.WriteLine("id: " + this.id + " lat: " + this.lat + " lon: " + this.lon);
        }

    } // endof XMLNoodi


    // yhen rakennusolion luokka
    public class XMLRakennus
    {
        double id; // tunniste
        ArrayList noderefs; // lista noodireferensseist�
        String Name; // TODO: xmlss� rakennuksen nimi on tag ="name", v=  <data>

        // konstruktori, ottaa nodetrefenssit parametrin�
        public XMLRakennus(double id, ArrayList nodet, String name)
        {
            this.id = id;
            this.noderefs = new ArrayList(nodet);
            this.Name = name;
        }

        // palauttaa rakennuksessa olevien noodien lukum��r�n
        public double annaId()
        {
            return this.id;
        }

        public String annaNimi()
        {
            return this.Name;
        }


        public void muutaNimi(String nimi)
        {
            this.Name = nimi;
        }
        public int annaNoodiLkm()
        {
            return this.noderefs.Count;
        }

        // palauttaa refenssilistasta i:n mukaisen nooditunnisteen
        public double annaNoodiId(int i)
        {   
            return (Double)this.noderefs[i];
        }


        // debuggaukseen
        public void printtaa()
        {
            System.Console.WriteLine("rakennus muistissa:::  id: " + this.id + " nodeja: " + this.noderefs.Count + " nimi:" + this.Name);
        }

    } // endof XMLRakennus

    // TODO: tietyypit 
    // tasg K = highway, v = footway/cycleway 
    //public class XMLTie
    //{
     //   ArrayList noderefs; // (double)
     //   String Name; // tags k =name, v = <data>

      //  double id; // tunniste
        //ArrayList noderefs; // lista noodireferensseist�
        //String Name;

    //}



    // TODO: vedet
    // tasg K = highway, v = footway/cycleway 
    public class XMLVesi
    {
        ArrayList noderefs;
        double id;
        String Name;


          // konstruktori, ottaa nodetrefenssit parametrin�
        public XMLVesi(double id, ArrayList nodet, String name)
        {
            this.id = id;
            this.noderefs = new ArrayList(nodet);
            this.Name = name;
        }

        // palauttaa rakennuksessa olevien noodien lukum��r�n
        public double annaId()
        {
            return this.id;
        }

        public String annaNimi()
        {
            return this.Name;
        }


        public void muutaNimi(String nimi)
        {
            this.Name = nimi;
        }
        public int annaNoodiLkm()
        {
            return this.noderefs.Count;
        }

        // palauttaa refenssilistasta i:n mukaisen nooditunnisteen
        public double annaNoodiId(int i)
        {   
            return (Double)this.noderefs[i];
        }


        // debuggaukseen
        public void printtaa()
        {
            System.Console.WriteLine("vesist� muistissa:::  id: " + this.id + " nodeja: " + this.noderefs.Count + " nimi:" + this.Name);
        }

        
    }

    // Varsinainen XML:lst� parsittu data
    public class XMLData
    {
        ArrayList Noodit;
        ArrayList Rakennukset;
        ArrayList Vedet;
       // ArrayList Tiet;

        public XMLData(ArrayList Noodit, ArrayList Rakennukset, ArrayList Vedet)
        {
            this.Noodit = new ArrayList(Noodit);
            this.Rakennukset = new ArrayList(Rakennukset);
            this.Vedet = new ArrayList(Vedet);
        }


        public int annaNoodiLkm()
        {
            return Noodit.Count;
        }

        public int annaRakennusLkm()
        {
            return Rakennukset.Count;
        }

        public int annaVesiLkm()
        {
            return Vedet.Count;
        }

        public XMLNoodi annaNoodi(int i)
        {
            return (XMLNoodi)Noodit[i];
        }
        public XMLRakennus annaRakennus(int i)
        {
            return (XMLRakennus)Rakennukset[i];
        }

        public XMLVesi annaVesi(int i)
        {
            return (XMLVesi)Vedet[i];
        }


    } // endof XMLData

    public class XMLLukija  // XML-lukijan luokka
    {   
            // Varsinainen parserimetodi   TODO: koodin selvennyst�, debuggauksien poistoa jossain vaiheessa, kun sit� ei tarvitse en�� ollenkaan 

        public static XMLData LueXML(String filename)
        {

            NumberStyles style = NumberStyles.AllowDecimalPoint;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

            ArrayList XmlNoodit = new ArrayList();
            ArrayList XmlRakennukset = new ArrayList();
            ArrayList XmlVedet = new ArrayList();

            ArrayList reftemp = new ArrayList();

            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            BufferedStream stream = new BufferedStream(file);
            XmlTextReader reader = new XmlTextReader(stream);

            int lukutyyppi = 0;  // 1= noodi, 2= tie, 3=talo/vesi


            double tempId = 0;
           // bool refluku = false;
            int muutettava =0;
            int muutettavaV = 0;
            bool auki = false;
            bool vauki = false;
            System.Console.WriteLine("aloitetaan xml-tiedoston " + filename + " parsiminen");


            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // XML elementti alkaa

                        if (reader.Name == "node")
                        {

                            lukutyyppi = 1;
                          //  System.Console.WriteLine("noodin tiedot alkaa");
                        }

                        if (reader.Name == "way")
                        {
                            lukutyyppi = 2;
                           // System.Console.WriteLine("rakennuksen tiedot alkaa");

                        }

                        if (lukutyyppi == 2 && reader.Name == "nd")
                        {
                            //refluku = true;
                        }


                        if (reader.AttributeCount > 0)
                        {

                            int attributeCount = reader.AttributeCount;
                           // System.Console.WriteLine(attributeCount);
                            // luettavien arvoje apumuistit
                            double tempLon = 0;
                            double tempLat = 0;
                            double tempRef = 0;
                            // parametrien summan varmistin
                            int count = 0;

                            for (int i = 0; i < attributeCount; i++)
                            {

                                reader.MoveToAttribute(i);

                                // tapaus = noodi
                                if (lukutyyppi == 1)
                                {

                                    // noodin data
                                    if (reader.Name == "lon")
                                    {
                                        Double.TryParse(reader.Value.ToString(), style, culture, out tempLon); count++;
                                    }
                                    if (reader.Name == "lat")
                                    {
                                        Double.TryParse(reader.Value.ToString(), style, culture, out tempLat); count++;
                                    }
                                    if (reader.Name == "id")
                                    {
                                        Double.TryParse(reader.Value.ToString(), style, culture, out tempId); count++;
                                    }
                                    // jos kaikki kolme paramaetri� tuli, noodin tallennus arraylistiin
                                    if (count == 3)
                                    {
                                        XMLNoodi uusiNoodi = new XMLNoodi(tempId, tempLat, tempLon);
                                        XmlNoodit.Add(uusiNoodi);
                                      // System.Console.WriteLine("noodi tallennettu");
                                        lukutyyppi = 0;
                                    }
                                }

                                // tapaus == rakennus tai vesi

                                if (lukutyyppi == 2)
                                {
                                    if (reader.Name == "id")
                                    {
                                        Double.TryParse(reader.Value.ToString(), style, culture, out tempId);
                                       // System.Console.WriteLine("rakennuksen id:" + tempId);
                                    }
                                    if (reader.Name == "ref")
                                    {
                                        Double.TryParse(reader.Value.ToString(), style, culture, out tempRef);
                                        reftemp.Add(tempRef); // lis�t��n refenssi referenssien temp-listaan
                                        tempRef = 0;
                                    }
                                    
                                            // luettu elementti oli rakennus, luodaan siis uusi rakennusolio ja tehd��n tallennus
                                    if (reader.Name == "k" && reader.Value.ToString() == "building") 
                                    {

                                        String nimi = "nimeton";
                                      //  System.Console.WriteLine("se oli talo.. tallennetaan listaan");
                                       // System.Console.WriteLine("id: " + tempId + " refs: " + reftemp.Count);

                                      //  reader.Read();
                                        //i =0;
                                        
                                           // System.Console.WriteLine(reader.Name + " " + reader.Value.ToString());
                                                                                  
                                               // reader.MoveToAttribute(1);
                                               
                                                //n//imi = reader.Value.ToString();
                                                //System.Console.WriteLine("l�ytyi nimi : " + reader.Value.ToString());
                                                                               
                                        XMLRakennus uusi = new XMLRakennus(tempId, reftemp, nimi);                                       
                                        XmlRakennukset.Add(uusi);
                                        tempId = 0;
                                        muutettava = XmlRakennukset.Count-1;
                                        auki = true;
                                        vauki = false;
                                    }


                                  
                                   if (reader.Name == "k" && reader.Value.ToString() == "name" && auki && !vauki)
                                   {
                                       reader.MoveToAttribute(i + 1);
                                       //System.Console.WriteLine(reader.Name + " " + reader.Value.ToString());
                                       //System.Console.WriteLine("muutettavan talon id" + muutettava);
                                       XMLRakennus temp = (XMLRakennus)XmlRakennukset[muutettava];
                                       temp.muutaNimi(reader.Value.ToString());
                                       auki = false;

                                   }
                                  


                                    //TODO: vesi,    nimi ennen p��tunnistetta 
                                    if (reader.Name == "v" && reader.Value.ToString() == "water")  // tallennetaan j�rvi talon tapaisesti
                                    {

                                        String nimi = "nimeton";
                                        //System.Console.WriteLine("se oli talo.. tallennetaan listaan");
                                        //System.Console.WriteLine("id: " + tempId + " refs: " + reftemp.Count);

                                        //  reader.Read();
                                        //i =0;

                                        // System.Console.WriteLine(reader.Name + " " + reader.Value.ToString());

                                        // reader.MoveToAttribute(1);

                                        //n//imi = reader.Value.ToString();
                                        //System.Console.WriteLine("l�ytyi nimi : " + reader.Value.ToString());

                                        XMLVesi uusi = new XMLVesi(tempId, reftemp, nimi);
                                        XmlVedet.Add(uusi);
                                        tempId = 0;
                                        muutettavaV = XmlVedet.Count-1;
                                        auki = false;
                                        vauki = true;
                                    }
                                   

                                }
                            }


                        }

                        break;


                    case XmlNodeType.EndElement: // XML-elementin loppu
                        if (reader.Name == "node")
                        {
                            //System.Console.WriteLine("noodin tiedot loppui");
                            tempId = 0;
                        }
                        if (reader.Name == "way")
                        {
                           // System.Console.WriteLine("rakennuksen tiedot loppui");
                            reftemp.Clear();
                            tempId = 0;
                            vauki = false;
                            auki = false;
                        }

                        lukutyyppi = 0;
                        muutettava = 0;
                        break;

                }

            }


            XMLData valmisData = new XMLData(XmlNoodit, XmlRakennukset, XmlVedet);
            System.Console.WriteLine("Parsiminen valmis");
            System.Console.WriteLine("noodeja tuli: " + valmisData.annaNoodiLkm());
            System.Console.WriteLine("rakennuksia : " + valmisData.annaRakennusLkm());
            System.Console.WriteLine("vesist�j� : " + valmisData.annaVesiLkm());
            return valmisData;

        }
/*
        // testi/esimerkki -ohjelma
 * 
        static void Main(string[] args)
        {
            // luetaan xml tiedostosta XMLData tyyppiseen luokkaan materiaalit

            XMLData Data = XMLLukija.LueXML("jkl.osm");
            
            System.Console.WriteLine("Parsitaan XML...");

            // tarkistetaan mit� j�i muistiin
            System.Console.WriteLine("noodeja tuli: " + Data.annaNoodiLkm());

            for (int indexi = 0; indexi < Data.annaNoodiLkm(); indexi++)
            {
                XMLNoodi uusio = (XMLNoodi)Data.annaNoodi(indexi);
                //uusio.printtaa();
            }
            System.Console.WriteLine("rakennuksia : " + Data.annaRakennusLkm());
            for (int indexi = 0; indexi < Data.annaRakennusLkm(); indexi++)
            {
                XMLRakennus uusio = (XMLRakennus)Data.annaRakennus(indexi);
                uusio.printtaa();
            }

            System.Console.ReadLine();

        }
 * 
 * */
    }

}


	