using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Search;
using Windows.Storage;

namespace dsGallery.view
{
    public class MusicCollection
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageLocation { get; set; }
        public StorageFile Path { get; set; }

        public MusicCollection(string Title, string Detail, string ImageLocation, StorageFile Path)
        {
            this.Title = Title;
            this.Detail = Detail;
            this.ImageLocation = ImageLocation;
            this.Path = Path;
        }
    }

    public class MusicCollections : ObservableCollection<MusicCollection>
    {
        readonly StorageFolder mmus = MainWindow.mmus;

        public MusicCollections()
        {
            MusicReaderAsync();
        }

        async void MusicReaderAsync()
        {
            if (mmus is not null)
            {
                IReadOnlyList<StorageFile> m_musics = await mmus.GetFilesAsync(CommonFileQuery.DefaultQuery);

                foreach (StorageFile file in m_musics)
                {
                    Add(new MusicCollection(file.DisplayName, "(詳細情報なし)", "/Resources/nodisc.png", file));
                }
            }
        }
    }
}

