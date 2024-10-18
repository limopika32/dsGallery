using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Search;

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
                var dict = new Dictionary<string, string[]>();

                try
                {
                    JsonValue data;
                    bool result = JsonValue.TryParse(
                    await FileIO.ReadTextAsync(
                            await mmus.GetFileAsync("mview.json")
                        ), out data
                    );

                    if (result)
                    {
                        JsonArray _contents = data.GetObject().GetNamedArray("contents");

                        foreach (JsonValue item in _contents)
                        {
                            dict.Add(
                                mmus.Path + item.GetObject().GetNamedString("path").Replace("/", "\\"),
                                [
                                  item.GetObject().GetNamedString("author"),
                                  item.GetObject().GetNamedString("title"),
                                  item.GetObject().GetNamedString("jacket"),
                                  item.GetObject().GetNamedString("description")
                                ]
                            );
                        }
                    }
                }
                catch (Exception) { }

                foreach (StorageFile file in m_musics)
                {
                    if (file.Name == "mview.json") continue;

                    if (dict.ContainsKey(file.Path))
                    {
                        Add(new MusicCollection(
                            dict[file.Path][1],
                            dict[file.Path][3] + "\n制作者: " + dict[file.Path][0],
                            dict[file.Path][2].Equals("") ? "/Resources/nodisc.png" : mmus.Path + dict[file.Path][2],
                            file
                            ));
                    }
                    else
                    {
                        Add(new MusicCollection(file.DisplayName, "", "/Resources/nodisc.png", file));
                    }
                }
            }
        }
    }
}

