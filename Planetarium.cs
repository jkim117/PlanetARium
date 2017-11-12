using System;
//using System.DateTime;
using UnityEngine;
//using UnityEngine.Gizmos;
//using System.collections;


public class Planetarium : MonoBehaviour
{
    // State variables for this class
    private int n = 1000; // For testing purposes, we will just use the first 1000 stars from stars.txt
    /*private int[] stars = new int[n]; // id number of the stars
    private double[] mags = new double[n]; // apparent magnitudes of the stars
    private double[] RAs = new double[n]; // right ascension of stars
    private double[] decs = new double[n]; // declination of stars
    private string[] spectralType = new string[n]; // spectarl classification of stars
    StarObject[] starObjects = new StarObject[n]; // array of starObjects
    GameObject[] starsShown = new GameObject[n]; // array of GameObjects corresponding to the stars*/
    private int[] stars;// id number of the stars
    private double[] mags; // apparent magnitudes of the stars
    private double[] RAs; // right ascension of stars
    private double[] decs; // declination of stars
    private string[] spectralType; // spectarl classification of stars
    StarObject[] starObjects; // array of starObjects
    GameObject[] starsShown; // array of GameObjects corresponding to the stars

    private double longitude = -74.655635; // hardcoded for now longitude of Princeton
    private double latitude = 40.3573; // latitude of Princeton
    private int starCounter = 0; // keeps count of stars as their positions are updated

    // getStars reads the txt file of data and initializes the star data arrays
    private void getStars()
    {
        stars = new int[n];
        mags = new double[n];
        RAs = new double[n];
        decs = new double[n];
        spectralType = new string[n];
        starObjects = new StarObject[n];
        starsShown = new GameObject[n];

        string[] lines = System.IO.File.ReadAllLines(@"stars.txt");

        for (int i = 0; i < this.stars.Length; i++)
        {
            string[] line = lines[i].Split(' ');

            this.stars[i] = Int32.Parse(line[0]);
            this.decs[i] = Convert.ToDouble(line[2]);
            this.RAs[i] = Convert.ToDouble(line[3]);
            this.mags[i] = Convert.ToDouble(line[4]);
            this.spectralType[i] = line[5];
        }
    }

    // This method is called at the beginning of the program
    void start()
    {
        // creates stars as well as gameobjects corresponding to those stars
        getStars();
        for (int i = 0; i < n; i++)
        {
            starObjects[i] = new StarObject(stars[i], RAs[i], decs[i], mags[i], spectralType[i]);
        
            GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            s.transform.localScale = new Vector3((float)starObjects[i].starRad(), (float)starObjects[i].starRad(), (float)starObjects[i].starRad());
            s.transform.position = starObjects[i].cart(sidCalc(), latitude);

            var meshFilter = s.AddComponent<MeshFilter>();
            s.AddComponent<MeshRenderer>();
            meshFilter.sharedMesh = Sphere;

            starsShown[i] = s;
        }
    }
    
    // this method is called each frame
    void update()
    {
        // To make sure that the cpu is not overloaded with updating the positions of all the stars each frame
        // the program only updates the position of 10 stars each frame. starCounter ensures that each frame, the program updates the position
        // of the next 10 stars. When starCounter reaches n, it cycles back to updating the stars again
        if (starCounter >= n)
            starCounter = 0;

        for (int i = starCounter; i < starCounter + 10; i++)
        {
            // GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            starsShown[i].transform.position = starObjects[i].cart(sidCalc(), latitude);
           //  Gizmos.DrawSphere(stars[i].cart(locSideHours, latitude), stars[i].starRad());// Draws 10 stars
        }
        starCounter += 10;
    }

    // this method calcualtes local sidereal time for the current time.
    private double sidCalc()
    {
        var time = DateTime.Now;

        int hour = time.Hour;
        int minutes = time.Minute;
        int seconds = time.Second;
        int z = -5; // This is the UT shift: for now this has been hard coded for eastern time, no daylights savings
        int day = time.Day;
        int month = time.Month;
        int year = time.Year;

        // This calculates local sidereal hours (to be inputted into StarObject.cart())
        double decHour = hour + minutes / 60.0 + seconds / 3600.0;
        double jDay = 367 * year - (int)(7 * (year + (int)((month + 9) / 12)) / 4) + (int)(275 * month / 9) + day + 1721013.5;
        double jCenturies = (jDay - 2451545.0) / 36525;
        double gmstMean = 100.46061837 + 36000.77053608 * jCenturies + 3.87933 * (Math.Pow(10, -4)) * (Math.Pow(jCenturies, 2)) - (Math.Pow(jCenturies, 3)) / (3.871 * (Math.Pow(10, 7)));
        double ut = decHour + z;
        double gmstActual = gmstMean + 360.985647366 * (ut / 24);

        return (gmstActual + longitude) % 360;
    }

}