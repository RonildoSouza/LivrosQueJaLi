﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(UserBook))]
    public class UserBook : BaseEntity
    {
        public string IdUser { get; set; }
        public string IdBook { get; set; }
        public int Read { get; set; }
        public int Wish { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}
