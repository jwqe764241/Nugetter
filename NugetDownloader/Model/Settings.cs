using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NugetDownloader.Model
{
	class Settings
	{
        public List<Source> Sources
        {
            get;
            set;
        }

		public Settings()
		{
            Sources = new List<Source>();
		}

		public void AddSource(Source source)
		{
            if (Sources.Count(p => p.Name.Equals(source.Name)) < 1)
                Sources.Add(source);
		}

		public void RemoveSource(string name)
		{
            Sources.Remove(Sources.Single(p => p.Name.Equals(name)));
		}
	}
}
