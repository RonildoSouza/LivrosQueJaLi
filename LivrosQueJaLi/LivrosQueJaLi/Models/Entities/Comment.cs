using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(Comment))]
    public class Comment : BaseEntity
    {
        public string IdBook { get; set; }
        public string UserComment { get; set; }

        [JsonProperty("Comment")]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public string UserAndDate { get { return $"{UserComment} - {CreatedAt}"; } }
    }
}
