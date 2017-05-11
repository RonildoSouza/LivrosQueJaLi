using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using LivrosQueJaLi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LivrosQueJaLi.Services
{
    public class GoogleBooksClient
    {
        private const string UrlGoogleBooksAPI = "https://www.googleapis.com/books/v1/volumes";
        private HttpClient _client;

        public GoogleBooksClient()
        {
            _client = new HttpClient();
        }

        public async Task<Book> GetBookForId(string pIdBook)
        {
            Book book = null;
            var url = $"{UrlGoogleBooksAPI}/{pIdBook}";

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        book = new Book();
                        var responseString = await response.Content.ReadAsStringAsync();
                        book = JsonConvert.DeserializeObject<Book>(responseString);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }

            return book;
        }

        public async Task<List<Book>> GetBooks(string pSearchTerm = "\"\"", int pStartIndex = 0)
        {
            List<Book> books = null;
            var url = $"{UrlGoogleBooksAPI}?q={pSearchTerm}&fields=items(id,volumeInfo/title," +
                $"volumeInfo/subtitle,volumeInfo/authors,volumeInfo/publisher,volumeInfo/publishedDate," +
                $"volumeInfo/description,volumeInfo/pageCount,volumeInfo/imageLinks/thumbnail,volumeInfo/language)" +
                $"&maxResults=40&startIndex={pStartIndex}";

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        books = new List<Book>();
                        var responseString = await response.Content.ReadAsStringAsync();
                        var array = JObject.Parse(responseString).SelectToken("items").ToArray();

                        for (int i = 0; i < array.Length; i++)
                            books.Add(JsonConvert.DeserializeObject<Book>(array[i].ToString()));
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }

            return books;
        }
    }
}