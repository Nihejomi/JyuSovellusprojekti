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
    /// Currently can be used to retrieve nodes, relations and ways from given bounding box
    /// or around the center (defined by users of OpenStreetMap) of given city
    /// TODO: any sort of exception handling, more functionality
    /// </summary>
    /// <remarks> created by Artur Kreisberg, v. 0.3 </remarks>
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
        /// Downloads OSM xml-file containing nodes, relations and ways inside given coordinates
        /// </summary>
        /// <param name="minlat">Minimum latitude</param>
        /// <param name="minlon">Minimum longitude</param>
        /// <param name="maxlat">Maximum latitude</param>
        /// <param name="maxlon">Maximum longitude</param>
        /// <param name="filename">Name for the downloaded file</param>
        public void downloadOSMFile(double minlat, double minlon, double maxlat, double maxlon, string filename)
        {
            string address = currentServer + queryMapBBox(coordinatesToString(minlat, minlon, maxlat, maxlon)) + email;
            client.DownloadFile(address, filename);
        }

        /// <summary>
        /// Downloads OSM xml-file containing nodes, relations and ways withing given distance from center of given city
        /// </summary>
        /// <param name="cityName">Name of the city, task is performed from the center of the city</param>
        /// <param name="rangeMeters">Distance from center in meters</param>
        /// <param name="filename">Name for the downloaded file</param>
        public void downloadOSMFile(string cityName, int rangeMeters, string filename)
        {
            string address = currentServer + queryMapAround(cityName, rangeMeters) + email;
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
        private string queryMapBBox(string coordinates)
        {
            return string.Format("(node({0});<;>;);out;", coordinates);
            //return string.Format("(node({0});<;);out;", coordinates); //use this for testing, it's faster given large query, put omits parts of buildings
        }

        /// <summary>
        /// Forms a request to perform a map query relations, nodes and ways around given point
        /// </summary>
        /// <param name="rangeMeters">Distance from node in meters, query tries to capture all nodes, relations and ways within</param>
        /// <param name="cityName">Name of the city, query is performed from a center node found in OSM-database</param>
        /// <returns></returns>
        private string queryMapAround(string cityName, int rangeMeters)
        {         
            return string.Format("(node[\"name\"=\"{0}\"][\"place\"=\"city\"];node(around:{1});<;>;);out;", cityName, rangeMeters);
        }
    }
}
