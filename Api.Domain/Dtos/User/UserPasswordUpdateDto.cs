using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserPasswordUpdateDto
    {
        public Guid? Id { get; set; }

        [Display(Name = "e-mail"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório"),
         EmailAddress(ErrorMessage = "O campo {0} em formato inválido"),
         MaxLength(100, ErrorMessage = "Número máximo de caractes {1} ")]
        public string Email { get; set; }

        [Display(Name = "senha do usuário"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório"),
         MinLength(6, ErrorMessage = "Quantidade mínima de  caracteres {1}")]
        public string Password { get; set; }
    }
}

