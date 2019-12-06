namespace JsonViewerControl.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Newtonsoft.Json.Linq;

    public sealed class JValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is JValue jValue)) { return value; }
            return jValue.Type switch
            {
                JTokenType.String => ("\"" + jValue.Value + "\""),
                JTokenType.Null => "Null",
                _ => value
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(this.GetType().Name + " can only be used for one way conversion.");
        }
    }
}
