using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using HR.LeaveManagmant.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace HR.LeaveManagmant.Persistence.Extensions;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<HrDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository,LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<IRefreshToken, SaveRefreshtoken>();

       var redisConnectionString = configuration.GetSection("Redis:RedisConfig").Value;
         services.AddSingleton<IConnectionMultiplexer>(sp =>
         {
            var configuration = ConfigurationOptions.Parse(redisConnectionString!);
            configuration.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(configuration);
         });

        var key = Encoding.ASCII.GetBytes(configuration["JWT_SECRET_KEY"]!);
        var tokenValidationParameter = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(key),
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           RequireExpirationTime = false,
       };

        services.AddSingleton(tokenValidationParameter);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = tokenValidationParameter;
            
        });

            
        return services;
    }
}
