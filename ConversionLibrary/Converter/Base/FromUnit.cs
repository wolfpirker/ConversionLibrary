namespace ConversionLibrary.Converter.Base
{
    public class FromUnit : ToUnit
    {
        private double _value;

        public FromUnit(ToUnit to) : base(to.UnitName, to.Base10, to.SiPrefix)
        {
        }
        public FromUnit(string unitName, Int16 base10, string siPrefix) : base(unitName, base10, siPrefix)
        {
        }

        public double Value { get => _value; set => _value = value; }
    }
}