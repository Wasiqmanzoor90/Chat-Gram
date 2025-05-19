using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Server.Application.Service
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Configure cookie extraction
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // First try to get the token from the cookie
                        var tokenFromCookie = context.Request.Cookies["token"];

                        if (!string.IsNullOrEmpty(tokenFromCookie))
                        {
                            // Token found in cookie
                            context.Token = tokenFromCookie;
                            return Task.CompletedTask;
                        }

                        // If no cookie, check Authorization header as fallback
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                        {
                            context.Token = authHeader.Substring("Bearer ".Length).Trim();
                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        // Log successful validation
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var email = context.Principal?.FindFirstValue(ClaimTypes.Email);
                        logger.LogInformation("Token validated for user: {Email}", email);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        // Log failed validation
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogWarning("Authentication failed: {Error}", context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "InstagramClone",
                    ValidAudience = "InstagramCloneUser",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"] ??
                            throw new InvalidOperationException("JWT:SecretKey is not configured"))
                    ),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            services.AddAuthorization();
            return services;
        }
    }
}