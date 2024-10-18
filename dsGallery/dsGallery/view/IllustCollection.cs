using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Data.Json;
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
                var dict = new Dictionary<string, string[]>();

                try
                {
                    JsonValue data;
                    bool result = JsonValue.TryParse(
                    await FileIO.ReadTextAsync(
                            await mill.GetFileAsync("iview.json")
                        ), out data
                    );

                    if (result)
                    {
                        JsonArray _contents = data.GetObject().GetNamedArray("contents");

                        foreach (JsonValue item in _contents)
                        {
                            dict.Add(
                                mill.Path + item.GetObject().GetNamedString("path").Replace("/", "\\"),
                                [
                                  item.GetObject().GetNamedString("author"),
                                  item.GetObject().GetNamedString("title"),
                                  item.GetObject().GetNamedString("description")
                                ]
                            );
                        }
                    }
                }
                catch (Exception) { }


                foreach (StorageFile file in m_illusts)
                {
                    if (file.Name == "iview.json" ) continue;

                    if (dict.ContainsKey(file.Path))
                    {
                        Add(new IllustCollection(
                            dict[file.Path][1],
                            dict[file.Path][2] + "\n制作者: " + dict[file.Path][0],
                            file.Path
                            ));
                    }
                    else
                    {
                        Add(new IllustCollection(file.DisplayName, "", file.Path));
                    }
                }
            }
        }
    }
}
