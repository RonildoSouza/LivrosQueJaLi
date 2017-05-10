using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

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

        public async Task<string> GetBookForId(string pIdBook)
        {
            string responseString = null;
            //var url = $"{UrlGoogleBooksAPI}/{pIdBook}";
            var url = $"{UrlGoogleBooksAPI}?q=id:{pIdBook}&fields=items(id,volumeInfo/title," +
                $"volumeInfo/subtitle,volumeInfo/authors,volumeInfo/publisher,volumeInfo/publishedDate," +
                $"volumeInfo/description,volumeInfo/pageCount,volumeInfo/imageLinks/thumbnail,volumeInfo/language)";

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                        responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }

            return responseString;
        }

        public async Task<string> GetBooks(string pSearchTerm = "\"\"")
        {
            string responseString = null;
            var url = $"{UrlGoogleBooksAPI}?q={pSearchTerm}&fields=totalItems,items(id,volumeInfo/title," +
                $"volumeInfo/subtitle,volumeInfo/authors,volumeInfo/publisher,volumeInfo/publishedDate," +
                $"volumeInfo/description,volumeInfo/pageCount,volumeInfo/imageLinks/thumbnail,volumeInfo/language)" +
                $"&maxResults=40&startIndex=0";

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                        responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }       

            return responseString;
        }
    }
}        