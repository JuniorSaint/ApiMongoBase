using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntities
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(90)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string IsActive { get; set; }

        [Required]
        public string UserType { get; set; }
    }
}

