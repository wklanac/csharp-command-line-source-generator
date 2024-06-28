using System.Reflection;

namespace CommandLineGenerator.Knowledge.Reflection;

public interface IConstructorResolver
{
    public ConstructorInfo Get(IEnumerable<ConstructorInfo> constructorInfo);
}