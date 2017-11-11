using System;

public class StarObject
{
    private int id;
    private double ra;
    private double dec;
    private double mag;
    private string spectra;

    private double inAlt;

    // constructor of StarObject
    public StarObject(int id1, double ra1, double dec1, double mag1, string spectra1)
    {
        id = id1;
        ra = ra1;
        dec = dec1;
        mag = mag1;
        spectra = spectra1;
    }

    // method calculates alt and az initially
    public double[] azAlt(double longitude, int hour, int minutes, int seconds, int z, int day, int month, int year)
    {
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

        // This calculates initial alt and az of the star
        double hourAngle = localSidHours - ra;
        double h = toDegrees(asin(sin(toRadians(dec)) * sin(toRadians(longitude)) + cos(toRadians(dec)) * cos(toRadians(longitude)) * cos(toRadians(hourAngle))));
        double az = Math.pi + atan(sin(toRadians(hourAngle)) / (sin(toRadians(longitude)) * cos(toRadians(hourAngle)) - tan(toRadians(dec)) * cos(toRadians(longitude))));

        double[] hAz = { az, h };
        return hAz;
    }

    public static void main(string[] args)
    {
        print("Hello");
    }

}
