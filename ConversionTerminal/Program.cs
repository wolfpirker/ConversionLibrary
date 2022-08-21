using ConversionLibrary;
using ConversionLibrary.Converter.Base;

// See https://aka.ms/new-console-template for more information

string getResultOfConversion<T>(T converter) where T : BaseConverter
{
    string? inputValue, unitTarget;
    System.Console.WriteLine($"supported units: {converter.SupportedUnits} (in combination with SI-Prefixes)");
    System.Console.WriteLine("Enter input value with unit, e.g.: 1.25 <unit>, etc.:");
    inputValue = Console.ReadLine();
    System.Console.WriteLine("Enter target unit:");
    unitTarget = Console.ReadLine();
    converter.GetNewInput(inputValue, unitTarget);
    return converter.GetResult();
}

System.Console.WriteLine(@"Program to convert between different units of length, data and temperature.
syntax rules: 
  1) as input give the value of the unit, separate with a space and write the standard SI-prefix 
  with full written unit name
  2) as target unit also write the full SI-prefix and full unitname.

to start enter first a single number for the type of conversion:
  1) length conversion
  2) data conversion
  3) temperature conversion
  q) quit");

while (true){

    string mode = Console.ReadLine();
    var lengthConverter = new ConversionLibrary.Converter.LengthConverter();
    var dataConverter = new ConversionLibrary.Converter.DataConverter();
    var temperatureConverter = new ConversionLibrary.Converter.TemperatureConverter();
    switch(mode){
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
    System.Console.WriteLine("Enter number for type of conversion or q (1, 2 or 3):");
}