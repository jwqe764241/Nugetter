using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO.Compression;

using Newtonsoft.Json;

using NugetDownloader.Event;
using NugetDownloader.Model;
using NugetDownloader.Global;

using Runner.Indexer;
using Runner.Search;
using Runner.Manifest;
using Runner.Download;
using System;

namespace NugetDownloader.ViewModel
{
	class PackageViewModel : BaseViewModel
	{
		//delete this later and add 
		private const string DownloadLocation = @"C:\NugetDownloader\downloads\";

		private ObservableCollection<MetaData> searchList;
		public ObservableCollection<MetaData> SearchList
		{
			get
			{
				return searchList;
			}
			set
			{
				searchList = value;
				OnPropertyChanged("SearchList");
			}
		}
		public string searchKeyword;
		public string SearchKeyword
		{
			get
			{
				return searchKeyword;
			}
			set
			{
				searchKeyword = value;
				OnPropertyChanged("SearchKeyword");
			}
		}
		public Indexer Indexer { get; set; }
		public MetaData SelectedItem { get; set; }
		public ManifestResult SelectedItemManifest { get; set; }
		public string SelectedVersion { get; set; }
		/*
		 * workers to query data
		 * if worker busy, cancel and run again
		 */
		private BackgroundWorker SearchWorker { get; set; }
		private BackgroundWorker ManifestWorker { get; set; }
		private BackgroundWorker DownloadWorker { get; set; }

		private Runner.Search.SearchOption SearchOption { get; set; }


		public ICommand ItemClickCommand { get; set; }
		public ICommand ShowLicenseCommand { get; set; }
		public ICommand ListScrollCommand { get; set; }
		public ICommand SearchCommand { get; set; }
		public ICommand InstallCommand { get; set; }

        public ICommand GoToProjectCommand { get; set; }
        public PackageViewModel()
        {
            ItemClickCommand = new BaseCommand(ItemClicked);
			ShowLicenseCommand = new BaseCommand(LicenseClicked);
			ListScrollCommand = new BaseCommand(ListScrolled);
			SearchCommand = new BaseCommand(InputEnterDown);
			InstallCommand = new BaseCommand(InstallClicked);
            GoToProjectCommand = new BaseCommand(GoToProjectClicked);

            SearchWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            SearchWorker.DoWork += SearchWork;
            SearchWorker.RunWorkerCompleted += SearchWorkCompleted;

			ManifestWorker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			ManifestWorker.DoWork += ManifestWork;
			ManifestWorker.RunWorkerCompleted += ManifestWorkCompleted;

			DownloadWorker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			DownloadWorker.DoWork += DownloadWork;
			DownloadWorker.RunWorkerCompleted += DownloadWorkCompleted;
		}

        public override void ViewOpened(object param)
        {
            try
            {
                searchList = new ObservableCollection<MetaData>();

                searchKeyword = "";
                SearchOption = new Runner.Search.SearchOption()
                {
                    q = searchKeyword,
                    take = 20
                };

                Indexer = new Indexer(ApiSettings.GetSelectedSource().Url);

                SearchWorker.RunWorkerAsync(SearchOption);
            }
            catch (JsonReaderException e)
            {
                MessageBox.Show("Can't read api list from selected server.");
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Invalid server url.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Unknown error in initialize indexer.");
            }
        }

        public override void ViewClosed(object param)
        {
            searchList.Clear();
        }

        public void SearchPackage(Runner.Search.SearchOption option)
        {
            if (!SearchWorker.IsBusy)
            {
                SearchWorker.RunWorkerAsync(option);
            }
        }
        private void SearchWork(object sender, DoWorkEventArgs e)
        {
            if(Indexer != null)
            {
                Runner.Search.SearchOption options = (Runner.Search.SearchOption)e.Argument;

                var result = Search.Run(Indexer.GetResource(Search.RequiredType).First().Id, options);
                e.Result = result;
            }
            else
            {
                e.Result = null;
            }
        }
        private void SearchWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Work Cancelled");
            }
            else if(e.Error != null)
            {
				MessageBox.Show("Error : " + e.Error);
            }
            else
            {
                var result = (SearchResult)e.Result;

                if (result != null)
                {
                    foreach (var item in result.Data)
                    {
                        SearchList.Add(item);
                    }

                    OnPropertyChanged("SearchList");
                }
            }
        }


		private void GetManifest()
		{
			if(!ManifestWorker.IsBusy)
			{
				ManifestWorker.RunWorkerAsync(SelectedItem);
			}
		}
		private void ManifestWork(object sender, DoWorkEventArgs e)
		{
			MetaData meta = (MetaData) e.Argument;
			if(meta == null)
			{
				SearchWorker.CancelAsync();
				return;
			}

			var result = Manifest.Run(Indexer.GetResource(Manifest.RequiredType).First().Id, meta._Id, meta.Version);
			e.Result = result;
		}
		private void ManifestWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			SelectedItemManifest = (ManifestResult)e.Result;
            SelectedVersion = SelectedItem.Versions.Last().Name;

            OnPropertyChanged("SelectedVersion");
            OnPropertyChanged("SelectedItemManifest");
			OnPropertyChanged("SelectedItem");
		}


		private void DownloadPackage(string id, string version)
		{
			if(!DownloadWorker.IsBusy)
			{
				DownloadWorker.RunWorkerAsync(new DownloadArgument(id, version));
			}
		}
		private void DownloadWork(object sender, DoWorkEventArgs e)
		{
			var argument = (DownloadArgument) e.Argument;

			var packageBinary = Download.Run(Indexer.GetResource(Download.RequiredType).First().Id, argument.Id, argument.Version);

			string packagePath = DownloadLocation + argument.Id + "\\" + argument.Version + "\\" + argument.Id + "." + argument.Version + ".nupkg";
			string decompressPath = DownloadLocation + argument.Id + "\\" + argument.Version + "\\";

			FileInfo fileInfo = new FileInfo(packagePath);
			if(!fileInfo.Exists)
			{
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			}

			var stream = fileInfo.Create();
			stream.Write(packageBinary, 0, packageBinary.Length);
			stream.Close();

			using (ZipArchive archive = ZipFile.OpenRead(packagePath))
			{
				var result = from currentEntry in archive.Entries
							 where currentEntry.FullName.StartsWith("lib/")
							 select currentEntry;

				foreach (ZipArchiveEntry entry in result)
				{
					string path = Path.Combine(decompressPath, entry.FullName);

					if (!Directory.Exists(Path.GetDirectoryName(path)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(path));
					}

					entry.ExtractToFile(path);
				}
			}

			e.Result = argument;
		}
		private void DownloadWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			var result = (DownloadArgument)e.Result;

			MessageBox.Show("File : " + result.Id + "." + result.Version + ".nupkg download complete");
		}

		private void ItemClicked(object param)
		{
			GetManifest();
		}
		
		private void LicenseClicked(object param)
		{
            System.Diagnostics.Process.Start(SelectedItemManifest.LicenseUrl);
		}

        private void GoToProjectClicked(object param)
        {
            System.Diagnostics.Process.Start(SelectedItemManifest.ProjectUrl);
        }

		private void ListScrolled(object param)
		{
			ScrollChangedEventArgs args = param as ScrollChangedEventArgs;
			
			ScrollViewer scroll = (ScrollViewer) args.OriginalSource;
			
			if(scroll.ScrollableHeight == scroll.VerticalOffset)
			{
				SearchOption.skip = SearchList.Count;
				SearchPackage(SearchOption);
			}
		}

		private void InputEnterDown(object param)
		{
			SearchList.Clear();
			 
			SearchOption.q = SearchKeyword;

            try
            {
                SearchPackage(SearchOption);
            }
            catch (JsonReaderException e)
            {
                MessageBox.Show("Can't read api list from selected server.");
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Invalid server url.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Unknown error in initialize indexer.");
            }
		}

		private void InstallClicked(object param)
		{
			string id = SelectedItemManifest.Id;
			string version = SelectedVersion;

			DownloadPackage(id, version);
		}
	}
}
