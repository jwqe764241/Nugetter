using Runner.Indexer;
using Runner.Search;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Input;

using NugetDownloader.Utils;

/*
* 1. 한번에 붙여넣기 안됨?
* 2. icon 없을경우 어케함?
* 3. 버전은 맨 끝으로 위치하게 변경
* 4. 스크롤 바닥에서 하면 20개 계속 추가
*/

namespace NugetDownloader.ViewModel
{
    class PackageViewModel : INotifyPropertyChanged
	{
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
		
        public Indexer Indexer { get; set; }
		private BackgroundWorker SearchWorker { get; set; }


        public PackageViewModel()
        {
            Indexer = new Indexer("https://api.nuget.org/v3/index.json");
            searchList = new ObservableCollection<MetaData>();
            
            SearchWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            SearchWorker.DoWork += SearchWork;
            SearchWorker.RunWorkerCompleted += SearchWorkCompleted;
            SearchPackage("");
		}

        public void SearchPackage(string query)
        {
            if (!SearchWorker.IsBusy)

            {
                SearchWorker.RunWorkerAsync(query);
            }
        }
    
        private void SearchWork(object sender, DoWorkEventArgs e)
        {
            string query = (string)e.Argument;

            SearchOption options = new SearchOption()
            {
                q = query,
                take = 20
            };

            var result = Search.Run(Indexer.GetResource(Search.RequiredType).ElementAt(0).Id, options);
            e.Result = result;
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
                
                foreach(var item in result.Data)
                    SearchList.Add(item);

                OnPropertyChanged("SearchList");
            }
        }

		private void OnScrolled(object value)
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
