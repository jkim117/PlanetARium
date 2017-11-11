from __future__ import division
import math
#Sidereal Calculator


#times should be of order: Hour, Min, Second, Day, Month, Year, Time zone shift, longitude + is east - is west

def toDecimalHour(hour, minute, seconds):
    return hour+minute/60.0+seconds/3600.0

def julianDay(day, month, year):
    return 367*year-int(7*(year+int((month+9)/12))/4)+int(275*month/9)+day+1721013.5

def julianCenturies(julianDay):
    return (julianDay-2451545.0)/36525

def gmstMean(julianCenturies):
    return 100.46061837+36000.77053608*julianCenturies+3.87933*(10**-4)*(julianCenturies**2)-(julianCenturies**3)/(3.871*(10**7))

def gmstActual(gmstMean,ut):
    return gmstMean+360.985647366*(ut/24)

def locSidRaw(gmstActual,longitude, westOrEast):
    if westOrEast=="east":
        return gmstActual+longitude
    if westOrEast=="west":
        return gmstActual-longitude

def locSidHours(locSidRaw):
    while locSidRaw>360:
        locSidRaw=locSidRaw-360
    return locSidRaw/15

def convertToUt(localDecimal,z):
    return localDecimal+z


def getSid(hour, minutes, seconds, z, day, month, year, longitude):
    decHour=toDecimalHour(hour,minutes,seconds)
    UT=convertToUt(decHour,z)

    jDay=julianDay(day,month,year)

    jCentury=julianCenturies(jDay)
 
    JD=jDay+UT/24
 
    gmSidMean=gmstMean(jCentury)
 
    gmSidActual=gmstActual(gmSidMean,UT)

    westEast = "west"

    if longitude > 0:
        westEast = "east"
    longitude = abs(longitude)
    
    rawLocalSid=locSidRaw(gmSidActual,longitude,westEast)
 
    localSid=locSidHours(rawLocalSid)
    return localSid


        



