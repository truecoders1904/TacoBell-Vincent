using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if(lines.Length == 0)
            {
                logger.LogError("No lines found in CSV file");
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning("Only 1 lines found in CSV file");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable location1 = null;
            ITrackable location2 = null;
            double maxDistance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                ITrackable locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;
                for (int j = 0; j < locations.Length; j++)
                {
                    ITrackable locB = locations[j];
                    GeoCoordinate corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    double distanceTo = corA.GetDistanceTo(corB);
                    if (distanceTo > maxDistance)
                    {
                        maxDistance = distanceTo;
                        location1 = locA;
                        location2 = locB;
                    }
                }
            }
            Console.WriteLine($"The two taco bells that are the furthest from each other are : {location1.Name} and {location2.Name}");
        }
    }
}