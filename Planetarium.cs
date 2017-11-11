using System;

public class Planetarium
{
    private string[] stars = new string[6001]; // testing first 1000 stars
	static void Main()
	{
        //These will be inputs eventually
        double longitude;
        int hour;
        int minutes;
        int seconds;
        int z;
        int day;
        int month;
        int year;
        // This calculates local sidereal hours
        double decHour = hour + minutes / 60.0 + seconds / 3600.0;
        double jDay = 367 * year - (int)(7 * (year + (int)((month + 9) / 12)) / 4) + (int)(275 * month / 9) + day + 1721013.5;
        double jCenturies = (jDay - 2451545.0) / 36525;
        double gmstMean = 100.46061837 + 36000.77053608 * julianCenturies + 3.87933 * (Math.pow(10, -4)) * (Math.pow(julianCenturies, 2)) - (Math.pow(jCenturies, 3)) / (3.871 * (Math.pow(10, 7)));
        double ut = decHour + z;
        double gmstActual = gmstMean + 360.985647366 * (ut / 24);
        double locSidRaw = gmstActual + longitude;

        while (locSidRaw > 360)
        {
            locSidRaw = locSidRaw - 360;
        }
        double localSidHours = locSidRaw;
    }

   
}
