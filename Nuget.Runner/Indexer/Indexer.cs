using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Runner.Indexer
{
	public class Indexer
	{
		private string indexUrl;

		private List<Resource> resources;

		public Indexer(string indexUrl)
		{
            SetIndexUrl(indexUrl);
		}

        public void SetIndexUrl(string indexUrl)
        {
            this.indexUrl = indexUrl;

            if(this.resources != null)
            {
                this.resources.Clear();
                this.resources.TrimExcess();
            }
            else
            {
                this.resources = new List<Resource>();
            }

            LoadResource();
        }

		private void LoadResource()
		{
			HttpClient httpClient = new HttpClient();
			var responseString = httpClient.GetStringAsync(indexUrl).Result;
			var index = JsonConvert.DeserializeObject<Index>(responseString);

			resources.AddRange(index.Resources);
		}

		public List<Resource> GetResource(string type)
		{
			return resources.Where(r => r.Type.StartsWith(type)).ToList();
		}

		public List<Resource> GetResource()
		{
			return resources;
		}
	}
}
