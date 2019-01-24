using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using System.Windows.Markup;
using Runner.Search;

using System.Windows;

namespace NugetDownloader.Converter
{
	public class VersionConverter : MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var versions = (Runner.Search.Version[]) value;
			var versionText = versions.Select(x => x.Name).ToArray();

			return versionText.OrderByDescending(x => new System.Version(x)).ToList();
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
