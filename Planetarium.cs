using System;

public class Planetarium
{
    private string[] stars = new string[6001]; // testing first 1000 stars
    // This file should be able to read the txt star data file
    // https://raw.githubusercontent.com/CelestiaProject/Celestia/VS2013-x86-x64/catalogs/stars.txt
    // And create an array of starObjects (maybe first couple thousand brightest)
    // Remember for each line in the txt file, the first number is the id, the third is the declination, the 4th is right ascension,
    // 5th is magnitude, and 6th is the stellar classification
    static void Main()
	
        //Find a way to get these values, from user input/ timestap of the computer. z refers to the positive or negative shift from UT time (time zone) make sure to
        // take daylights savings into account.
        double longitude;
        int hour;
        int minutes;
        int seconds;
        int z;
        int day;
        int month;
        int year;
        // This calculates local sidereal hours (to be inputted into StarObject.cart())
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

//based on the sideral hours (this is re-calcualted for every time step), update the positions of the starObjects using the .cart() method. Make the ar environment reflect this change.
   
}
