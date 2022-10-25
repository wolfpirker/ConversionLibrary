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
        List<string> testInputs = DataConverterTest.TestInputs;
        List<string> testTargetUnits = DataConverterTest.TestTargetUnits;
        List<string> siPrefixInput = new()
        {
            "kilo", "mega", "tera", ""
        };
        List<string> siPrefixTarget = new()
        {
            "", "giga", "mega", ""
        };
        List<string> siUnitInput = new()
        {
            "byte", "byte", "byte", "bit"
        };
        List<string> siUnitTarget = new()
        {
            "bit", "byte", "byte", "byte"
        };

        CategoryEnum category = CategoryEnum.Data;

        int i = 0;
        foreach (string input in testInputs){        

            var parser = new StringParser(new List<string> (){"byte", "bit"});          
            StringParserResult result = parser.GetParserResults(input, testTargetUnits[i], category);   
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
        var sampleUnits = new List<string>(){"byte", "bit"};
        var parser = new StringParser(sampleUnits);
         Assert.Throws<ConversionNotSupportedException>(() => parser.GetParserResults("125 kibibyte", "byte", CategoryEnum.Data));          
    }

    [Test]
    public void RaiseInvalidInputFormatException()
    {
        var sampleUnits = new List<string>(){"byte", "bit"};
        var parser = new StringParser(sampleUnits);            
         Assert.Throws<InvalidInputFormatException>(() => parser.GetParserResults("125 kilobyte millibit", "byte", CategoryEnum.Data));          
    }
}
