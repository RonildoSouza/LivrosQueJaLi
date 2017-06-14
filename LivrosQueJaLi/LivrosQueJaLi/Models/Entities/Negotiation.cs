using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(Negotiation))]
    public class Negotiation : BaseEntity
    {
        public string IdUserBook { get; set; }
        public string IdUserInterested { get; set; }
        public string Message { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public string UserAndDate { get; set; }
    }
}
