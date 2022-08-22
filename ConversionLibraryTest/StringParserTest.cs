namespace ConversionLibraryTest.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ConversionLibrary.Converter;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Exceptions;

public class StringParserTest
{
    [Test]
    public void GetParserResults()
    {
        CategoryEnum cat = 0;
        
        List<string> testInputs = LengthConverterTest.TestInputs.Concat(DataConverterTest.TestInputs.Concat(TemperatureConverterTest.TestInputs)).ToList<string>();
        List<string> testTargetUnits = LengthConverterTest.TestTargetUnits.Concat(DataConverterTest.TestTargetUnits.Concat(TemperatureConverterTest.TestTargetUnits)).ToList<string>();
        List<string> siPrefixInput = new()
        {
            // {"15505.5 decimeter", "15.52 Hectoinch", "15.214 Miles", "15250 decifoot"}
            "deci", "hecto", "", "deci",
            // "15505.5 kilobyte", "15525 megabyte", "15.214 terabyte", "32 bit"
            "kilo", "mega", "tera", "",
            // "15.5 Celsius", "132.4 fahrenheit", "12.5 centicelsius"
            "", "", "centi"
        };
        List<string> siPrefixTarget = new()
        {
            // {"kilometer", "miles", "centimeter", "Mile"}
            "kilo", "", "centi", "",
            // {"bit", "gigabyte", "megabyte", "byte"}
            "", "giga", "mega", "",
            // "Fahrenheit", "celsius", "millifahrenheit"
            "", "", "milli"
        };
        List<string> siUnitInput = new()
        {
            "meter", "inch", "miles", "foot",
            "byte", "byte", "byte", "bit",
            "celsius", "fahrenheit", "celsius"
        };
        List<string> siUnitTarget = new()
        {
            "meter", "miles", "meter", "mile",
            "bit", "byte", "byte", "byte",
            "fahrenheit", "celsius", "fahrenheit"
        };

        Dictionary<int, CategoryEnum> categoryDict = new()
        {
            {0, CategoryEnum.Length },
            {4, CategoryEnum.Data },
            {8, CategoryEnum.Temperature }
        };
        int i = 0;
        foreach (string input in testInputs){
            if (categoryDict.ContainsKey(i)){
                cat = categoryDict[i];
            }
            var parser = new StringParser(input, testTargetUnits[i], cat);   
            System.Console.WriteLine($"{i}: {input}, {testTargetUnits[i]}");         
            StringParserResult result = parser.GetParserResults();   
            Assert.Multiple(() =>
            {
                Assert.That(siPrefixInput[i], Is.EqualTo(result.FromUnit.SiPrefix), $"The result should have been {siPrefixInput[i]}, but was {result.FromUnit.SiPrefix}");
                Assert.That(siPrefixTarget[i], Is.EqualTo(result.ToUnit.SiPrefix), $"The result should have been {siPrefixTarget[i]}, but was {result.ToUnit.SiPrefix}");
                Assert.That(siUnitInput[i], Is.EqualTo(result.FromUnit.UnitName), $"The result should have been {siUnitInput[i]}, but was {result.FromUnit.UnitName}");
                Assert.That(siUnitTarget[i], Is.EqualTo(result.ToUnit.UnitName), $"The result should have been {siUnitTarget[i]}, but was {result.ToUnit.UnitName}");
            });
            
            i++;
        }
    }

    [Test]
    public void RaiseConversionNotSupportedException()
    {
        var parser = new StringParser("125 kibibyte", "byte", CategoryEnum.Data);            
         Assert.Throws<ConversionNotSupportedException>(() => parser.GetParserResults());  
        
    }

    [Test]
    public void RaiseInvalidInputFormatException()
    {
        var parser = new StringParser("125 kilobyte millibit", "byte", CategoryEnum.Data);            
         Assert.Throws<InvalidInputFormatException>(() => parser.GetParserResults());          
    }
}
