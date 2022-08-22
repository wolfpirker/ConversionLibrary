namespace ConversionLibraryTest.Converter;
using System.Collections.Generic;
using ConversionLibrary.Converter;
using ConversionLibrary.Converter.Exceptions;
using NUnit.Framework;

public class UniversalConverterTest
{
    private static List<string> testInputs;
    private static List<string> testTargetUnits;
    private static List<string> expectedResults;

    [SetUp]
    public void TestInit()
    {
        testInputs = LengthConverterTest.TestInputs.Concat(DataConverterTest.TestInputs.Concat(TemperatureConverterTest.TestInputs)).ToList<string>();
        testTargetUnits = LengthConverterTest.TestTargetUnits.Concat(DataConverterTest.TestTargetUnits.Concat(TemperatureConverterTest.TestTargetUnits)).ToList<string>();
        expectedResults = LengthConverterTest.ExpectedResults.Concat(DataConverterTest.ExpectedResults.Concat(TemperatureConverterTest.ExpectedResults)).ToList<string>();
    }

    [Test]
    public void GetResultByNewInstance()
    {
        int i = 0;
        foreach (string input in testInputs){
            var converter = new UniversalConverter(input, testTargetUnits[i]);
            string result = converter.GetResult();
            Assert.That(expectedResults[i], Is.EqualTo(result), $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        } 
    }

    [Test]
    public void GetResultNoNewInstances()
    {
        int i = 0;
        var converter = new UniversalConverter();
        foreach (string input in testInputs){

            converter.SetNewInput(input, testTargetUnits[i]);
            string result = converter.GetResult();
            Assert.That(expectedResults[i], Is.EqualTo(result), $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        }         
    }

    [Test]
    public void RaiseConversionNotSupportedException()
    {
        var converter = new UniversalConverter("10 kibibyte", "byte");
        Assert.Throws<ConversionNotSupportedException>(() => converter.GetResult());
    }
}