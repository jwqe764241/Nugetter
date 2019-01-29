using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Markup;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Drawing;

using System.Windows;

namespace NugetDownloader.Converter
{
    public class IconConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
			{
				var bmp = Properties.Resources.nuget_package_logo;

				return Imaging.CreateBitmapSourceFromHBitmap(
					bmp.GetHbitmap(),
					IntPtr.Zero,
					Int32Rect.Empty,
					BitmapSizeOptions.FromEmptyOptions());
			}

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
