using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsGallery.view
{
    public class MusicCollection
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageLocation { get; set; }
        public string Path { get; set; }

        public MusicCollection(string Title, string Detail, string ImageLocation, string Path)
        {
            this.Title = Title;
            this.Detail = Detail;
            this.ImageLocation = ImageLocation;
            this.Path = Path;
        }
    }

    public class MusicCollections : ObservableCollection<MusicCollection>
    {
        public MusicCollections()
        {
            Add(new MusicCollection("Sample6", "loremipsum1", "/Resources/Illust/costco.png", ""));
            Add(new MusicCollection("Sample7", "loremipsum2", "/Resources/Illust/costco.png", ""));
            Add(new MusicCollection("Sample8", "loremipsum3", "/Resources/Illust/costco.png", ""));
            Add(new MusicCollection("Sample9", "loremipsum4", "/Resources/Illust/costco.png", ""));
            Add(new MusicCollection("Sample10", "loremipsum5", "/Resources/Illust/costco.png", ""));
        }
    }
}

