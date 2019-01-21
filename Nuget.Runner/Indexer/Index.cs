using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Runner.Indexer
{
	public class Index
	{
		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("resources")]
		public List<Resource> Resources { get; set; }
	}

	public class Resource
	{
		[JsonProperty("@id")]
		public string Id { get; set; }

		[JsonProperty("@type")]
		public string Type { get; set; }

		[JsonProperty("@comment")]
		public string Comment { get; set; }
	}
}
