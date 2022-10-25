using ConversionLibrary;
using ConversionLibrary.Converter;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Contract;

// See https://aka.ms/new-console-template for more information; about .NET6 changes

static string getResultOfConversion<T>(T converter, IEnumerable<string> units) where T : IConverter
{
    string? inputValue, unitTarget;
    System.Console.WriteLine("supported units: " + string.Join(", ", units) + " (in combination with SI-Prefixes)");
    System.Console.WriteLine("Enter input value with unit, e.g.: 1.25 <unit>, etc.:");
    inputValue = Console.ReadLine();
    System.Console.WriteLine("Enter target unit:");
    unitTarget = Console.ReadLine();
    return converter.GetResult(inputValue, unitTarget);
}

System.Console.WriteLine(@"Program to convert between different units of length, data and temperature.
syntax rules: 
  *) as input give the value of the unit, separate with a space and write the standard SI-prefix 
  with full written unit name
  *) as target unit also write the full SI-prefix and full unitname.

to start enter first a single number for the type of conversion:
  1) length conversion
  2) data conversion
  3) temperature conversion  
  q) quit");

while (true){

    string mode = Console.ReadLine();
    var lengthConverter = new LengthConverter(new StringParser(LengthConverter.SupportedUnits));
    var dataConverter = new DataConverter(new StringParser(DataConverter.SupportedUnits));
    var temperatureConverter = new TemperatureConverter(new StringParser(TemperatureConverter.SupportedUnits));
    switch(mode){
        case "1":
            System.Console.WriteLine($"Type of Conversion: Length conversion");
            System.Console.WriteLine($"{getResultOfConversion(lengthConverter, LengthConverter.SupportedUnits)}");
            break;
        case "2":
            System.Console.WriteLine($"Type of Conversion: Data conversion");
            System.Console.WriteLine("Note: binary prefixes are not supported: kibi, mebi, gibi, tebi.");
            System.Console.WriteLine($"{getResultOfConversion(dataConverter, DataConverter.SupportedUnits)}");
            break;
        case "3":
            System.Console.WriteLine($"Type of Conversion: Temperature conversion");
            System.Console.WriteLine($"{getResultOfConversion(temperatureConverter, TemperatureConverter.SupportedUnits)}");
            break;
        default:
            return;
    }
    System.Console.WriteLine("Enter number for type of conversion (0, 1, 2 or 3) or q to quit:");
}