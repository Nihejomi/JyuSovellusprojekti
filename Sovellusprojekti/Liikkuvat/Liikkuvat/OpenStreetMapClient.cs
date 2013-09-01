using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;
using System.Drawing;
using System.Windows;

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
            
            //client.DownloadFile(address, filename);<-- ei toiminut, kokeilen valmista temppiä.  Henrik
        }

        /// <summary>
        /// Downloads OSM xml-file containing node at the center of given city
        /// </summary>
        /// <param name="cityName">Name of the city</param>
        /// <param name="filename">Name for the downloaded file</param>
        public void downloadOSMFile(string cityName, string filename)
        {
            string address = currentServer + queryCityNode(cityName) + email;
            client.DownloadFile(address, filename);
        }


        public double[] calculateBBox(double lat, double lon, double km)
        {
            double radiusEarth = 6371; //radius of earth in meters

            double right = degreesToRadians(90.0);
            double left = degreesToRadians(270.0);
            double up = degreesToRadians(0.0);
            double down = degreesToRadians(180.0);

            double latRadian = degreesToRadians(lat);
            double lonRadian = degreesToRadians(lon);

            //φ2 = asin( sin(φ1)*cos(d/R) + cos(φ1)*sin(d/R)*cos(θ) )
            //λ2 = λ1 + atan2( sin(θ)*sin(d/R)*cos(φ1), cos(d/R)−sin(φ1)*sin(φ2) )

            double minlat = radiansToDegrees(Math.Asin( Math.Sin(latRadian) * Math.Cos(km/radiusEarth) + 
                Math.Cos(latRadian) * Math.Sin(km/radiusEarth) * Math.Cos(down)));

            double minlon = radiansToDegrees(lonRadian + Math.Atan2(Math.Sin(left) * Math.Sin(km / radiusEarth) * Math.Cos(latRadian),
                                 Math.Cos(km / radiusEarth) - Math.Sin(latRadian) * Math.Sin(minlat)));
            double maxlat = radiansToDegrees(Math.Asin(Math.Sin(latRadian) * Math.Cos(km / radiusEarth) + 
                Math.Cos(latRadian) * Math.Sin(km / radiusEarth) * Math.Cos(up)));
            double maxlon = radiansToDegrees(lonRadian + Math.Atan2(Math.Sin(right) * Math.Sin(km / radiusEarth) * Math.Cos(latRadian),
                                 Math.Cos(km / radiusEarth) - Math.Sin(latRadian) * Math.Sin(maxlat)));
            
            return new double[4] {minlat, minlon, maxlat, maxlon};

        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="degree">Degrees</param>
        /// <returns>Radians</returns>
        private double degreesToRadians(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="radians">Radians</param>
        /// <returns>Degrees</returns>
        private double radiansToDegrees(double radians)
        {
            return radians * 180.0 / Math.PI;
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
            return string.Format("{0:0.000000},{1:0.000000},{2:0.000000},{3:0.000000}",
                minlat, minlon, maxlat, maxlon);
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
        /// Forms a request to perform a map query to retrieve node of given city
        /// </summary>
        /// <param name="cityName">Name of the city</param>
        /// <returns></returns>
        private string queryCityNode(string cityName)
        {         
            return string.Format("(node[\"name\"=\"{0}\"][\"place\"=\"city\"];);out;", cityName);
        }
    }
}
