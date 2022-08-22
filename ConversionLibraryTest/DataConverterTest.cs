namespace ConversionLibraryTest.Converter;
using System.Collections.Generic;
using ConversionLibrary.Converter;
using NUnit.Framework;

public class DataConverterTest
{
    private static readonly List<string> testInputs = new List<string>{"15505.5 kilobyte", "15525 megabyte", "15.214 terabyte"};
    private static readonly List<string> testTargetUnits = new List<string>{"bit", "gigabyte", "megabyte"};
    private static readonly List<string> expectedResults = new List<string>{"124,044,000.00 bit", "15.53 gigabyte", "15,214,000.00 megabyte"};

    public static List<string> TestInputs => testInputs;

    public static List<string> TestTargetUnits => testTargetUnits;

    public static List<string> ExpectedResults => expectedResults;

    [Test]
    public void GetResultByNewInstance()
    {
        int i = 0;
        foreach (var input in testInputs){

            var converter = new DataConverter(input, testTargetUnits[i]);
            string result = converter.GetResult();
            Assert.AreEqual(result, expectedResults[i], $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        } 
    }

    [Test]
    public void GetResultNoNewInstances()
    {
        int i = 0;
        var converter = new DataConverter();
        foreach (var input in testInputs){

            converter.SetNewInput(input, testTargetUnits[i]);
            string result = converter.GetResult();
            Assert.AreEqual(result, expectedResults[i], $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        } 
    }
}