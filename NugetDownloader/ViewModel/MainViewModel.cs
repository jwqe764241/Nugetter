using System.ComponentModel;
using System.Windows.Input;

using NugetDownloader.Event;

namespace NugetDownloader.ViewModel
{
	class MainViewModel : BaseViewModel
	{
		public ICommand SettingsCommand { get; set; }
		public ICommand PackageCommand { get; set; }

		private object selectedViewModel;
		public object SelectedViewModel
		{
			get { return selectedViewModel; }
			set
			{
				selectedViewModel = value;
				OnPropertyChanged("SelectedViewModel");
			}
		}

		private SettingsViewModel _SettingsViewModel { get; set; }
		private PackageViewModel _PackageViewModel { get; set; } 

		public MainViewModel()
		{
			SettingsCommand = new BaseCommand(OpenSettings);
			PackageCommand = new BaseCommand(OpenPackage);

			_SettingsViewModel = new SettingsViewModel();
			_PackageViewModel = new PackageViewModel();

			SelectedViewModel = _SettingsViewModel;
		}

		private void OpenSettings(object obj)
		{
			SelectedViewModel = _SettingsViewModel;
		}

		private void OpenPackage(object obj)
		{
			SelectedViewModel = _PackageViewModel;
		}
	}
}
