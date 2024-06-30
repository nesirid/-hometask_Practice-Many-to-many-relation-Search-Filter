using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using Service.DTOs.Admin.Groups;
using Service.Helpers;
using Service.Services.Interface;
using Service.Services;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<ITeacherService, TeacherService>();

            services.AddScoped<IValidator<GroupCreateDto>, GroupCreateDtoValidator>();
            return services;
        }

    }
}
