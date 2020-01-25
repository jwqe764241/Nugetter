using System.ComponentModel;
using System.Windows.Input;

using NugetDownloader.Event;

namespace NugetDownloader.ViewModel
{
	class MainViewModel : BaseViewModel
	{
		public ICommand SettingsCommand { get; set; }
		public ICommand PackageCommand { get; set; }

		private BaseViewModel selectedViewModel;
		public object SelectedViewModel
		{
			get { return selectedViewModel; }
			set
			{
                selectedViewModel.ViewClosed(null);
				selectedViewModel = (BaseViewModel)value;
				OnPropertyChanged("SelectedViewModel");
                selectedViewModel.ViewOpened(null);
			}
		}

		private BaseViewModel _SettingsViewModel { get; set; }
		private BaseViewModel _PackageViewModel { get; set; } 

		public MainViewModel()
		{
			SettingsCommand = new BaseCommand(OpenSettings);
			PackageCommand = new BaseCommand(OpenPackage);

			_SettingsViewModel = new SettingsViewModel();
			_PackageViewModel = new PackageViewModel();

			selectedViewModel = _SettingsViewModel;
            selectedViewModel.ViewOpened(null);
		}

		private void OpenSettings(object obj)
		{
			SelectedViewModel = _SettingsViewModel;
		}

		private void OpenPackage(object obj)
		{
			SelectedViewModel = _PackageViewModel;
		}

        public override void ViewOpened(object param)
        {
        }

        public override void ViewClosed(object param)
        {
        }
    }
}
