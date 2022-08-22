using ConversionLibrary;
using ConversionLibrary.Converter.Base;

// See https://aka.ms/new-console-template for more information; about .NET6 changes

static string getResultOfConversion<T>(T converter) where T : BaseConverter
{
    string? inputValue, unitTarget;
    System.Console.WriteLine($"supported units: {string.Join(", ", converter.SupportedUnits)} (in combination with SI-Prefixes)");
    System.Console.WriteLine("Enter input value with unit, e.g.: 1.25 <unit>, etc.:");
    inputValue = Console.ReadLine();
    System.Console.WriteLine("Enter target unit:");
    unitTarget = Console.ReadLine();
    converter.SetNewInput(inputValue, unitTarget);
    return converter.GetResult();
}

System.Console.WriteLine(@"Program to convert between different units of length, data and temperature.
syntax rules: 
  *) as input give the value of the unit, separate with a space and write the standard SI-prefix 
  with full written unit name
  *) as target unit also write the full SI-prefix and full unitname.

to start enter first a single number for the type of conversion:
  0) universal converion - decides which converter to use itself
  1) length conversion
  2) data conversion
  3) temperature conversion  
  q) quit");

while (true){

    string mode = Console.ReadLine();
    var universalConverter = new ConversionLibrary.Converter.UniversalConverter();
    var lengthConverter = new ConversionLibrary.Converter.LengthConverter();
    var dataConverter = new ConversionLibrary.Converter.DataConverter();
    var temperatureConverter = new ConversionLibrary.Converter.TemperatureConverter();
    switch(mode){
        case "0":
            System.Console.WriteLine($"Type of Conversion: Universal conversion");
            System.Console.WriteLine($"{getResultOfConversion(universalConverter)}");
            break;
        case "1":
            System.Console.WriteLine($"Type of Conversion: Length conversion");
            System.Console.WriteLine($"{getResultOfConversion(lengthConverter)}");
            break;
        case "2":
            System.Console.WriteLine($"Type of Conversion: Data conversion");
            System.Console.WriteLine("Note: binary prefixes are not supported: kibi, mebi, gibi, tebi.");
            System.Console.WriteLine($"{getResultOfConversion(dataConverter)}");
            break;
        case "3":
            System.Console.WriteLine($"Type of Conversion: Temperature conversion");
            System.Console.WriteLine($"{getResultOfConversion(temperatureConverter)}");
            break;
        default:
            return;
    }
    System.Console.WriteLine("Enter number for type of conversion (0, 1, 2 or 3) or q to quit:");
}