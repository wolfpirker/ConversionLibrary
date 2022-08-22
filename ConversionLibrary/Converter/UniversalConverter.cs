using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Exceptions;

namespace ConversionLibrary.Converter
{
    // this converter class allows conversion without giving the 
    // specific conversion category; like a mediator
    public class UniversalConverter : BaseConverter
    {
        private CategoryEnum ConverterCategory = CategoryEnum.Invalid;
        private LengthConverter lengthConverter;
        private DataConverter dataConverter;
        private TemperatureConverter temperatureConverter;

        private IEnumerable<string> _supportedUnits;
        public UniversalConverter() 
        {
            InitializeConvertersAndUnits();
        }

        public UniversalConverter(string inputValue, string targetUnit) : base(inputValue, targetUnit)
        {
            InitializeConvertersAndUnits();
        }

        private void InitializeConvertersAndUnits(){
            lengthConverter = new LengthConverter();
            dataConverter = new DataConverter();
            temperatureConverter = new TemperatureConverter();
            _supportedUnits = lengthConverter.SupportedUnits.Concat(
                dataConverter.SupportedUnits).Concat(temperatureConverter.SupportedUnits);
        }

        public override IEnumerable<string> SupportedUnits => _supportedUnits;

        public override string GetResult(){
            ConverterCategory = getConverterCategoryMatch();

            if (ConverterCategory == CategoryEnum.Length){
                return useConverter(lengthConverter);
            }
            else if (ConverterCategory == CategoryEnum.Data){
                return useConverter(dataConverter);
            }
            else if (ConverterCategory == CategoryEnum.Temperature){
                return useConverter(temperatureConverter);
            } 
            throw new ConversionNotSupportedException();
        }        

        private string useConverter<T>(T converter) where T : BaseConverter{
            converter.SetNewInput(InputValue, TargetUnit);
            return converter.GetResult();
        }

        // find a match of the units with a converter category of supported units
        private CategoryEnum getConverterCategoryMatch(){
            string trimmedInput = InputValue.Trim().ToLower();
            if (lengthConverter.SupportedUnits.Any(unit => trimmedInput.EndsWith(unit))){
                return CategoryEnum.Length;
            }
            else if (dataConverter.SupportedUnits.Any(unit => trimmedInput.EndsWith(unit))){
                return CategoryEnum.Data;
            }
            else if (temperatureConverter.SupportedUnits.Any(unit => trimmedInput.EndsWith(unit))){
                return CategoryEnum.Temperature;
            }
            return CategoryEnum.Invalid;
        }
    }
}