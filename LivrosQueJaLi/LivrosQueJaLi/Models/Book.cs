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
        public ImageLinks ImageLinks { get; set; }
        public string Language { get; set; }
    }

    public class ImageLinks
    {
        public string Thumbnail { get; set; }
        public string Small { get; set; }
    }   
}
