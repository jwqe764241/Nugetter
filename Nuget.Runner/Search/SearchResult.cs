using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Runner.Search
{
    public class SearchResult
	{
		[JsonProperty("@context")]
		public Context Context { get; set; }

		[JsonProperty]
		public int TotalHits { get; set; }

		[JsonProperty]
		public DateTime LastReopen { get; set; }

		[JsonProperty]
		public string Index { get; set; }

		[JsonProperty]
		public MetaData[] Data { get; set; }
	}

    public class Context
	{
		[JsonProperty("@vocab")]
		public string Vocab { get; set; }

		[JsonProperty("@base")]
		public string Base { get; set; }
	}

    //검색된 패키지의 메타데이터
    public class MetaData
	{
		[JsonProperty("@id")]
		public string Id { get; set; }

		[JsonProperty("@type")]
		public string Type { get; set; }

		[JsonProperty]
		public string Registration { get; set; }

		[JsonProperty("id")]
		public string _Id { get; set; }

		[JsonProperty]
		public string Version { get; set; }

		[JsonProperty]
		public string Description { get; set; }

		[JsonProperty]
		public string Summary { get; set; }

		[JsonProperty]
		public string Title { get; set; }

		[JsonProperty]
		public string IconUrl { get; set; }

		[JsonProperty]
		public string LicenseUrl { get; set; }

		[JsonProperty]
		public string ProjectUrl { get; set; }

		[JsonProperty]
		public string[] Tags { get; set; }

		[JsonProperty]
		public string[] Authors { get; set; }

		[JsonProperty]
		public int TotalDownloads { get; set; }

		[JsonProperty]
		public bool Verified { get; set; }

		[JsonProperty]
		public Version[] Versions { get; set; }
	}

    public class Version
	{
		[JsonProperty("version")]
		public string Name { get; set; }

		[JsonProperty]
		public int Downloads { get; set; }

		[JsonProperty("@id")]
		public string Id { get; set; }
	}

	
}
