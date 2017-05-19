using System;
using Newtonsoft.Json;

namespace LivrosQueJaLi.Models
{
    public class Book
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Authors { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public int? PageCount { get; set; }

        ReadingModes _readingModes;
        public ReadingModes ReadingModes
        {
            get { return _readingModes; }
            set
            {
                if (value != null && !value.Image)
                {
                    ImageLinks = new ImageLinks();
                    _readingModes = value;
                }
            }
        }

        public ImageLinks ImageLinks { get; set; }
        public string Language { get; set; }
    }

    public class ReadingModes
    {
        public bool Text { get; set; }
        public bool Image { get; set; }
    }

    public class ImageLinks
    {
        string _thumbnail;
        public string Thumbnail
        {
            get
            {
                if (string.IsNullOrEmpty(_thumbnail))
                    return "noimage_128_183.png";

                return _thumbnail;
            }
            set { _thumbnail = value; }
        }

        string _small;
        public string Small
        {
            get
            {
                if (string.IsNullOrEmpty(_small))
                    return "noimage_300_418.png";

                return _small;
            }
            set { _small = value; }
        }
    }
}
