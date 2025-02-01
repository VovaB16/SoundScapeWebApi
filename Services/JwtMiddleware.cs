using Microsoft.IdentityModel.Tokens;
using SoundScape.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, ApplicationDbContext dbContext)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachUserToContext(context, dbContext, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, ApplicationDbContext dbContext, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
            var email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;

            var user = dbContext.Users.Find(userId);
            if (user != null && user.Email == email)
            {
                context.Items["User"] = user;
                context.Items["Email"] = email;
            }
        }
        catch (SecurityTokenException)
        {
            // do nothing if jwt validation fails
        }
    }
}
