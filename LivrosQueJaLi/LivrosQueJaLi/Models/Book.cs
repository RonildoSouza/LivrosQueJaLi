using System;

namespace LivrosQueJaLi.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string[] Authors { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Thumbnail { get; set; }
        public string Language { get; set; }
    }
}
