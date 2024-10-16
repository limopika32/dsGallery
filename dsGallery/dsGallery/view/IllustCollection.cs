using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace dsGallery.view
{
    public class IllustCollection
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageLocation { get; set; }

        public IllustCollection(string Title, string Detail, string ImageLocation)
        {
            this.Title = Title;
            this.Detail = Detail;
            this.ImageLocation = ImageLocation;
        }
    }

    public class IllustCollections : ObservableCollection<IllustCollection>
    {
        readonly StorageFolder mill = MainWindow.mill;

        public IllustCollections()
        {
            IllustReaderAsync();
        }

        async void IllustReaderAsync()
        {
            if (mill is not null)
            {
                IReadOnlyList<StorageFile> m_illusts = await mill.GetFilesAsync(CommonFileQuery.DefaultQuery);

                foreach (StorageFile file in m_illusts)
                {
                    Add(new IllustCollection(file.DisplayName, "from SD card", file.Path));
                }
            }
        }
    }
}
