using System;
using DateTime;
using Windows.Devices.Geolocation;

public class Planetarium
{
    private int[] stars = new int[6001]; // testing first 1000 stars
    private double[] mags = new double[6001];
    private double[] RAs = new double[6001];
    private double[] decs = new double[6001];
    private string[] spectralType = new string[6001];
    private static void getStars()
    {
        string[] lines = System.IO.File.ReadAllLines(@"stars.txt");
        for (int i = 0; i < stars.Length; i++)
        {
            string[] line = lines[i].Split(' ');
            stars[i] = Int32.Parse(line[0]);
            decs[i] = Convert.ToDouble(line[2]);
            RAs[i] = Convert.ToDouble(line[3]);
            mags[i] = Convert.ToDouble(line[4]);
            spectralType = line[5];
        }
    }
    
    // This file should be able to read the txt star data file
    // https://raw.githubusercontent.com/CelestiaProject/Celestia/VS2013-x86-x64/catalogs/stars.txt
    // And create an array of starObjects (maybe first couple thousand brightest)
    // Remember for each line in the txt file, the first number is the id, the third is the declination, the 4th is right ascension,
    // 5th is magnitude, and 6th is the stellar classification
    static void Main() {
        //Find a way to get these values, from user input/ timestap of the computer. z refers to the positive or negative shift from UT time (time zone) make sure to
        // take daylights savings into account.

        // location stuff
        var accessStatus = await Geolocator.RequestAccessAsync(); // we need to enable location in Unity project settings
        
        switch (accessStatus)
        {
            case GeolocationAccessStatus.Allowed:
                _rootPage.NotifyUser("Waiting for update...", NotifyType.StatusMessage);

                // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = _desireAccuracyInMetersValue };

                // Subscribe to the StatusChanged event to get updates of location status changes.
                _geolocator.StatusChanged += OnStatusChanged;

                // Carry out the operation.
                Geoposition pos = await geolocator.GetGeopositionAsync();
                Geocoordinate coord = pos.Coordinate; // I really don't know
                Geopoint point = pos.Point; // Seriously no idea

                UpdateLocationData(pos);
                //_rootPage.NotifyUser("Location updated.", NotifyType.StatusMessage);
                break;

            case GeolocationAccessStatus.Denied:
                _rootPage.NotifyUser("Access to location is denied.", NotifyType.ErrorMessage);
                LocationDisabledMessage.Visibility = Visibility.Visible;
                UpdateLocationData(null);
                break;

            case GeolocationAccessStatus.Unspecified:
                _rootPage.NotifyUser("Unspecified error.", NotifyType.ErrorMessage);
                UpdateLocationData(null);
                break;
        }


        var time = DateTime.UtcNow; // if we're using UTC, we don't need z, right?
        double longitude = -74.655635; //hardcoded for now
        int hour = time.Hour;
        int minutes = time.Minute;
        int seconds = time.Second;
        int z = 0; //ya?
        int day = time.Day; // would implementation be easier with time.DayofYear?
        int month = time.Month;
        int year = time.Year;

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
        double localSidHours = locSidRaw; //wasting memory? looks like you just changed the variable name


    }

//based on the sideral hours (this is re-calcualted for every time step), update the positions of the starObjects using the .cart() method. Make the ar environment reflect this change.
   
}
