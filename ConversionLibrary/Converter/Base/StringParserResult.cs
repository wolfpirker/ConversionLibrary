namespace ConversionLibrary.Converter.Base
{
    // class to be used by BaseConverter
    // extract all relevant information from input strings;
    // including SI Prefixes, and category of conversion (length, data, temperature)

    public class StringParserResult{
        private readonly FromUnit _fromUnit;
        private readonly ToUnit _toUnit;

        public StringParserResult(FromUnit fromUnit, ToUnit toUnit)
        {
            this._fromUnit = fromUnit;
            this._toUnit = toUnit;
        }

        public FromUnit FromUnit => _fromUnit;
        public ToUnit ToUnit => _toUnit;
    }
}