﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(Comment))]
    public class Comment : BaseEntity
    {
        public string IdBook { get; set; }
        public string IdUserComment { get; set; }
        public string CommentText { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
