using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

using NugetDownloader.Utils;

using Runner.Indexer;
using Runner.Search;

namespace NugetDownloader.ViewModel
{
    class PackageViewModel : INotifyPropertyChanged
	{
        private List<MetaData> searchList;
        public List<MetaData> SearchList
        {
            get { return searchList; }
            set
            {
                searchList = value;
                OnPropertyChanged("SearchList");
            }
        }

        private Indexer indexer;
        public Indexer Indexer
        {
            get { return indexer; }
            set { indexer = value; }
        }

        public PackageViewModel()
        {
            indexer = new Indexer("https://api.nuget.org/v3/index.json");
            searchList = new List<MetaData>();

            SearchPackage("");
        }

        public void SearchPackage(string query)
        {
            SearchOption options = new SearchOption()
            {
                q = query,
                take = 20
            };

            var result = Search.Run(Indexer.GetResource(Search.RequiredType).ElementAt(0).Id, options);
            SearchList.AddRange(result.Data);

            OnPropertyChanged("SearchList");
        }
    
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
