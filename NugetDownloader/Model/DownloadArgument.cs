using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetDownloader.Model
{
	public class DownloadArgument
	{
		public string Id { get; private set; }

		public string Version { get; private set; }

		public DownloadArgument(string id, string version)
		{
			Id = id;
			Version = version;
		}
	}
}
