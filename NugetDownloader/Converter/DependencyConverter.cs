using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

using Runner.Manifest;

namespace NugetDownloader.Converter
{
	public class DependencyConverter : MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var group = (Group)value;
			var members = group?.Members;

			if(members != null)
			{
				foreach (Member member in members)
				{
					StringBuilder builder = new StringBuilder();

					var name = member.TargetFramework;

					builder.Append(name + "");

					var dependencies = member.Dependencies;

					if (dependencies != null)
					{
						foreach (Dependency dependency in dependencies)
						{
							builder.Append(dependency.Id + " " + dependency.Version);
						}
					}
				}
			}
			
			return "";
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
