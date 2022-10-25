namespace ConversionLibraryTest.Converter;
using System.Collections.Generic;
using ConversionLibrary.Converter;
using ConversionLibrary.Converter.Base;
using Moq;
using NUnit.Framework;

public class DataConverterTest
{
    private static readonly List<string> testInputs = new() { "15505.5 kilobyte", "15525 megabyte", "15.214 terabyte", "32 bit"};
    private static readonly List<string> testTargetUnits = new() { "bit", "gigabyte", "megabyte", "byte"};
    private static readonly List<string> expectedResults = new() { "124,044,000.00 bit", "15.53 gigabyte", "15,214,000.00 megabyte", "4.00 byte"};

    public static List<string> TestInputs => testInputs;

    public static List<string> TestTargetUnits => testTargetUnits;

    public static List<string> ExpectedResults => expectedResults;

    [Test]
    public void GetResult()
    {
        int i = 0;
        // Arrange
        var mock = new Mock<StringParser>(new List<string>(){"byte", "bit"});
        //mock.Setup(p => p.GetParserResults((It.IsAny<string>(),It.IsAny<string>(), CategoryEnum.Data )).Returns(StringParserResult);

        var converter = new DataConverter(mock.Object);
        foreach (var input in testInputs){

            string result = converter.GetResult(input, testTargetUnits[i]);
            Assert.That(expectedResults[i], Is.EqualTo(result), $"The result should have been {expectedResults[i]}, but was {result}");
            i++;
        } 
    }
}