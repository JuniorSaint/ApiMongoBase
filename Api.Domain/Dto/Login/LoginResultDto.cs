using System;
namespace Api.Domain.Dtos.Login
{
    public class LoginResultDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }
    }
}
