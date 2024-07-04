namespace CommandLineGenerator.Utility;

/// <summary>
///     Extension methods for <see cref="string" />
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     String formatting wrapper around all built-in types with literal support
    ///     in C#, including strings, integers, floating point numbers, booleans, and characters.
    /// </summary>
    /// <param name="input">Input object to format</param>
    /// <returns>String representation formatted as a literal</returns>
    /// <exception cref="ArgumentException">
    ///     When the input supplied is not a built-in type with literal support
    /// </exception>
    public static string ToLiteral(this object input)
    {
        return input switch
        {
            string => $"\"{input}\"",
            char => $"\'{input}\'",
            float => $"{input}f",
            double => $"{input}d",
            int => $"{input}",
            uint => $"{input}U",
            long => $"{input}L",
            ulong => $"{input}UL",
            bool b => b.ToLiteral(),
            _ => throw new ArgumentException(
                "Cannot represent type that does not support literal notation as literal.")
        };
    }

    /// <summary>
    ///     String formatting wrapper around boolean using literal formatting.
    /// </summary>
    /// <param name="input">Input boolean to format</param>
    /// <returns>String boolean literal representation</returns>
    public static string ToLiteral(this bool input)
    {
        return $"{input.ToString().ToLower()}";
    }
}