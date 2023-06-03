using API_Affiliates.Models.Auth;
using API_Affiliates.Repository;
using API_Affiliates.ServiceInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Affiliates.Services
{
    public class AuthService : IAuthService
    {
        private readonly ProjectDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            
        }
        private string GenerateToken(string idUser)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));

            var tokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes), //aca le puse la variable para no repetir codigo
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = tokenCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            //string tokenCreated = tokenHandler.WriteToken(tokenConfig);
            return tokenHandler.WriteToken(tokenConfig);
        }
        public async Task<AuthResponse> GetToken(AuthRequest request)
        {
            var user = _context.Users.FirstOrDefault(x =>
                x.UserName == request.UserName &&
                x.Password == request.Password
            );

            if (user == null) return await Task.FromResult<AuthResponse>(null);

            string token = GenerateToken(user.Id.ToString());

            return new AuthResponse() { Token = token, Success = true, Message = "Ok" };

        }
    }
}
