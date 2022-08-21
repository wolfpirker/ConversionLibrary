using ConversionLibrary;

// See https://aka.ms/new-console-template for more information

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
    string? inputValue, unitTarget;
    string mode = Console.ReadLine();
    switch(mode){
        case "1":
            System.Console.WriteLine("Enter input value with unit, e.g.: 1.25 kilometer, 3 kiloinches, 3.5 foot:");
            inputValue = Console.ReadLine();
            System.Console.WriteLine("Enter target unit, e.g.: meter, millimeter, kiloinch:");
            unitTarget = Console.ReadLine();
            var lengthConverter = new ConversionLibrary.Converter.LengthConverter(inputValue, unitTarget);
            Console.WriteLine(lengthConverter.GetResult());
            break;
        case "2":
            System.Console.WriteLine("Note: binary prefixes are not supported: kibi, mebi, gibi, tebi.");
            System.Console.WriteLine("Enter input value with unit, e.g.: 8 bit, 12 kilobyte, 2.5 terabyte:");
            inputValue = Console.ReadLine();
            System.Console.WriteLine("Enter target unit, e.g.: bit, kilobyte, terabyte:");
            unitTarget = Console.ReadLine();
            var dataConverter = new ConversionLibrary.Converter.DataConverter(inputValue, unitTarget);
            Console.WriteLine(dataConverter.GetResult());
            break;
        case "3":
            System.Console.WriteLine("Enter input value with unit, e.g.: 1.25 celsius, 15.2 fahrenheit:");
            inputValue = Console.ReadLine();
            System.Console.WriteLine("Enter target unit: celsius or fahrenheit:");
            unitTarget = Console.ReadLine();
            var tempConverter = new ConversionLibrary.Converter.TemperatureConverter(inputValue, unitTarget);
            Console.WriteLine(tempConverter.GetResult());
            break;
        default:
            return;
    }
    System.Console.WriteLine("Enter number for type of conversion or q (1, 2 or 3):");
}