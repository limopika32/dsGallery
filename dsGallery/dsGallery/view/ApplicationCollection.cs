using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsGallery.view
{
    public class ApplicationCollection
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageLocation { get; set; }
        public string Path { get; set; }

        public ApplicationCollection(string Title, string Detail, string ImageLocation, string Path)
        {
            this.Title = Title;
            this.Detail = Detail;
            this.ImageLocation = ImageLocation;
            this.Path = Path;
        }
    }

    public class ApplicationCollections : ObservableCollection<ApplicationCollection>
    {
        public ApplicationCollections()
        {
            Add(new ApplicationCollection("Sample1", "loremipsum1", "/Resources/Illust/costco.png", ""));
            Add(new ApplicationCollection("Sample2", "loremipsum2", "/Resources/Illust/costco.png", ""));
            Add(new ApplicationCollection("Sample3", "loremipsum3", "/Resources/Illust/costco.png", ""));
            Add(new ApplicationCollection("Sample4", "loremipsum4", "/Resources/Illust/costco.png", ""));
            Add(new ApplicationCollection("Sample5", "loremipsum5", "/Resources/Illust/costco.png", ""));
        }
    }
}

