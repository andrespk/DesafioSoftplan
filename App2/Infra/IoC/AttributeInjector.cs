using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using static App2.Infra.IoC.Attributes;

namespace App2.Infra.IoC
{
    public static class AttributeInjector
    {
        public static void AddInjectionByAttribute(this IServiceCollection services)
        {
            RegisterWithAttribute(ref services, typeof(InjectsSingletonAttribute));
            RegisterWithAttribute(ref services, typeof(InjectsTransientAttribute));
            RegisterWithAttribute(ref services, typeof(InjectsScopedAttribute));
        }

        private static void RegisterWithAttribute(ref IServiceCollection services, Type injectableAttribute)
        {
            var appAssemblies = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Concat(AppDomain.CurrentDomain.GetAssemblies())
                .Where(o => o.FullName.ToLower().Contains("app"))
                .Distinct();

            foreach (var assembly in appAssemblies)
            {
                var allTypes = assembly.GetTypes();

                var classesWithAttribute = allTypes.Where(t => t.IsClass && t.CustomAttributes.Any(a => a.AttributeType == injectableAttribute));

                var an = classesWithAttribute.ToArray();

                foreach (var implementationType in classesWithAttribute)
                {
                    var interfaceType = allTypes.FirstOrDefault(t => t.IsInterface && t.Name.Substring(1) == implementationType.Name);

                    if (interfaceType == null)
                    {
                        //Log.Fatal($"DI registration [{implementationType.Name}] is missing.");
                        throw new Exception($"Failed to resolve interface for class [{implementationType.Name}], unable to inject dependency.");
                    }

                    //Log.Information($"DI registration [{implementationType.Name}] OK");

                    if (injectableAttribute == typeof(InjectsTransientAttribute))
                    {
                        services.AddTransient(interfaceType, implementationType);
                    }

                    if (injectableAttribute == typeof(InjectsScopedAttribute))
                    {
                        services.AddScoped(interfaceType, implementationType);
                    }

                    if (injectableAttribute == typeof(InjectsSingletonAttribute))
                    {
                        services.AddSingleton(interfaceType, implementationType);
                    }
                }
            }
        }
    }
}