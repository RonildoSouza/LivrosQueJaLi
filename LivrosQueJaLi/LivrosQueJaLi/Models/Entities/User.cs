using Microsoft.WindowsAzure.MobileServices;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(User))]
    public class User : BaseEntity
    {
        public string IdFacebook { get; set; }
        public string Name { get; set; }
    }
}
