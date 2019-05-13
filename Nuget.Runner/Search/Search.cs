using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Runner.Search
{
    public static class Search
	{
		public static string RequiredType
		{
			get { return "SearchQueryService"; }
		}

		public static SearchResult Run(string id, SearchOption option)
		{
			HttpClient httpClient = new HttpClient();

			return JsonConvert.DeserializeObject<SearchResult>(
				httpClient.GetStringAsync(GetUrl(id, option)).Result);
		}
		
		private static string GetUrl(string id, SearchOption option)
		{
			StringBuilder builder = new StringBuilder(id + "?");

			foreach (PropertyInfo property in typeof(SearchOption).GetProperties())
			{
				builder.Append(property.Name).Append("=").Append(property.GetValue(option)).Append("&");
			}

			return builder.ToString();
		}
	}
}
