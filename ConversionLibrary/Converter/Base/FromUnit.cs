namespace ConversionLibrary.Converter.Base
{
    public class FromUnit
    {
        private readonly double _value;
        private readonly string _unitName;
        private readonly Int16 _base10;
        private readonly string _siPrefix;
        

        public FromUnit(string unitName, Int16 base10,  string siPrefix, double value)
        {
            this.SiPrefix = siPrefix;
            this._unitName = unitName;
            this._base10 = base10;
            this._siPrefix = siPrefix;
            this._value = value;            
        }
        
        public Int16 Base10 => _base10;
        public string UnitName => _unitName;
        public string SiPrefix { get; }
        public double Value => _value;
        
    }
}