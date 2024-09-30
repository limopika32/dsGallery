using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IllustCollections()
        {
            Add(new IllustCollection("Sample1", "loremipsum", "/Resources/Illust/costco.png"));
            Add(new IllustCollection("Sample2", "loremipsum", "/Resources/Illust/costco.png"));
            Add(new IllustCollection("Sample3", "loremipsum", "/Resources/Illust/costco.png"));
            Add(new IllustCollection("Sample4", "loremipsum", "/Resources/Illust/costco.png"));
            Add(new IllustCollection("Sample5", "loremipsum", "/Resources/Illust/costco.png"));
        }
    }
}
