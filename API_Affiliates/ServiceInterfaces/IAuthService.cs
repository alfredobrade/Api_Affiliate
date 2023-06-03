using API_Affiliates.Models.Auth;

namespace API_Affiliates.ServiceInterfaces
{
    public interface IAuthService
    {

        Task<AuthResponse> GetToken(AuthRequest request);
    }
}
