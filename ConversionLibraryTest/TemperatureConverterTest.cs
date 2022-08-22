namespace ConversionLibraryTest.Converter;
using System.Collections.Generic;
using ConversionLibrary.Converter;
using ConversionLibrary.Converter.Exceptions;
using NUnit.Framework;

public class TemperatureConverterTest
{
    private static readonly List<string> testInputs = new List<string>{"15.5 Celsius", "132.4 fahrenheit", "12.5 centicelsius"};
    private static readonly List<string> testTargetUnits = new List<string>{"Fahrenheit", "celsius", "millifahrenheit"};
    private static readonly List<string> expectedResults = new List<string>{"59.90 fahrenheit", "55.78 celsius", "32,225.00 millifahrenheit"};

    public static List<string> TestInputs => testInputs;

    public static List<string> TestTargetUnits => testTargetUnits;

    public static List<string> ExpectedResults => expectedResults;

    [Test]
    public void GetResultByNewInstance()
    {
        int i = 0;
        foreach (var input in TestInputs){

            var converter = new TemperatureConverter(input, TestTargetUnits[i]);
            string result = converter.GetResult();
            Assert.AreEqual(result, ExpectedResults[i], $"The result should have been {ExpectedResults[i]}, but was {result}");
            i++;
        } 
    }

    [Test]
    public void GetResultNoNewInstances()
    {
        int i = 0;
        var converter = new TemperatureConverter();
        foreach (var input in TestInputs){

            converter.SetNewInput(input, TestTargetUnits[i]);
            string result = converter.GetResult();
            Assert.AreEqual(result, ExpectedResults[i], $"The result should have been {ExpectedResults[i]}, but was {result}");
            i++;
        } 
    }

    [Test]
    public void RaiseConversionNotSupportedException()
    {
        var converter = new UniversalConverter("10 kelvin", "celsius");
        Assert.Throws<ConversionNotSupportedException>(() => converter.GetResult());
    }
}