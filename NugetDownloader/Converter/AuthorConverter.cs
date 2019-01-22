using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Markup;
using System.Windows.Data;

namespace NugetDownloader.Converter
{
    public class AuthorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as string[];
            var count = list.Count();

            if (count != 0)
            {
                StringBuilder builder = new StringBuilder("작성자: ");
                
                for(int i = 0; i < count; ++i)
                {
                    builder.Append(list[i]);

                    if (i != (count - 1))
                        builder.Append(", ");
                }

                return builder.ToString();
            }

            return "작성자: 없음";
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
