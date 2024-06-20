namespace CommandLineGenerator.ComponentGeneration;

/// <summary>
/// Interface for generic component generation.
/// Implementations could generate source code, syntax tree models, or other component models.
/// </summary>
/// <typeparam name="T">Component to generate.</typeparam>
public interface IComponentGenerator<in T>
{
    public void GenerateAndAdd(T entity);
}