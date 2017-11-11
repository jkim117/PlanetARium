using System;
using System.Drawing;

public class StarObject
{
    private int id; // id number
    private double ra; // right ascension coordinate
    private double dec; // declination coordinate
    private double mag; // apparent magnitude 
    private string spectra; // Stellar classification
    
    // constructor of StarObject
    public StarObject(int id1, double ra1, double dec1, double mag1, string spectra1)
    {
        id = id1;
        ra = ra1;
        dec = dec1;
        mag = mag1;
        spectra = spectra1;
    }

    // This commented out method is not used. I have not deleted it yet just in case we need to use its code later
    // method calculates alt and az initially
    /*public double[] azAlt(double longitude, int hour, int minutes, int seconds, int z, int day, int month, int year)
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
    }*/

    // Based on the first letter of the stellar classification, this method returns a color for the star
    public Color getColor(string spectra)
    {
        Color color;
    char c = spectra.ToCharArray()[0];
        if (c == 'O')
        {
            return color.setColor(48, 115, 221);

        }
        else if (c == 'B')
        {
            return color.setColor(88, 139, 221);
        }
        else if (c == 'A')
        {
            return color.setColor(169, 192, 229);
        }
        else if (c == 'F')
        {
            return color.setColor(218, 227, 242);
        }
        else if (c == 'G')
        {
            return color.setColor(234, 170, 255);
        }
        else if (c == 'K')
        {
            return color.setColor(232, 123, 64);
        }
        else if (c == 'M')
        {
            return color.setColor(232, 19, 11);
        }
    }

    // Based on the star's magnitude, the star returns a radius for the star in the sky.
    // For now I've set n = 1, but it should be eventually set to the smallest radius
    public double starRad()
    {
        double n = 1; // smallest radius
        return n * (1 - (mag + 5) / 25);
    }

    // Given the sidereal time (calculated in the main class, Planetarium) and the latitude of the observor, this method calculates the xyz position
    // of the star and returns it.
    public double[] cart(double sid, double latitude)
    {
        // theta, sidereal time itself is constant for all stars
        // keep in mind that this sid differs star to star, so it must have more similarity to hour angle
        // so actually, each sid second is a real second, so increment initial sidereal time by 1 for each second
        double hourAngle = sid - ra;
        double a2 = cos(90 - latitude);
        double a3 = sin(90 - latitude);

        double c2 = cos(latitude);
        double c3 = sin(latitude);

        double rho = 1.0;
        double r = rho * sin(toRadians(90 - dec));
        double x = r * sin(hourAngle);
        double y = c2 + r * cos(hourAngle) * a2;
        double z = c3 + r * cos(hourAngle) * a3;

        double[] xyz = { x, y, z };

        return xyz;
    }

    // Main method
    public static void main(string[] args)
    { 
        print("Hello");
    }

}
