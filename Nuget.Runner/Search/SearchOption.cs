using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Search
{
    public class SearchOption
	{
		public string q { get; set; }

		public int skip { get; set; }

		public int take { get; set; }

		public bool prerelease { get; set; }

		public string semVerLevel { get; set; }
	}
}
