using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using LivrosQueJaLi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LivrosQueJaLi.Helpers;

namespace LivrosQueJaLi.Services
{
    public class GoogleBooksClient
    {
        private const string UrlGoogleBooksAPI = "https://www.googleapis.com/books/v1/volumes";
        private HttpClient _client;
        private Book _book = new Book();

        public GoogleBooksClient()
        {
            _client = new HttpClient();
        }

        public async Task<Book> GetBookForId(string pIdBook)
        {
            var url = $"{UrlGoogleBooksAPI}/{pIdBook}";
            //var url = $"{UrlGoogleBooksAPI}?q=id:{pIdBook}&fields=items(id,volumeInfo/title," +
            //    $"volumeInfo/subtitle,volumeInfo/authors,volumeInfo/publisher,volumeInfo/publishedDate," +
            //    $"volumeInfo/description,volumeInfo/pageCount,volumeInfo/imageLinks/thumbnail,volumeInfo/language)";

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        _book = JsonConvert.DeserializeObject<Book>(responseString, new BookConverter());
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }

            return _book;
        }

        public async Task<List<Book>> GetBooks(string pSearchTerm = "\"\"")
        {
            var url = $"{UrlGoogleBooksAPI}?q={pSearchTerm}&fields=totalItems,items(id,volumeInfo/title," +
                $"volumeInfo/subtitle,volumeInfo/authors,volumeInfo/publisher,volumeInfo/publishedDate," +
                $"volumeInfo/description,volumeInfo/pageCount,volumeInfo/imageLinks/thumbnail,volumeInfo/language)" +
                $"&maxResults=40&startIndex=0";

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }
    }
}