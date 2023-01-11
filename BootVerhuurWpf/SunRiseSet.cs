using System;


namespace BootVerhuurWpf
{
    public class Sun
    {
        private double longitude = 5.489710; // Lengtegraad in decimale graden, <0 voor OL, >0 voor WL
        double latitude = 52.225370; // Breedtegraad in decimale graden, >0 voor NB, <0 voor ZB

        int year = ToDay.Year;
        int month = ToDay.Month +1; 
        int date = ToDay.Day;
        int hours = ToDay.Hour; 
        int minutes = ToDay.Minute;

        static DateTime ToDay = DateTime.Now;

        static TimeZoneInfo UtcTimeZone = TimeZoneInfo.Utc;
        static TimeZoneInfo LocalTimeZone = TimeZoneInfo.Local;

        static TimeSpan difference = LocalTimeZone.GetUtcOffset(ToDay) - UtcTimeZone.GetUtcOffset(ToDay);
        
        int offset = difference.Hours;

        public static string sun_set;
        public static string sun_rise;



        /* ------------------------- Hier onder niet wijzigen ------------------------------------------- */
        public int DayInMonth(int month, int year)
        {
            int n = 31;
            month -= 1;
            if ((month == 0) || (month == 2) || (month == 4) || (month == 6) || (month == 7) || (month == 9) ||
                (month == 11))
            {
                n = 31;
            }

            if ((month == 3) || (month == 5) || (month == 8) || (month == 10))
            {
                n = 30;
            }

            if (month == 1)
            {
                n = 28;
                if ((year % 4) == 0) n = 29;
                if ((year % 100) == 0) n = 28;
                if ((year % 400) == 0) n = 29;
            }

            return n;
        }

        public double Frac(double X)
        {
            X = X - Math.Floor(X);
            if (X < 0) X = X + 1.0;
            return X;
        }

        public string HoursMinutes(double time)
        {
            var _hrs = Math.Floor(time);
            var _min = Math.Round(60.0 * Frac(time));
            if (_min == 60)
            {
                // Voorkom dat er 16:60 verschijnt ipv 17:00
                _min = 0;
                _hrs += 1;
                if (_hrs > 24)
                    _hrs = 0; // Strict formeel; Zon gaat beneden de poolcirkel niet op/onder rond middernacht...
            }

            var str = _hrs + ":";
            if (_min >= 10) str = str + _min;
            else str = str + "0" + _min;
            if (str.LastIndexOf('N') > 0) str = "--:--";
            return str;
        }

        public Sun()
        {
            if (year < 1900) year += 1900;

            if (offset >= 1320) offset = (offset - 1440) / -60;
            else offset = offset / -60;

            double doy = 0;

            for (var i = 1; i < month; i++) doy = doy + DayInMonth(i, year);
            doy = doy + date;
            double x = hours + offset + minutes / 60;
            doy = doy - 1 + (x - 12) / 24;

            x = doy * 2 * Math.PI / 365; // fractional year in radians
            double eqtime = 229.18 * (0.000075 + 0.001868 * Math.Cos(x) - 0.032077 * Math.Sin(x) - 0.014615 * Math.Cos(2 * x) - 0.040849 * Math.Sin(2 * x));

            // declination (in degrees)
            double declin = 0.006918 - 0.399912 * Math.Cos(x) + 0.070257 * Math.Sin(x) - 0.006758 * Math.Cos(2 * x);
            declin = declin + 0.000907 * Math.Sin(2 * x) - 0.002697 * Math.Cos(3 * x) + 0.00148 * Math.Sin(3 * x);
            declin = declin * 180 / Math.PI;

            // solar azimuth angle for sunrise and sunset corrected for atmospheric refraction (in degrees),
            x = Math.PI / 180;
            double hars = Math.Cos(x * 90.833) / (Math.Cos(x * latitude) * Math.Cos(x * declin));
            hars = hars - Math.Tan(x * latitude) * Math.Tan(x * declin);
            hars = Math.Acos(hars) / x;

            // sunrise and sunset (local time)
            x = 720 + 4 * (longitude - hars) - eqtime;
            x = x / 60 + offset;
            sun_rise = HoursMinutes(x);
            Console.WriteLine($"Sun rise {sun_rise}");

            x = 720 + 4 * (longitude + hars) - eqtime;
            x = x / 60 + offset;
            sun_set = HoursMinutes(x);
            Console.WriteLine($"Sun set {sun_set}");
        }
    }
}
