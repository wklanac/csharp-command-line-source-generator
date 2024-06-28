namespace CommandLineGenerator.Knowledge.Reflection;

public interface ITypeMetadataTemplater<T>
{
    public string GetTemplate(ITypeMetadataProvider<T> typeMetadataProviderProvider, Type type);
}