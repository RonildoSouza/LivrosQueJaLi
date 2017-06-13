using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LivrosQueJaLi.Models.Entities
{
    [DataTable(nameof(User))]
    public class User : BaseEntity
    {
        public string IdFacebook { get; set; }
        public string UserName { get; set; }

        private string _photo;
        public string Photo
        {
            get
            {
                if (string.IsNullOrEmpty(_photo))
                    return "noimage_100_100.png";

                return _photo;
            }
            set { _photo = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email?.ToLower(); }
            set { _email = value?.ToLower(); }
        }
        public string Password { get; set; }

        [JsonIgnore]
        public List<UserBook> UserBooks { get; set; }
    }
}
