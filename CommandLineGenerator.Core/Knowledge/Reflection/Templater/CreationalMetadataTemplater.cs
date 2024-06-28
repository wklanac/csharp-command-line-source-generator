using System.Reflection;
using System.Text;
using CommandLineGenerator.Caching;
using CommandLineGenerator.Knowledge.Reflection.Model;

namespace CommandLineGenerator.Knowledge.Reflection.Templater;

public class CreationalMetadataTemplater(IConstructorResolver constructorResolver)
    : ITypeMetadataTemplater<CreationalMetadata>
{
    private readonly ConcurrentCache<Type, CreationalMetadata> creationalMetadataCache = new();
    private readonly ConcurrentCache<IEnumerable<ConstructorInfo>, ConstructorInfo> constructorResolverCache = new();
        
    
    public string GetTemplate(ITypeMetadataProvider<CreationalMetadata> typeMetadataProviderProvider, Type type)
    {
        var typeMetadata = creationalMetadataCache.Get(type, typeMetadataProviderProvider.Get);
        var constructorToUse = constructorResolverCache.Get(
            typeMetadata.Constructors,
            constructorResolver.Get);
        
        StringBuilder templateBuilder = new StringBuilder();

        templateBuilder.Append($"var {{name}}{type.Name} = new {type.Name}(");

        var parameters = constructorToUse.GetParameters();
        
        if (parameters.Length > 0)
        {
            templateBuilder.AppendLine();
            
            for (var parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                var parameter = parameters[parameterIndex];
                var finalParameter = parameterIndex == parameters.Length - 1;
                var trailingCharacter = finalParameter ? "" : ",";
                templateBuilder.AppendLine($"    {parameter.Name}: {{{parameter.Name}}}{trailingCharacter}");
            }
        }
        
        templateBuilder.AppendLine(");");

        foreach (var writeableProperty in typeMetadata.WriteableProperties)
        {
            templateBuilder.AppendLine($"{{name}}{type.Name}.{writeableProperty.Name} = {{{writeableProperty.Name}}};");
        }

        return templateBuilder.ToString();
    }
}