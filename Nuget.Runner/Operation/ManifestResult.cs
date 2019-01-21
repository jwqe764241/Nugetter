using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Runner.Operation
{
    public class ManifestResult
	{
		[JsonProperty]
		public string Id { get; set; }

		[JsonProperty]
		public string Version { get; set; }

		[JsonProperty]
		public string Title { get; set; }

		[JsonProperty]
		public string Authors { get; set; }

		[JsonProperty]
		public string Owners { get; set; }

		[JsonProperty]
		public bool RequireLicenseAcceptance { get; set; }

		[JsonProperty]
		public string LicenseUrl { get; set; }

		[JsonProperty]
		public string ProjectUrl { get; set; }

		[JsonProperty]
		public string Description { get; set; }

		[JsonProperty]
		public string Summary { get; set; }

		[JsonProperty]
		public string ReleaseNotes { get; set; }

		[JsonProperty]
		public string Copyright { get; set; }

		[JsonProperty]
		public string Language { get; set; }

		[JsonProperty]
		public string Tags { get; set; }

		[JsonProperty]
		public Group Dependencies { get; set; }
	}

    public class Group
	{
		[JsonProperty("group")]
		[JsonConverter(typeof(SingleValueArrayConverter<Member>))]
		public List<Member> Members { get; set; }
	}

    public class Member
	{
		[JsonProperty("@targetFramework")]
		public string TargetFramework { get; set; }

		[JsonProperty("dependency")]
		[JsonConverter(typeof(SingleValueArrayConverter<Dependency>))]
		public List<Dependency> Dependencies { get; set; }
	}

    public class Dependency
	{
		[JsonProperty("@id")]
		public string Id { get; set; }

		[JsonProperty("@version")]
		public string Version { get; set; }
	}
}
