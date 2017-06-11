using Microsoft.WindowsAzure.MobileServices;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(Friend))]
    public class Friend : BaseEntity
    {
        public string IdUser { get; set; }
        public string IdUserFriend { get; set; }
    }
}
