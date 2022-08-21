namespace ConversionLibrary.Converter.Base
{
    public class ToUnit
    {
        private readonly string _unitName;
        private readonly string _siPrefix;
        private readonly Int16 _base10;

        public ToUnit(string unitName, Int16 base10, string siPrefix)
        {
            this._unitName = unitName;
            this._base10 = base10;
            this._siPrefix = siPrefix;
        }

        public string UnitName => _unitName;
        public string SiPrefix => _siPrefix;
        public Int16 Base10 => _base10;
                
    }    
}