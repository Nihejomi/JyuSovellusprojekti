using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;


namespace HTMLParser
{
    class Parsinta
    {

        public int zombieMaara;
        public void Alusta(string mista)//(entinen maini string[] args)
        {

            string kunta;
            string asukasluku;
            string saa;
           

            List<string> listaKunnista = new List<String>();
            List<string> lista = new List<String>();

            kunta = mista; //kysyKysymys();

            lista = haeKunnat(listaKunnista);
            asukasluku = haeAsukasluku(kunta, listaKunnista);
            zombieMaara = zombieLuku(asukasluku);

            saa = haeSaa(kunta);
           // tulosta(kunta, asukasluku, saa, zombieMaara);

        }

        private static int zombieLuku(string asukasluku)
        {

            int asukasLUKU;
            int.TryParse(asukasluku, out asukasLUKU);

            int zombieProsentti = 5;
            int zombieMaara = zombieProsentti * asukasLUKU / 100;
            return zombieMaara;
        }


        private static string kysyKysymys()
        {
            Console.WriteLine("Minkä kunnan tiedot haetaan?");
            string kunnanNimi = Console.ReadLine();
            return kunnanNimi;

        }

        private static List<string> haeKunnat(List<string> listaKunnista)
        {

            string htmlKunnat = "http://fi.wikipedia.org/wiki/Luettelo_Suomen_kunnista";
            HtmlDocument doc = new HtmlWeb().Load(htmlKunnat);
            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");

            for (int i = 1; i < rows.Count; ++i)
            {
                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");

                listaKunnista.Add(cols[0].InnerText);


            }

            return listaKunnista;
        }

        private static void tulosta(string kunta, string asukasluku, string saa, int zombieMaara)
        {
            Console.WriteLine("Kunnannimi: " + kunta);
            Console.WriteLine("Kunnan asukasluku: " + asukasluku);
            Console.WriteLine("Kunnan tämän hetkinen sää: " + saa);
            Console.WriteLine("Kunnan mahdollinen zombie määrä: " + zombieMaara);
            Console.ReadKey();
        }

        private static string haeSaa(string kunta)
        {

            string htmlSaa = "http://www.foreca.fi/Finland/" + kunta;
            HtmlDocument docX = new HtmlWeb().Load(htmlSaa);

            HtmlNodeCollection node = docX.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'txt-xxlarge', ' '))]");
            foreach (HtmlNode node1 in node)
            {
                return node1.InnerText;
            }
            return "";
        }

        private static string haeAsukasluku(string kunta, List<string> lista)
        {
            string htmlKunnat = "http://fi.wikipedia.org/wiki/Luettelo_Suomen_kunnista";
            HtmlDocument doc = new HtmlWeb().Load(htmlKunnat);
            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");


            if (lista.Contains(kunta))
            {


                for (int i = 1; i < rows.Count; ++i)
                {

                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                    string kunnannimi = cols[0].InnerText;
                    if (kunnannimi == kunta)
                    {

                        string asukasluku = cols[3].InnerText;
                        if (asukasluku.Contains("&#160;"))
                        {
                            string uusvakiluku = asukasluku.Replace("&#160;", "");
                            return uusvakiluku;
                        }
                        else
                        {
                            return asukasluku;
                        }





                    }

                }
                return "";
            }
            return "";
        }


    }
}