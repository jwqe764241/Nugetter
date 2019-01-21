using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using NugetDownloader.Utils;

namespace NugetDownloader.ViewModel
{
	class MainViewModel : INotifyPropertyChanged
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

		public MainViewModel()
		{
			SettingsCommand = new BaseCommand(OpenSettings);
			PackageCommand = new BaseCommand(OpenPackage);

			SelectedViewModel = new SettingsViewModel();
		}

		private void OpenSettings(object obj)
		{
			SelectedViewModel = new SettingsViewModel();
		}

		private void OpenPackage(object obj)
		{
			SelectedViewModel = new PackageViewModel();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
