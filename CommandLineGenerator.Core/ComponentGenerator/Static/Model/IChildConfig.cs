namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Interface denoting a child configuration in a hierarchical structure.
///     Child configurations are expected to know how to create a relationship with their parent.
/// </summary>
/// <typeparam name="T">Parent type</typeparam>
public interface IChildConfig<in T> where T : ICommandLineConfig
{
    public ICommandLineConfigRelationship GetParentRelationship(T parent);
}