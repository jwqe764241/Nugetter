using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

using NugetDownloader.Model;
using NugetDownloader.Event;

namespace NugetDownloader.ViewModel
{
    class SettingsViewModel : BaseViewModel
    {
		private List<Source> defaultSources;
        private List<Source> userDefinedSources;

		public SettingsViewModel()
		{
            defaultSources = new List<Source>();
            userDefinedSources = new List<Source>();

            //기본 패키지 소스
            AddDefaultSource(new Source { Name = "MyGet", Url = "https://www.myget.org/F/workflow/", Selected = true, Type = SourceType.Default });
            AddDefaultSource(new Source { Name = "NuGet", Url = "https://api.nuget.org/v2/index.json", Selected = true, Type = SourceType.Default });
        }

		public IEnumerable<Source> DefaultSources
		{
			get { return defaultSources; }
		}
        public IEnumerable<Source> UserDefinedSources
        {
            get { return userDefinedSources; }
        }

        public void AddDefaultSource(Source source)
        {
            defaultSources.Add(source);
            OnPropertyChanged("DefaultSources");
        }

        public void AddDefinedSource(Source source)
        {
            userDefinedSources.Add(source);
            OnPropertyChanged("UserDefinedSources");
        }
    }
}
