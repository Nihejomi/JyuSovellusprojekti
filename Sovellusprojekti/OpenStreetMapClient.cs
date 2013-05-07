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
    /// <summary>
    /// Client for retrieving OpenStreetMap-data from Overpass Api servers.
    /// </summary>
    /// <remarks> created by Artur Kreisberg, v. 0.2 </remarks>
    class OpenStreetMapClient
    {      
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB"); //Used to enforce correct format when converting double to string
        WebClient client = new WebClient();

        private string[] serverList; //holds all available Overpass Api servers, that can be used to retrieve OSM-data
        private string currentServer; //server currently in use
        private string email = "&contact=artkrei@gmail.com"; //contact address, attached to every query

        /// <summary>
        /// Constructor for OpenStreetMapClient-class, initializes serverList, currently doesn't do much
        /// </summary>
        public OpenStreetMapClient()
        {
            serverList = new string[] { "http://overpass-api.de/api/interpreter?data=", "http://overpass.osm.rambler.ru/cgi/interpreter?data=", "http://api.openstreetmap.fr/oapi/interpreter?data=" };
            currentServer = serverList[0];
        }
        
        /*
         * TODO: kunnollinen virheiden käsittely hakiessa tietoja, syötettyjen tietojen tarkistus, lista OpenStreetMapin Overpass API-servereistä,
         * lisää toiminnallisuuksi: tietyn kokoinen alue pisteen ympäriltä
         */

        /// <summary>
        /// Downloads OSM xml-file containing nodes inside given coordinates
        /// </summary>
        /// <param name="minlat">Minimum latitude</param>
        /// <param name="minlon">Minimum longitude</param>
        /// <param name="maxlat">Maximum latitude</param>
        /// <param name="maxlon">Maximum longitude</param>
        /// <param name="filename">Name for the downloaded file</param>
        public void downloadOSMFile(double minlat, double minlon, double maxlat, double maxlon, string filename)
        {
            string address = currentServer + queryMap(coordinatesToString(minlat, minlon, maxlat, maxlon)) + email;
            client.DownloadFile(address, filename);
        }

        //experimental
        /// <summary>
        /// Downloads OSM xml-file containing relations with given value and key
        /// </summary>
        /// <param name="key">Tag key</param>
        /// <param name="value">Key value</param>
        /// <param name="filename">Name for the downloaded file</param>
        public void downloadOSMFile(string key, string value, string filename)
        {
            string address = currentServer + queryKeyValue(key, value) + email;
            client.DownloadFile(address, filename);
        }

        /// <summary>
        /// Transforms bounding box coordinates into string format
        /// </summary>
        /// <param name="minlat">Minimum latitude</param>
        /// <param name="minlon">Minimum longitude</param>
        /// <param name="maxlat">Maximum latitude</param>
        /// <param name="maxlon">Maximum longitude</param>
        /// <returns>Bounding box in string format</returns>
        private string coordinatesToString(double minlat, double minlon, double maxlat, double maxlon)
        {
            return string.Format("{0},{1},{2},{3}", minlat.ToString(culture), minlon.ToString(culture), maxlat.ToString(culture), maxlon.ToString(culture));
        }

        /// <summary>
        /// Forms a request to perform a map query bounded by given coordinates
        /// </summary>
        /// <param name="coordinates">Bounding box of coordinates</param>
        /// <returns>Useable Overpass map query for given coordinates</returns>
        private string queryMap(string coordinates)
        {
            return string.Format("(node({0});<;);out;", coordinates);
        }

        //experimental
        /// <summary>
        /// Forms a request to retrieve all nodes with given key and value
        /// </summary>
        /// <param name="key">Tag key</param>
        /// <param name="value">Key value</param>
        /// <returns>Useable Overpass relations query</returns>
        private string queryKeyValue(string key, string value) 
        {
            return string.Format("relation[\"{0}\"=\"{1}\"];node(r);out;", key, value);
        }

    }
}
