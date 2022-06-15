
namespace GaiaShare.Helpers
{
    public static class GeographicHelper
    {
        // TODO: add parameter to limit decimal places.
        public static double convertMetersToFeet(double meters)
        {
            return meters * 3.28084;
        }

        // TODO: add parameter to limit decimal places.
        public static double convertMetersToMiles(double meters)
        {
            return meters * 0.000621371;
        }

        /// <summary>
        /// Returns a HREF link
        /// </summary>
        public static string getMapLink(double lat, double lng, string name)
        {
            return $"<a target=\"_new\" href=\"https://www.google.com/maps/@{lat},{lng},15z\">{name}</a>";
        }

        /// <summary>
        /// From Jerry (VB code)
        /// Returns MILES
        /// </summary>
        public static double dist_haversine(double lat1, double lon1, double lat2, double lon2)
        {
            // where  φ is latitude, λ is longitude, R
            var R = 6371e3; // metres
            var φ1 = toRadians(lat1); //.toRadians();
            var φ2 = toRadians(lat2); //.toRadians();
            var Δφ = toRadians(lat2 - lat1); //.toRadians();
            var Δλ = toRadians(lon2 - lon1); //.toRadians();

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                Math.Cos(φ1) * Math.Cos(φ2) *
                Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = R * c; // metres
            var miles = d / 1609.34; // per mile
            //if (miles > 100)
            //    return parseInt(miles);
            return miles;
        }

        // Rhumb line navigation - Course between two points with same bearing the whole way.
        // http://www.movable-type.co.uk/scripts/latlong.html
        public static double bearing_haversine(double lat1, double lon1, double lat2, double lon2)
        {
            // where  φ is latitude, λ is longitude, R
            var R = 6371e3; // metres
            var φ1 = toRadians(lat1); //.toRadians();
            var φ2 = toRadians(lat2); //.toRadians();
            var Δφ = toRadians(lat2 - lat1); //.toRadians();
            var Δλ = toRadians(lon2 - lon1); //.toRadians();

            // lambda 2 destination coord (long)
            // phi latitued
            var y = Math.Sin(Δλ) * Math.Cos(φ2);
            var x = Math.Cos(φ1) * Math.Sin(φ2) -
                    Math.Sin(φ1) * Math.Cos(φ2) * Math.Cos(Δλ);
            var θ = Math.Atan2(y, x);
            var brng = (θ * 180 / Math.PI + 360) % 360; // degrees
            return brng;
        }

        public static string DegreesToCardinal(double degrees)
        {
            string[] caridnals = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };
            return caridnals[(int)Math.Round(((double)degrees % 360) / 45)];
        }

        public static string DegreesToCardinalDetailed(double degrees)
        {
            string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            return caridnals[(int)Math.Round(((double)degrees * 10 % 3600) / 225)];
        }

        public static double toRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }


    }
}
