# ![Logo](https://raw.githubusercontent.com/RobThree/NISOCountries/master/NISOCountries.Core/Resources/ISO64.png) NISOCountries
Always up-to-date (.Net) ISO Country list

On many occasions I needed a list of [ISO 3166-1 countrycodes](https://en.wikipedia.org/wiki/ISO_3166-1) (alpha2, alpha3 and/or numeric). And on many occasions I resorted to some 'hardcoded' list of values (usually included in a resource file of some sort). Ofcourse this is a maintenance nightmare because whenever this list changes you need to update your resource file (or worse: recompile or update your database or...). 

This project provides a means of keeping your data up-to-date by retrieving the latest data from the web or any other source provided. [NISOCounties.Core](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Core) provides a set of interfaces, basesclasses etc. that help you implement your own data provider tailored to your needs. It also provides a class to easily look up these ISO 3166-1 codes.

Currently NISOCountries provides 4 packages that retrieve data from commonly used sources for ISO 3166-1 data:

1. [NISOCountries.GeoNames](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.GeoNames) retrieves data from [geonames.org](http://geonames.org) (specifically: [countryInfo.txt](http://download.geonames.org/export/dump/countryInfo.txt))
2. [NISOCountries.Ripe](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Ripe) retrieves data from [ripe.net](https://www.ripe.net/) (specifically: [iso3166-countrycodes.txt](ftp://ftp.ripe.net/iso3166-countrycodes.txt))
3. [NISOCountries.Wikipedia.CSQ](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Wikipedia.CSQ) retrieves data from [wikipedia](http://wikipedia.org/) (specifically [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1), using [CsQuery](https://github.com/jamietre/CsQuery))
4. [NISOCountries.Wikipedia.HAP](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Wikipedia.HAP) retrieves data from [wikipedia](http://wikipedia.org/) (specifically [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1), using [Html Agility Pack](http://htmlagilitypack.codeplex.com/))

Everything is available as [NuGet package](https://www.nuget.org/packages?q=nisocountries).

Implementing your very own dataprovider is simple so feel free to add to the above list your very own!

**NOTE:** None of the above sources are 'official' and all of the above contain, sometimes subtly, different data. Which data you want to rely on is up to you.

## Basic Usage

Usage is pretty straightforward. NISOCountries provides some convenience classes, methods etc. to hide most of the details you'll probably never use but more specific overloads, constructors etc. are available to tailor everything to your needs. The most basic 'hello world' example goes a little something like this:

````c#
    //We choose, for this example, to use Wikipedia as source (using CsQuery)
    using NISOCountries.Wikipedia.CSQ;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            //Retrieve 3166-1 data from Wikipedia
            var countries = new WikipediaISOCountryReader().GetDefault();

            //Voila!
            Console.WriteLine(countries.Where(c => c.Alpha2 == "NL").First().CountryName);
        }
    }
````
Output:
````
NETHERLANDS
````

...more documentation to come soon...

[![Build status](https://ci.appveyor.com/api/projects/status/bnrsj4uyo3hggh9m)](https://ci.appveyor.com/project/RobIII/nisocountries) <a href="https://www.nuget.org/packages/NISOCountries.Core/"><img src="http://img.shields.io/nuget/v/NISOCountries.Core.svg?style=flat-square" alt="NuGet version" height="18"></a> <a href="https://www.nuget.org/packages/NISOCountries.Core/"><img src="http://img.shields.io/nuget/dt/NISOCountries.Core.svg?style=flat-square" alt="NuGet version" height="18"></a>
