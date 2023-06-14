using System;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace XML_Test
{
    class Program
    {
        static void Main(string[] args) {
            Stateid();
            Console.WriteLine("\n");
            
            LastNamesFromFile();
            Console.WriteLine("\n");
            
            LastNamesFromLink();

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }

        public static void Stateid() {
            string filename = "timeseries.xml";
            string currentDirectory = Directory.GetCurrentDirectory();
            string TimeseriesFilepath = Path.Combine(currentDirectory, filename);
            XElement timeseries = XElement.Load(TimeseriesFilepath);

            IEnumerable<string> stateid = timeseries.Descendants("timeseries").Select(x => (string)x.Attribute("stateid"));
            Console.WriteLine(stateid.FirstOrDefault());
        }

        public static void LastNamesFromFile() {
            string filename = "races.xml";
            string currentDirectory = Directory.GetCurrentDirectory();
            string racesFilepath = Path.Combine(currentDirectory, filename);
            XElement races = XElement.Load(racesFilepath);

            List<string> results = races.Descendants("lastname")
                .Select(lName => (string)lName.Value)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            Console.WriteLine("File: ");
            foreach (var result in results)
                Console.WriteLine("Lastname: " + result);
        }

        public static void LastNamesFromLink() {
            XmlDocument doc = new XmlDocument();
            doc.Load("https://wwwlab.webug.se/examples/XML/electionservice/races/?mode=xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("lastname");
            List<string> nodesList = nodes.Cast<XmlNode>()
                .Select(node => node.InnerText)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            Console.WriteLine("Link: ");
            foreach (var result in nodesList)
                Console.WriteLine("Lastname: " + result);
        }
    }
}
