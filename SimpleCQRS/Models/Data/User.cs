using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCQRS.Models.Data
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 32, MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
