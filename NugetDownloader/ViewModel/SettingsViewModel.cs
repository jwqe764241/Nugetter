using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

using NugetDownloader.Model;
using NugetDownloader.Event;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace NugetDownloader.ViewModel
{
    class SettingsViewModel : BaseViewModel
    {
		private ObservableCollection<Source> defaultSources;
        private ObservableCollection<Source> userDefinedSources;

        public ICommand AddDefinedSourceCommand { get; set; }

        public SettingsViewModel()
		{
            defaultSources = new ObservableCollection<Source>();
            userDefinedSources = new ObservableCollection<Source>();

            AddDefinedSourceCommand = new BaseCommand(AddDefinedSourceClicked);

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

        public void AddDefinedSourceClicked(object param)
        {
            AddDefinedSource((Source)param);
        }
    }
}
