using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Email;

namespace Api.Domain.Interfaces.Services
{
    public interface ISendEmailSerivce
    {
        Task SendMail(SendEmailDto sendEmail);
    }
}

