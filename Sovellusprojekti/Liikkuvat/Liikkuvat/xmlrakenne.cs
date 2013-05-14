/*  KMKK-sovellusprojekti: Avointa Dataa hyödyntävä Zombie-selviytymispeli 
 *  
 *  XML-Parseri lukemaan xml-tiedostosta pelissä käytettävä karttadata
 *  Palauttaa kaiken karttadatan XMLData tyyppisenä oliona
 *  versio 0.04 (lopullinen?)
 *  Joel Kivelä joaakive@student.jyu.fi
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
        ArrayList noderefs; // lista noodireferensseistä
        String Name; // TODO: xmlssä rakennuksen nimi on tag ="name", v=  <data>

        // konstruktori, ottaa nodetrefenssit parametrinä
        public XMLRakennus(double id, ArrayList nodet, String name)
        {
            this.id = id;
            this.noderefs = new ArrayList(nodet);
            this.Name = name;
        }

        // palauttaa rakennuksessa olevien noodien lukumäärän
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


    public class XMLVesi
    {
        ArrayList noderefs;
        double id;
        String Name;


          // konstruktori, ottaa nodetrefenssit parametrinä
        public XMLVesi(double id, ArrayList nodet, String name)
        {
            this.id = id;
            this.noderefs = new ArrayList(nodet);
            this.Name = name;
        }

        // palauttaa rakennuksessa olevien noodien lukumäärän
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
            System.Console.WriteLine("vesistö muistissa:::  id: " + this.id + " nodeja: " + this.noderefs.Count + " nimi:" + this.Name);
        }

        
    }


    public class XMLTie
    {
        ArrayList noderefs;
        double id;
        int tyyppi;
        String Name;


        // konstruktori, ottaa nodetrefenssit parametrinä
        public XMLTie(double id, ArrayList nodet, String name, int tyyppi)
        {
            this.id = id;
            this.noderefs = new ArrayList(nodet);
            this.Name = name;
            this.tyyppi = tyyppi;
        }

        // palauttaa rakennuksessa olevien noodien lukumäärän
        public double annaId()
        {
            return this.id;
        }

        public String annaNimi()
        {
            return this.Name;
        }
        public int annaTyyppi()
        {
            return this.tyyppi;
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
            System.Console.WriteLine("tie muistissa:::  id: " + this.id + " nodeja: " + this.noderefs.Count + " nimi:" + this.Name);
        }


    }


    public class XMLRuoho
    {
        ArrayList noderefs;
        double id;
        String Name;
        int tyyppi;  // 0 = ruohikko, 1 = metsä, 2 = suo


        // konstruktori, ottaa nodetrefenssit parametrinä
        public XMLRuoho(double id, ArrayList nodet, String name, int tyyppi)
        {
            this.id = id;
            this.tyyppi = tyyppi;
            this.noderefs = new ArrayList(nodet);
            this.Name = name;
        }

        // palauttaa rakennuksessa olevien noodien lukumäärän
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
        public int annaTyyppi()
        {
            return this.tyyppi;
        }
        // palauttaa refenssilistasta i:n mukaisen nooditunnisteen
        public double annaNoodiId(int i)
        {
            return (Double)this.noderefs[i];
        }


        // debuggaukseen
        public void printtaa()
        {
            System.Console.WriteLine("ruohoalue muistissa:::  id: " + this.id + " nodeja: " + this.noderefs.Count + " nimi:" + this.Name);
        }


    }


    // Varsinainen XML:lstä parsittu data
    public class XMLData
    {
        ArrayList Noodit;
        ArrayList Rakennukset;
        ArrayList Vedet;
        ArrayList Tiet;
        ArrayList Ruohot;

        public XMLData(ArrayList Noodit, ArrayList Rakennukset, ArrayList Vedet, ArrayList Tiet, ArrayList Ruohot)
        {
            this.Noodit = new ArrayList(Noodit);
            this.Rakennukset = new ArrayList(Rakennukset);
            this.Vedet = new ArrayList(Vedet);
            this.Tiet = new ArrayList(Tiet);
            this.Ruohot = new ArrayList(Ruohot);
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

       

        public int annaTieLkm()
        {
            return Tiet.Count;
        }

        public int annaRuohoLkm()
        {
            return Ruohot.Count;
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
        public XMLTie annaTie(int i)
        {
            return (XMLTie)Tiet[i];
        }
        public XMLRuoho annaRuoho(int i)
        {
            return (XMLRuoho)Ruohot[i];
        }

    } // endof XMLData

    public class XMLLukija  // XML-lukijan luokka
    {   
            // Varsinainen parserimetodi   TODO: koodin selvennystä, debuggauksien poistoa jossain vaiheessa, kun sitä ei tarvitse enää ollenkaan 

        public static XMLData LueXML(String filename)
        {

            NumberStyles style = NumberStyles.AllowDecimalPoint;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

            ArrayList XmlNoodit = new ArrayList();
            ArrayList XmlRakennukset = new ArrayList();
            ArrayList XmlVedet = new ArrayList();
            ArrayList XmlTiet = new ArrayList();
            ArrayList XmlRuohot = new ArrayList();

            ArrayList reftemp = new ArrayList();

            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            BufferedStream stream = new BufferedStream(file);
            XmlTextReader reader = new XmlTextReader(stream);

            int lukutyyppi = 0;  // 1= noodi, 2= tie, 3=talo/vesi


            double tempId = 0;
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
                        }

                        if (reader.Name == "way")
                        {
                            lukutyyppi = 2;

                        }

                        if (lukutyyppi == 2 && reader.Name == "nd")
                        {
                            //refluku = true;
                        }


                        if (reader.AttributeCount > 0)
                        {

                            int attributeCount = reader.AttributeCount;
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
                                    // jos kaikki kolme paramaetriä tuli, noodin tallennus arraylistiin
                                    if (count == 3)
                                    {
                                        XMLNoodi uusiNoodi = new XMLNoodi(tempId, tempLat, tempLon);
                                        XmlNoodit.Add(uusiNoodi);
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
                                        reftemp.Add(tempRef); // lisätään refenssi referenssien temp-listaan
                                        tempRef = 0;
                                    }

                                    // tie

                                    if (reader.Name == "k" && reader.Value.ToString() == "highway")
                                    {
                                        i++;
                                        reader.MoveToAttribute(i);

                                        // näitä ehtoja vähentämällä saa laskettua tieolioiden kuormaa 
                                        if ( (reader.Name == "v" && reader.Value.ToString() == "footway") ||  (reader.Name == "v" && reader.Value.ToString() == "motorway") ||
                                            (reader.Name == "v" && reader.Value.ToString() == "cycleway") ||  (reader.Name == "v" && reader.Value.ToString() == "residential") ||
                                            (reader.Name == "v" && reader.Value.ToString() == "secondary" || reader.Name == "v" && reader.Value.ToString() == "tertiary" || reader.Name == "v" && reader.Value.ToString() == "pedestrian"))
                                        {
                                            int tyyppi = 0;
                                            if (reader.Value.ToString() == "motorway" || reader.Value.ToString() == "residential" || reader.Value.ToString() == "secondary" || reader.Value.ToString() == "tertiary")
                                            {
                                                tyyppi = 1;
                                            }
                                            /*
                                            if (reader.Value.ToString() == "footway" || reader.Value.ToString() == "cycleway")
                                            {
                                                tyyppi = 0;
                                            }
                                            */
                                            String nimi = "nimeton";

                                            XMLTie uusi = new XMLTie(tempId, reftemp, nimi, tyyppi);
                                            XmlTiet.Add(uusi);
                                            tempId = 0;
                                            auki = false;
                                            vauki = false;
                                        }

                                    }

                                            // luettu elementti oli rakennus, luodaan siis uusi rakennusolio ja tehdään tallennus
                                    if (reader.Name == "k" && reader.Value.ToString() == "building") 
                                    {

                                        String nimi = "nimeton";                                                                                                          
                                        XMLRakennus uusi = new XMLRakennus(tempId, reftemp, nimi);                                       
                                        XmlRakennukset.Add(uusi);
                                        tempId = 0;
                                        muutettava = XmlRakennukset.Count-1;
                                        auki = true;
                                        vauki = false;
                                    }

                                    // rakennukselle nimi                                  
                                   if (reader.Name == "k" && reader.Value.ToString() == "name" && auki && !vauki)
                                   {
                                       reader.MoveToAttribute(i + 1);                                  
                                       XMLRakennus temp = (XMLRakennus)XmlRakennukset[muutettava];
                                       temp.muutaNimi(reader.Value.ToString());
                                       auki = false;

                                   }

                                   if ((reader.Name == "v" && reader.Value.ToString() == "meadow") || (reader.Name == "v" && reader.Value.ToString() == "grass")
                                       || (reader.Name == "v" && reader.Value.ToString() == "forest") || (reader.Name == "v" && reader.Value.ToString() == "park" )
                                       || (reader.Name == "v" && reader.Value.ToString() == "wood") || (reader.Name == "v" && reader.Value.ToString() == "wetland"))  // tallennetaan ruohoalue talon tapaisesti
                                   {

                                       String nimi = "nimeton";


                                       int tyyppi=0;

                                       if (reader.Value.ToString() == "meadow" || reader.Value.ToString() == "grass" || reader.Value.ToString() == "park")
                                           tyyppi = 0;
                                       if (reader.Value.ToString() == "forest" || reader.Value.ToString() == "wood")
                                           tyyppi = 1;

                                       if (reader.Value.ToString() == "wetland" || reader.Value.ToString() == "swamp")
                                           tyyppi = 2;

                                       XMLRuoho uusi = new XMLRuoho(tempId, reftemp, nimi, tyyppi);
                                       XmlRuohot.Add(uusi);
                                       tempId = 0;

                                       //muutettavaV = XmlVedet.Count - 1;
                                       auki = false;
                                       vauki = false;
                                   }         

                                    //vesi,    nimi ennen päätunnistetta 
                                    if (reader.Name == "v" && reader.Value.ToString() == "water")  // tallennetaan järvi talon tapaisesti
                                    {

                                        String nimi = "nimeton";                                    
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
                            tempId = 0;
                        }
                        if (reader.Name == "way")
                        {
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

            XMLData valmisData = new XMLData(XmlNoodit, XmlRakennukset, XmlVedet, XmlTiet, XmlRuohot);
            System.Console.WriteLine("Parsiminen valmis");
            System.Console.WriteLine("noodeja tuli: " + valmisData.annaNoodiLkm());
            System.Console.WriteLine("rakennuksia : " + valmisData.annaRakennusLkm());
            System.Console.WriteLine("vesistöjä : " + valmisData.annaVesiLkm());
            System.Console.WriteLine("teitä : " + valmisData.annaTieLkm());
            System.Console.WriteLine("ruohoja : " + valmisData.annaRuohoLkm());
            return valmisData;

        }

    }

}


	