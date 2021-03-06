using System;

namespace Api.Domain.Dtos.User
{
    public class UserUpdateResultDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string UserType { get; set; }
    }
}
