namespace ConversionLibraryTest.Converter;
using System.Collections.Generic;
using ConversionLibrary.Converter;
using NUnit.Framework;

public class LengthConverterTest
{
    private static readonly List<string> testInputs = new List<string>{"15505.5 decimeter", "15.52 Hectoinch", "15.214 Miles", "10 meter", "15250 decifoot"};
    private static readonly List<string> testTargetUnits = new List<string>{"kilometer", "miles", "centimeter", "millimeter", "Mile"};
    private static readonly List<string> expectedResults = new List<string>{"1.55 kilometer", "0.02 miles", "2,447,932.60 centimeter", "10,000.00 millimeter", "0.29 mile"};

    public static List<string> TestInputs => testInputs;

    public static List<string> TestTargetUnits => testTargetUnits;

    public static List<string> ExpectedResults => expectedResults;

    [Test]
    public void GetResultByNewInstance()
    {
        int i = 0;
        foreach (var input in testInputs){

            var converter = new LengthConverter(input, testTargetUnits[i]);
            string result = converter.GetResult();
            Assert.AreEqual(result, expectedResults[i], $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        } 
    }

    [Test]
    public void GetResultNoNewInstances()
    {
        int i = 0;
        var converter = new LengthConverter();
        foreach (var input in testInputs){

            converter.SetNewInput(input, testTargetUnits[i]);
            string result = converter.GetResult();
            Assert.AreEqual(result, expectedResults[i], $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        } 
    }

    [Test]
    public void TestException()
    {

    }
}