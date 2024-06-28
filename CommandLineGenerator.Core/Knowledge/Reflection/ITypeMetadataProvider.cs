namespace CommandLineGenerator.Knowledge.Reflection;

public interface ITypeMetadataProvider<T>
{
    public T Get(Type type);
}