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
            char unit;

            PrintCaption();

            Console.Write("\nFirst city: ");
            Query1 = Console.ReadLine();
            Console.Write("Second city: ");
            Query2 = Console.ReadLine();
            Console.Write("Select unit [km]/[mil]: ");
            SelectUnit(Console.ReadLine(), out unit);

            SearchCityCoords(ref city1, Query1);
            SearchCityCoords(ref city2, Query2);
            Console.Clear();

            Console.WriteLine("City 1: " + city1.Name + "   [" + city1.Lat + " " + city1.Lon + "]");
            Console.WriteLine("City 2: " + city2.Name + "   [" + city2.Lat + " " + city2.Lon + "]");
            Console.WriteLine("\nDistance: " + CalculateDistance(city1, city2, unit) + " " + GetUnit(unit));

            Console.ReadKey();
        }

        private static string GetUnit(char unit)
        {
            switch (unit)
            {
                case 'k':
                    return "km";
                case 'm':
                    return "mil";
                default:
                    return "km";
            }
        }

        private static void SelectUnit(string v, out char unit)
        {
            switch (v)
            {
                case "km":
                    {
                        unit = 'k';
                    }
                    break;
                case "mil":
                    {
                        unit = 'm';
                    }
                    break;
                default:
                    unit = 'k';
                    break;
            }
        }

        private static double CalculateDistance(City city1, City city2, char unit)
        {
            const double R = 6371;
            double lat1 = city1.Lat * (Math.PI / 180);
            double lat2 = city2.Lat * (Math.PI / 180);
            double lon1 = city1.Lon * (Math.PI / 180);
            double lon2 = city2.Lon * (Math.PI / 180);
            double Dlat = (lat2 - lat1);
            double Dlon = (lon2 - lon1);
            double a = Math.Sin(Dlat / 2) * Math.Sin(Dlat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(Dlon / 2) * Math.Sin(Dlon / 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            double d = R * c;

            if(unit == 'm')
                d = d * 0.621371192;
                 
            return Math.Round(d, 3);
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

            try
            {
                city.Lat = double.Parse(latNode.InnerText);
            }

            catch (Exception e)
            {
                if (e is FormatException)
                {
                    string str = latNode.InnerText;
                    str = str.Replace('.', ',');
                    city.Lat = double.Parse(str);
                }
            }

            try
            {
                city.Lon = double.Parse(lonNode.InnerText);
            }
            catch (Exception e)
            {
                if (e is FormatException)
                {
                    string str = lonNode.InnerText;
                    str = str.Replace('.', ',');
                    city.Lon = double.Parse(str);
                }
            }
        }

        private static void PrintCaption()
        {
            Console.WriteLine("DistanceCalculator v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("=========================");
        }
    }
}
