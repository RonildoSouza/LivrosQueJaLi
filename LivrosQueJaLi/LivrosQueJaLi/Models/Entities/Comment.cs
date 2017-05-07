using Microsoft.WindowsAzure.MobileServices;
using System;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(Comment))]
    public class Comment : BaseEntity
    {
        public string UserComment { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
