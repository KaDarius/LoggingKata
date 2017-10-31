using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Geolocation;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Logger.Info("Log initialized");

            var csvPath = Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv";
            Logger.Debug("Created csvPath variable: " + csvPath);

            var rows = File.ReadAllLines(csvPath);

            if (rows.Length == 0)
            {
                Logger.Error("No locations to check. Must have at least one locations.");
            }
            else if (rows.Length == 1)
            {
                Logger.Warn("Only one location provided. Must have two to perform a check.");
            }

            var parser = new TacoParser();


            var locations = rows.Select(row => parser.Parse(row))
                .OrderBy(loc => loc.Location.Longitude)
                .ThenBy(loc => loc.Location.Latitude)
                .ToArray();

            ITrackable a = null;
            ITrackable b = null;
            double distance = 0;

            //TODO:  Find the two TacoBells in Alabama that are the furthurest from one another.

            foreach (var locA in locations)
            {
                var origin = new Coordinate
                {
                    Latitude = locA.Location.Latitude,
                    Longitude = locA.Location.Longitude
                };

                foreach (var locB in locations)
                {
                    var dest = new Coordinate
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Longitude
                    };

                    var nDist = GeoCalculator.GetDistance(origin, dest);

                    if (nDist > distance)
                    {
                        Logger.Info("Found the next furthest apart");
                        a = locA;
                        b = locB;
                        distance = nDist;
                    }

                }

            }

            Console.WriteLine($"The two Taco Bells that are the furthest apart are: {a.Name} and {b.Name}");
            Console.WriteLine($"These two locations are {distance} miles apart.");
            Console.ReadLine();
        }
    }
}