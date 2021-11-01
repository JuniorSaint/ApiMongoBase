using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Email
{
    public class SendEmailDto
    {
        [Required(ErrorMessage = "Enviar para é obrigatório")]
        [RegularExpression(@"[a-z A-Z 0-9 _ \-\.]+[@]+[a-z]+[\.][a-z]{2,3}", ErrorMessage = "Campo com formato inválido")]
        public string SendTo { get; set; }

        [Required(ErrorMessage = "Assunto é campo obrigatório")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "O corpo da mensagem é obrigatório")]
        public string BodyEmail { get; set; }
    }
}

