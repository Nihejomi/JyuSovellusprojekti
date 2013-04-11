using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;

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
            string paskat = formatAddress(minlat, minlon, maxlat, maxlon);
            return client.DownloadString(paskat);
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
         * Tässä formatoidaan syötettyjä arvoja, etteivät desimaalimerkit sössi tuota osoitteen luontia. Tullaan käyttämään mahdollisesti muuhunkin.
         */
        private string formatAddress(double minlat, double minlon, double maxlat, double maxlon)
        {
            return string.Format("http://overpass-api.de/api/interpreter?data=(node({0},{1},{2},{3});<;);out;", minlat.ToString(culture), minlon.ToString(culture), maxlat.ToString(culture), maxlon.ToString(culture));
        }
    }
}
