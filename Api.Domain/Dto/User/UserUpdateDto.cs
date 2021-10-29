using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Campo Id é obrigatório")]
        public Guid Id { get; set; }

        [Display(Name = "Nome do usuário"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório")]
        [MaxLength(90, ErrorMessage = "Número máximo de caractes {1} ")]
        public string UserName { get; set; }

        [Display(Name = "e-mail"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório"),
         EmailAddress(ErrorMessage = "O campo {0} em formato inválido"),
         MaxLength(100, ErrorMessage = "Número máximo de caractes {1} ")]
        public string Email { get; set; }

        [Display(Name = "senha do usuário"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório"),
         MinLength(6, ErrorMessage = "Quantidade mínima de  caracteres {1}")]
        public string Password { get; set; }

        [Display(Name = "usuário ativo"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório")]
        public string IsActive { get; set; }

        [Display(Name = "Tipo de usuário"),
         Required(ErrorMessage = "O campo {0} é campo obrigatório")]
        public string UserType { get; set; }
    }
}

