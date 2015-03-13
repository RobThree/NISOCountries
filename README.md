# ![Logo](https://raw.githubusercontent.com/RobThree/NISOCountries/master/NISOCountries.Core/Resources/ISO64.png) NISOCountries
Always up-to-date (.Net) ISO Country list

On many occasions I needed a list of [ISO 3166-1 countrycodes](https://en.wikipedia.org/wiki/ISO_3166-1) (alpha2, alpha3 and/or numeric). And on many occasions I resorted to some 'hardcoded' list of values (usually included in a resource file of some sort). Ofcourse this is a maintenance nightmare because whenever this list changes you need to update your resource file (or worse: recompile or update your database or...). 

This project provides a means of keeping your data up-to-date by retrieving the latest data from the web or any other source provided. [NISOCounties.Core](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Core) provides a set of interfaces, basesclasses etc. that help you implement your own data provider tailored to your needs. It also provides a class to easily look up these ISO 3166-1 codes.

Currently NISOCountries provides 4 packages that retrieve data from commonly used sources for ISO 3166-1 data:

1. [NISOCountries.GeoNames](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.GeoNames) retrieves data from [geonames.org](http://geonames.org) (specifically: [countryInfo.txt](http://download.geonames.org/export/dump/countryInfo.txt))
2. [NISOCountries.Ripe](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Ripe) retrieves data from [ripe.net](https://www.ripe.net/) (specifically: [iso3166-countrycodes.txt](http://riii.me/ripe-iso3166))
3. [NISOCountries.Wikipedia.CSQ](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Wikipedia.CSQ) retrieves data from [wikipedia](http://wikipedia.org/) (specifically [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1), using [CsQuery](https://github.com/jamietre/CsQuery))
4. [NISOCountries.Wikipedia.HAP](https://github.com/RobThree/NISOCountries/tree/master/NISOCountries.Wikipedia.HAP) retrieves data from [wikipedia](http://wikipedia.org/) (specifically [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1), using [Html Agility Pack](http://htmlagilitypack.codeplex.com/))

Everything is available as [NuGet package](https://www.nuget.org/packages?q=nisocountries).

Implementing your very own dataprovider is simple so feel free to add to the above list your very own!

**NOTE:** None of the above sources are '[official](http://www.iso.org/iso/country_codes.htm)' and all of the above contain, sometimes subtly, different data. Which data you want to rely on is up to you.

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
        //Retrieve ISO 3166-1 data from Wikipedia
        var countries = new WikipediaISOCountryReader().GetDefault();

        //Voila!
        Console.WriteLine(countries.Where(c => c.Alpha2 == "NL").First().CountryName);
    }
}
````
Output:
````
Netherlands
````

NISOCountries.Core provides an `ISOCountryLookup` class that offers methods to lookup countries by their alpha-2, alpha-3 and numeric codes (e.g. "US", "USA", "840") and even provides "autodetection" of the type of code and `TryGet()` methods to handle non-existing codes more easily:

````c#
var countries = new WikipediaISOCountryReader().GetDefault();
var lookup = new ISOCountryLookup<WikipediaCountry>(countries);

//Use 'autodetection'
lookup.Get("US");
lookup.Get("USA");
lookup.Get("840");

//A bit more explicit:
lookup.GetByAlpha("US");
lookup.GetByAlpha("USA");

//Even more explicit:
lookup.GetByAlpha2("US");
lookup.GetByAlpha3("USA");
lookup.GetByNumeric("USA");

//TryGet methods
WikipediaCountry result;
lookup.TryGet("XX", out result);
lookup.TryGetByAlpha2("XX", out result);
lookup.TryGetByAlpha3("XXX", out result);
lookup.TryGetByNumeric("999", out result);
````

## Core

The NISOCountry.Core library provides all required baseclasses and interfaces to be implemented when implementing your own country-data provider. A dataprovider is set of objects that implement interfaces like the `IISOCountry`, `ISourceProvider` and `ISOCountryReader<T>`. An `ISOCountryReader<T>` is the object that handles reading the underlying data (using the `ISourceProvider` and `IStreamParser<T>`), normalizing the data (using an `IValueNormalizer<T>`) and returning the normalized data as `IEnumerable<T>` where `T` implements `IISOCountry`. This way, you can create objects that read data from the web, files or other sources, have specific normalization rules (for example: uppercase all names or strip diacritics or...) and return data the way *you* want it. The `ISOCountryLookup<T>` and some classes implementing `ISOCountryComparer<T>` etc. are all provided in the core and more specific implementations may, or can, be provided by the provider-specific packages.

NISOCountries currently provides 4 implementations, as mentioned above, that provide guidance on how to implement your own dataprovider. Two of these implementations use Wikipedia and only differ in the way the data is extracted (using either CsQuery or the HtmlAgilityPack), one uses data from Ripe and one uses data from GeoNames.org. Some simple form of caching is built-in for these dataproviders to prevent the sources of the data being accessed too often ("play nice").

The library, or the packages if you will, is built to be extensible but still easy to use. There are many overloads and/or conveniencemethods that allow you to either be very specific in how you want to use the library and 'massaging' of the underlying data or to rely on default settings and keep it to a minimum.

[![Build status](https://ci.appveyor.com/api/projects/status/bnrsj4uyo3hggh9m)](https://ci.appveyor.com/project/RobIII/nisocountries) <a href="https://www.nuget.org/packages/NISOCountries.Core/"><img src="http://img.shields.io/nuget/v/NISOCountries.Core.svg?style=flat-square" alt="NuGet version" height="18"></a> <a href="https://www.nuget.org/packages/NISOCountries.Core/"><img src="http://img.shields.io/nuget/dt/NISOCountries.Core.svg?style=flat-square" alt="NuGet version" height="18"></a>
