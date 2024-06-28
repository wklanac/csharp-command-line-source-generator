using CommandLineGenerator.Knowledge.Reflection.Model;

namespace CommandLineGenerator.Knowledge.Reflection.Provider;

public class CreationalMetadataProvider: ITypeMetadataProvider<CreationalMetadata>
{
    public CreationalMetadata Get(Type type)
    {
        var constructors = type.GetConstructors();
        var writeableProperties = type.GetProperties().Where(p => p.CanWrite);

        return new CreationalMetadata(writeableProperties, constructors);
    }
}