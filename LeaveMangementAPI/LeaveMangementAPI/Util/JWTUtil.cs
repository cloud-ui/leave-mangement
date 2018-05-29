using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Util
{
    public class JWTUtil
    {

        public async Task<string> GetMessageByToken(HttpContext context)
        {
            string account = "";            
            var schemeProvider = context.RequestServices.GetService(typeof(IAuthenticationSchemeProvider)) as IAuthenticationSchemeProvider;
            var defaultAuthenticate = await schemeProvider.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                var user = result?.Principal;
                if (user != null)
                {
                    account = user.Identity.Name;
                }
            }
            return account;
        }


        public string GetJwt(string account, IConfiguration configuration)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                          ClaimValueTypes.Integer64),
                //用户名
                new Claim(ClaimTypes.Name,account),
                //角色
                new Claim(ClaimTypes.Role,"a")
            };

            var audienceConfig = configuration.GetSection("TokenAuthentication:Audience").Value;
            var symmetricKeyAsBase64 = configuration.GetSection("TokenAuthentication:SecretKey").Value;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var jwt = new JwtSecurityToken(
                issuer: audienceConfig,
                audience: audienceConfig,
                claims: claims,
                notBefore: now,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var token_bearer = "Bearer " + encodedJwt;
            var response = new
            {
                IsSuccess = true,
                Data = new
                {
                    token = token_bearer,
                    expiration = jwt.ValidTo
                }
            };
            //return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return token_bearer;
        }


    }
}
