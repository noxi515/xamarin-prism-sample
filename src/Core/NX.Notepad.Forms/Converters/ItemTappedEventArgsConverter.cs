using System.Globalization;
using Xamarin.Forms;

namespace NX.Notepad.Converters
{
    public class ItemTappedEventArgsConverter : TypeValueConverter<ItemTappedEventArgs>
    {
        protected override object Convert(ItemTappedEventArgs value, object parameter, CultureInfo culture)
        {
            return value?.Item;
        }
    }
}