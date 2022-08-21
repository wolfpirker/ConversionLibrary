namespace ConversionLibrary.Converter.Base
{
    public class FromUnit
    {
        private readonly double _value;
        private readonly string _unitName;
        private readonly Int16 _base10; 

        public FromUnit(string unitName, Int16 base10, double value)
        {
            this._unitName = unitName;
            this._base10 = base10;
            this._value = value;            
        }

        public double Value => _value;
        public string UnitName => _unitName;
        public Int16 Base10 => _base10;
    }
}