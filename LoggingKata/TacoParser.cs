namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {

            if (line == null)
            {
                return null;
            }

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Not enough info on Line.");
                return null;
            }

            double lat;
            double lon;
            try
            {
                lat = double.Parse(cells[0]);
                lon = double.Parse(cells[1]);
            }
            catch
            {
                return null;
            }
            if (lat > 90 || lat < -90)
            {
                return null;
            }
            if (lon > 180 || lon < -180)
            {
                return null;
            }
            TacoBell tacobell = new TacoBell();
            tacobell.Name = cells[2];

            Point point = new Point();
            point.Latitude = lat;
            point.Longitude = lon;

            tacobell.Location = point;

            return tacobell;
        }
    }
}