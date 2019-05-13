using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Runner.Manifest
{
    public static class Manifest
	{
		public static string RequiredType
		{
			get { return "PackageBaseAddress"; }
		}

		public static ManifestResult Run(string id, string lowId, string version)
		{
			HttpClient httpClient = new HttpClient();
			var resultString = httpClient.GetStringAsync(GetUrl(id, lowId, version)).Result;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(resultString);
			
			string json = JsonConvert.SerializeXmlNode(doc["package"]["metadata"], Newtonsoft.Json.Formatting.None, true);

			return JsonConvert.DeserializeObject<ManifestResult>(json);
		}

		public static string GetUrl(string id, string lowId, string version)
		{
			StringBuilder builder = new StringBuilder(id);
			//ID and version text must be lower case
			builder.AppendFormat("{0}/{1}/{0}.nuspec", lowId.ToLowerInvariant(), version.ToLowerInvariant());

			return builder.ToString();
		}
	}
}
