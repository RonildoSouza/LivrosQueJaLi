using System;
using Newtonsoft.Json;

namespace LivrosQueJaLi.Models
{
    public class Book
    {
        #region items 
        public string Id { get; set; }

        #region volumeInfo 
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string[] Authors { get; set; }
        #endregion volumeInfo                  

        public string Publisher { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Description { get; set; }
        public int? PageCount { get; set; }

        #region imageLinks       
        public string Thumbnail { get; set; }
        #endregion imageLinks

        public string Language { get; set; }
        #endregion items 
    }
}
