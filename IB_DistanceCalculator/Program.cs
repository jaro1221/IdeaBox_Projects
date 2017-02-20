using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IB_DistanceCalculator
{
    class Program
    {
        struct City
        {
            public string Name { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
        };

        static void Main(string[] args)
        {
            City city1 = new City();
            City city2 = new City();

            string Query1;
            string Query2;

            PrintCaption();

            Console.Write("\nFirst city: ");
            Query1 = Console.ReadLine();
            Console.Write("Second city: ");
            Query2 = Console.ReadLine();

            SearchCityCoords(ref city1, Query1);
            SearchCityCoords(ref city2, Query2);
            Console.Clear();

            Console.WriteLine("City 1: " + city1.Name + "   [" + city1.Lat + " " + city1.Lon + "]");
            Console.WriteLine("City 2: " + city2.Name + "   [" + city2.Lat + " " + city2.Lon + "]");
            Console.WriteLine("\nDistance: " + CalculateDistance(city1, city2));

            Console.ReadKey();
    }

        private static double CalculateDistance(City city1, City city2)
        {
            const double R = 6371000;
            double lat1 = city1.Lat * (Math.PI / 180);
            double lat2 = city2.Lat * (Math.PI / 180);
            double lon1 = city1.Lon * (Math.PI / 180);
            double lon2 = city2.Lon * (Math.PI / 180);
            double Dlat = (lat2 - lat1);
            double Dlon = (lon2-lon1);
            double a = Math.Sin(Dlat / 2) * Math.Sin(Dlat / 2) * Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(Dlon / 2) * Math.Sin(Dlon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;

        }

        private static void SearchCityCoords(ref City city, string query)
        {
            WebClient webClient = new WebClient();
            string xmlString = webClient.DownloadString("https://maps.googleapis.com/maps/api/geocode/xml?address=" + query);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlString);

            XmlNode nameNode = xml.DocumentElement.SelectSingleNode("..//result//formatted_address");
            XmlNode latNode = xml.DocumentElement.SelectSingleNode("..//result//geometry//location//lat");
            XmlNode lonNode = xml.DocumentElement.SelectSingleNode("..//result//geometry//location//lng");

            city.Name = nameNode.InnerText;
            
            string lat = latNode.InnerText;
            string[] latVs = lat.Split('.');
            double latDec = double.Parse(latVs[1]);
            city.Lat = double.Parse(latVs[0]) + latDec * Math.Pow(0.1, Math.Ceiling(Math.Log10(latDec)));
            string lon = lonNode.InnerText;
            string[] lonVs = lon.Split('.');
            double lonDec = double.Parse(lonVs[1]);
            city.Lon = double.Parse(lonVs[0]) + lonDec * Math.Pow(0.1, Math.Ceiling(Math.Log10(lonDec)));
            Console.WriteLine(Math.Pow(0.1, Math.Ceiling(Math.Log10(latDec))));
            Console.ReadKey();
            //city.Lat = double.Parse(lat);
            //Console.WriteLine(lat.Normalize());
            //Console.WriteLine(lon);
            //Console.WriteLine(latNode.InnerText.Length);
        }

        private static void PrintCaption()
        {
            Console.WriteLine("DistanceCalculator v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("=========================");

        }
    }
}
