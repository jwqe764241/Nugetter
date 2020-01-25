using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

using NugetDownloader.Model;
using NugetDownloader.Event;
using NugetDownloader.Global;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace NugetDownloader.ViewModel
{
    class SettingsViewModel : BaseViewModel
    {
        public ICommand AddDefinedSourceCommand { get; set; }
        public ICommand SelectSourceCommand { get; set; }

        public SettingsViewModel()
		{
            AddDefinedSourceCommand = new BaseCommand(AddDefinedSourceClicked);
            SelectSourceCommand = new BaseCommand(SelectSource);

            Source defaultSource = new Source { Name = "NuGet", Selected = true, Url = "https://api.nuget.org/v2/index.json", Type = SourceType.Default };
            AddDefaultSource(defaultSource);
            AddDefaultSource(new Source { Name = "MyGet", Selected = false, Url = "https://www.myget.org/F/workflow/", Type = SourceType.Default });

            ApiSettings.SetSelectedSource(defaultSource);
        }

		public IEnumerable<Source> DefaultSources
		{
			get { return ApiSettings.DefaultSources; }
		}
        public IEnumerable<Source> UserDefinedSources
        {
            get { return ApiSettings.UserDefinedSources; }
        }

        public void AddDefaultSource(Source source)
        {
            ApiSettings.AddDefaultSource(source);
            OnPropertyChanged("DefaultSources");
        }
           
        public void AddDefinedSource(Source source)
        {
            ApiSettings.AddDefinedSource(source);
            OnPropertyChanged("UserDefinedSources");
        }

        public void AddDefinedSourceClicked(object param)
        {
            AddDefinedSource((Source)param);
        }

        public void SelectSource(object param)
        {
            ApiSettings.SetSelectedSource((Source)param);
        }

    }
}
