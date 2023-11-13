using System.Reflection;
using AutoMapper;
using CleanArchitechture.Core.Interfaces.Common;

namespace CleanArchitechture.Core.Utilities;

public class MappingProfile : Profile
{
    private readonly string interfaceName = nameof(IMapFrom<object>);
    private readonly string methodName = nameof(IMapFrom<object>.Mapping);
    public MappingProfile()
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(c => c.FullName.StartsWith(nameof(CleanArchitechture))).ToArray();
        ApplyMappingsFromAssembly(assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly[] assemblies)
    {
        var types = new List<Type>();
        foreach (var assembly in assemblies)
        {
            types.AddRange(
                assembly.GetExportedTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                    .ToList());
        }

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(methodName)
                             ?? type.GetInterface($"{interfaceName}`1")?.GetMethod(methodName);

            methodInfo?.Invoke(instance, new object[] { this });

        }
    }
}