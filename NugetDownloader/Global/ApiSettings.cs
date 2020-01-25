using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NugetDownloader.Model;
using System.Collections.ObjectModel;

namespace NugetDownloader.Global
{
    static class ApiSettings
    {
        private static ObservableCollection<Source> defaultSources = new ObservableCollection<Source>();
        private static ObservableCollection<Source> userDefinedSources = new ObservableCollection<Source>();
        private static Source selectedSource = null;

        public static IEnumerable<Source> DefaultSources
        {
            get { return defaultSources; }
        }
        public static IEnumerable<Source> UserDefinedSources
        {
            get { return userDefinedSources; }
        }

        public static void AddDefaultSource(Source source)
        {
            defaultSources.Add(source);
        }

        public static void AddDefinedSource(Source source)
        {
            userDefinedSources.Add(source);
        }

        public static void SetSelectedSource(Source source)
        {
            selectedSource = source;
        }

        public static Source GetSelectedSource()
        {
            return selectedSource;
        }
    }
}
