using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using IronPython.Hosting;

namespace Sheldon.Scripting.IronPython
{
    public class IronPythonConverter : IValueConverter
    {
        public static readonly IronPythonConverter Instance = new IronPythonConverter();
        private readonly PythonEngine _engine;

        public IronPythonConverter()
        {
            _engine = new PythonEngine();
            _engine.Import("clr");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _engine.Globals["value"] = value;
            var result = _engine.Evaluate(parameter.ToString());
            if (result == null) return null;

            var resultType = result.GetType();
            var converter = TypeDescriptor.GetConverter(resultType);
            return converter.CanConvertTo(targetType) ? converter.ConvertTo(result, targetType) : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}