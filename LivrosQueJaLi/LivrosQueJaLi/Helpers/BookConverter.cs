using LivrosQueJaLi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.Helpers
{
    public class BookConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Book));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            Book book = jo.ToObject<Book>();

            book.Id = jo.SelectToken("id").ToString();
            book.Publisher = jo.SelectToken("publisher").ToString();
            book.Description = jo.SelectToken("description").ToString();
            book.Language = jo.SelectToken("language").ToString();
            //book.PublishedDate = (DateTime) jo.SelectToken("publishedDate");

            book.Title = jo.SelectToken("volumeInfo.title").ToString();
            book.SubTitle = jo.SelectToken("volumeInfo.subtitle").ToString();
            book.Authors = jo.SelectToken("volumeInfo.authors").ToObject<string[]>();
            book.PageCount = Convert.ToInt32(jo.SelectToken("volumeInfo.title").ToString());

            book.Thumbnail = jo.SelectToken("imageLinks.thumbnail").ToString();

            return book;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
