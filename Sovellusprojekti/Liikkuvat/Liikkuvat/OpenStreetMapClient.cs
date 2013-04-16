using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;
using System.Drawing;

namespace Peli
{
    /*
     * Yksinkertainen luokka, joka hakee pyydettyjen pituus ja leveys tietojen perusteella OpenStreetMap-tietoja Overpass API-servereiltä 
     */
    class OpenStreetMapClient
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
        WebClient client = new WebClient();

        /*
         * TODO: kunnollinen virheiden käsittely hakiessa tietoja, syötettyjen tietojen tarkistus, lista OpenStreetMapin Overpass API-servereistä,
         * lisää toiminnallisuuksi: tietyn kokoinen alue pisteen ympäriltä 
         */
        //http://api.openstreetmap.fr/oapi/interpreter
        //http://overpass.osm.rambler.ru/cgi/interpreter

        /*
         * Pyytää annettujen pituus ja leveys kordinaattejen pohjalta suorakulmion muotoiselta alueelta kaikki tiedot merkkijonona. Merkkijono noudattaa XML:n tapaista OpenStreetMapin
         * omaa formaattia, jonka keskiössä on Nodet 
         */
        public string downloadOSMstring(double minlat, double minlon, double maxlat, double maxlon)
        {
            return client.DownloadString(formatAddress(minlat, minlon, maxlat, maxlon));
        }

        /*
         * Pyytää annettujen pituus ja leveys kordinaattejen pohjalta suorakulmion muotoiselta alueelta kaikki tiedot tiedoston muodossa. Tiedoston sisältö noudattaa XML:n tapaista OpenStreetMapin
         * omaa formaattia, jonka keskiössä on Nodet 
         */
        public void downloadOSMfile(double minlat, double minlon, double maxlat, double maxlon, string filename)
        {
            client.DownloadFile(formatAddress(minlat, minlon, maxlat, maxlon), filename);
        }

        /*
        * Tietyn pisteen ympäriltä neliökartta
        */
        public void downloadOSMfile(double lat, double lon, string filename)
        {
            double value = 0.0078125/2;
            double minlat = lat - value; double minlon = lon - value; double maxlat = lat + value; double maxlon = lon + value;
            client.DownloadFile(formatAddress(minlat, minlon, maxlat, maxlon), filename);
        }

        /*
         * Varoitus! Tämä ottaa kuvan koordinaattejen keskeltä, ja se ei ole niin iso kuin voisi kuvitella
         */
        public Image downloadMapPic(double minlat, double minlon, double maxlat, double maxlon, string filename)
        {
            string centerlat = (minlat + (maxlat - minlat / 2)).ToString(culture);
            string centerlon = (minlon + (maxlon - minlon / 2)).ToString(culture);
            string address = string.Format("http://ojw.dev.openstreetmap.org/StaticMap/?lat={0}&lon={1}&z=18&w=2000&h=2000&att=none&mode=Export&show=1", centerlat, centerlon);
            client.DownloadFile(address, filename);
            Image img = Image.FromFile(filename);
            return img;
        }

        public Image downloadMapPic(double lat, double lon, string filename)
        {
            string address = string.Format("http://ojw.dev.openstreetmap.org/StaticMap/?lat={0}&lon={1}&z=18&w=2000&h=2000&att=none&mode=Export&show=1", lat.ToString(culture), lon.ToString(culture));
            client.DownloadFile(address, filename);
            Image img = Image.FromFile(filename);
            return img;
        }

        /*
         * Tässä formatoidaan syötettyjä arvoja, etteivät desimaalimerkit sössi tuota osoitteen luontia. Tullaan käyttämään mahdollisesti muuhunkin.
         */
        private string formatAddress(double minlat, double minlon, double maxlat, double maxlon)
        {
            return string.Format("http://overpass-api.de/api/interpreter?data=(node({0},{1},{2},{3});<;);out;", minlat.ToString(culture), minlon.ToString(culture), maxlat.ToString(culture), maxlon.ToString(culture));
        }
    }
}
