using IK.SCP.Domain.Dtos;

namespace IK.SCP.Application.ViewModels
{
    public class LoginResponse
    {
        public bool Ok { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
