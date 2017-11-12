using System;
//using System.Drawing;
using UnityEngine;

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

    private double toRadians(double deg)
    {
        return Math.PI / 180 * deg;
    }

    // Based on the first letter of the stellar classification, this method returns a color for the star
    public Color getColor(string spectra)
    {
        Color color;
    char c = spectra.ToCharArray()[0];
        if (c == 'O')
        {
            color = new Color(48, 115, 221, 1);
            return color;

        }
        else if (c == 'B')
        {
            color = new Color(88, 139, 221, 1);
            return color;
        }
        else if (c == 'A')
        {
            color = new Color(169, 192, 229, 1);
            return color;
        }
        else if (c == 'F')
        {
            color = new Color(218, 227, 242, 1);
            return color;
        }
        else if (c == 'G')
        {
            color = new Color(234, 170, 255, 1);
            return color;
        }
        else if (c == 'K')
        {
            color = new Color(232, 123, 64, 1);
            return color;
        }
        else if (c == 'M')
        {
            color = new Color(232, 19, 11, 1);
            return color;
        }
        else
        {
            color = new Color(234, 170, 255, 1);
            return color;
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
    public Vector3 cart(double sid, double latitude)
    {
        // theta, sidereal time itself is constant for all stars
        // keep in mind that this oldSid (or hourAngle now) differs star to star, so it must have more similarity to hour angle
        // so actually, each sid second is a real second, so increment initial sidereal time by 1 for each second
        double hourAngle = toRadians(sid - ra);
        double a2 = Math.Cos(toRadians(90 - latitude));
        double a3 = Math.Sin(toRadians(90 - latitude));

        double c2 = Math.Cos(toRadians(latitude));
        double c3 = Math.Sin(toRadians(latitude));

        double rho = 50.0;
        double r = rho * Math.Sin(toRadians(90 - dec));
        double x = r * Math.Sin(hourAngle);
        double y = c2 + r * Math.Cos(hourAngle) * a2;
        double z = c3 + r * Math.Cos(hourAngle) * a3;

        Vector3 v = new Vector3((float)x, (float)y, (float)z);

        return v;
    }

    // Main method
    public static void main(string[] args)
    { 
        Console.WriteLine("HelloWorld");
    }

}
