using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Search;

namespace dsGallery.view
{
    public class ApplicationCollection
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageLocation { get; set; }
        public string Path { get; set; }
        public List<String> thumbnails { get; set; }


        public ApplicationCollection(string Title, string Detail, string ImageLocation, List<String> thumbnails , string Path)
        {
            this.Title = Title;
            this.Detail = Detail;
            this.ImageLocation = ImageLocation;
            this.thumbnails = thumbnails;
            this.Path = Path;
        }
    }

    public class ApplicationCollections : ObservableCollection<ApplicationCollection>
    {
        readonly StorageFolder mapp = MainWindow.mapp;

        public ApplicationCollections()
        {
            Clear();

            ApplicationReaderAsync();
        }

        async void ApplicationReaderAsync()
        {
            if (mapp is not null)
            {
                IReadOnlyList<StorageFolder> m_applis = await mapp.GetFoldersAsync(CommonFolderQuery.DefaultQuery);

                foreach (StorageFolder folder in m_applis)
                {
                    try
                    {
                        JsonValue data;
                        bool result = JsonValue.TryParse(
                            await FileIO.ReadTextAsync(
                                await folder.GetFileAsync("view.json")
                            ), out data
                        );

                        if (result)
                        {
                            Debug.WriteLine(folder.Path);

                            string author = data.GetObject().GetNamedString("author");
                            string title = data.GetObject().GetNamedString("title");
                            string icon = data.GetObject().GetNamedString("icon");
                            JsonArray _thumbnails = data.GetObject().GetNamedArray("thumbnails");
                            string execPath = data.GetObject().GetNamedString("execPath");
                            string description = data.GetObject().GetNamedString("description");

                            icon = icon.Equals("") ? "/Resources/Illust/costco.png" : folder.Path + icon;

                            List<String> thumbnails = new List<string>();

                            foreach (var item in _thumbnails)
                            {
                                thumbnails.Add(folder.Path + item.GetString());
                            }

                            Add(new ApplicationCollection(
                                title,
                                description + "\n\n" + "制作者: " + author,
                                icon,
                                thumbnails,
                                execPath)
                                );
                        }
                    }
                    catch (FileNotFoundException e) { }

                }
            }
        }
    }
}

