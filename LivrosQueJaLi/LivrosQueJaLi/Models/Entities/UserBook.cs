using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(UserBook))]
    public class UserBook : BaseEntity
    {
        public string IdUser { get; set; }
        public string IdBook { get; set; }
        public bool IsRead { get; set; }
        public bool IsWish { get; set; }
        public bool Lent { get; set; }
        public bool Borrowed { get; set; }
        public bool Seeling { get; set; }
        public bool Sold { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}
