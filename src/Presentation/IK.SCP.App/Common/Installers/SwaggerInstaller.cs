using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IK.SCP.App.Common.Installers
{
    public static class SwaggerInstaller
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer JWT-Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                c.CustomSchemaIds(i => i.FullName);

                c.OperationFilter<TagByApiExplorerSettingsOperationFilter>();


            });

            return services;
        }

        public class TagByApiExplorerSettingsOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var apiExplorerSettings = controllerActionDescriptor
                        .ControllerTypeInfo.GetCustomAttributes(typeof(ApiExplorerSettingsAttribute), true)
                        .Cast<ApiExplorerSettingsAttribute>().FirstOrDefault();
                    if (apiExplorerSettings != null && !string.IsNullOrWhiteSpace(apiExplorerSettings.GroupName))
                    {
                        operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = apiExplorerSettings.GroupName } };
                    }
                    else
                    {
                        operation.Tags = new List<OpenApiTag>
                    {new OpenApiTag {Name = controllerActionDescriptor.ControllerName}};
                    }
                }
            }
        }
    }
}
