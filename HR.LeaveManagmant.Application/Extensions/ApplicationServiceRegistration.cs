using AutoMapper;
using FluentValidation;
using HR.LeaveManagmant.Application.MappingProfile;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManagmant.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        var automapper = new MapperConfiguration(item => item.AddProfile(new LeaveTypeProfile()));
        IMapper mapper = automapper.CreateMapper();
        services.AddSingleton(mapper);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        return services;
    }
}
