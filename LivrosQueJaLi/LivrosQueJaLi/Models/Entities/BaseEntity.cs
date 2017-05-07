using Microsoft.WindowsAzure.MobileServices;
using System;

namespace LivrosQueJaLi.Models.Entities
{
    public class BaseEntity
    {              
        public string Id { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
