using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Saa
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            String file = "http://fi.wikipedia.org/wiki/Luettelo_Suomen_kunnista";
            HtmlDocument doc = new HtmlWeb().Load(file);
            //doc.LoadHtml(file);

            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");

           
            for (int i = 1; i < rows.Count; ++i)
            {
                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");

                string kunta = cols[0].InnerText;
                
                string vakiluku = cols[3].InnerText;
                
                String html = "http://www.foreca.fi/Finland/" + kunta;
                                HtmlDocument docX = new HtmlWeb().Load(html);
                                
                                HtmlNodeCollection node = docX.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'txt-xxlarge', ' '))]");
                                foreach (HtmlNode node1 in node)
                                {

                                   Console.WriteLine("Kunta : " + kunta);
                                    if (vakiluku.Contains("&#160;"))
                                    {
                                        vakiluku.Replace("&#160;", " ");
                                        Console.WriteLine("Väkiluku : " + vakiluku);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Väkiluku : " + vakiluku);
                                    }
                                   Console.WriteLine("Lämpötila : " + node1.InnerText);
                                   Console.ReadKey();
                   
                                }

            

            }


              
                 
            }

        }
    }




