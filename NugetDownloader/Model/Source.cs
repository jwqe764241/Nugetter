using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetDownloader.Model
{
	enum SourceType
	{
		Default,
		UserDefined
	}

	class Source
	{
		public string Url { get; set; }

		public string Name { get; set; }

		public SourceType Type { get; set; }
	}
}
