using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Runner.Operation
{
	public static class Download
	{
		public static string RequiredType
		{
			get { return "PackageBaseAddress"; }
		}

		public static byte[] Run(string id, string lowId, string version)
		{
			HttpClient httpClient = new HttpClient();

			return httpClient.GetByteArrayAsync(GetUrl(id, lowId, version)).Result;
		}

		private static string GetUrl(string id, string lowId, string version)
		{
			StringBuilder builder = new StringBuilder(id);
			//ID and version text must be lower case
			builder.AppendFormat("{0}/{1}/{0}.{1}.nupkg", lowId.ToLowerInvariant(), version.ToLowerInvariant());

			return builder.ToString();
		}
	}
}
