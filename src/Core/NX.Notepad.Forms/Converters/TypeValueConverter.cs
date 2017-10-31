using System;
using System.Globalization;
using Xamarin.Forms;

namespace NX.Notepad.Converters
{
    public abstract class TypeValueConverter<TFrom> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TFrom) value, parameter, culture);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual object Convert(TFrom value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TypeValueConverter<TFrom, TTo> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TFrom) value, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TTo) value, parameter, culture);
        }

        protected virtual TTo Convert(TFrom value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual TFrom ConvertBack(TTo value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TypeValueConverter<TFrom, TTo, TParameter> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TFrom) value, (TParameter) parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TTo) value, (TParameter) parameter, culture);
        }

        protected virtual TTo Convert(TFrom value, TParameter parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual TFrom ConvertBack(TTo value, TParameter parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TypeValueConverter<TFrom, TTo, TConvertParameter, TConvertBackParameter> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TFrom) value, (TConvertParameter) parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TTo) value, (TConvertBackParameter) parameter, culture);
        }

        protected virtual TTo Convert(TFrom value, TConvertParameter parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual TFrom ConvertBack(TTo value, TConvertBackParameter parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}