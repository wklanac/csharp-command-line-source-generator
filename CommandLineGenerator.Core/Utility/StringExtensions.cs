namespace CommandLineGenerator.Extensions;

public static class StringExtensions
{
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
            _ => throw new ArgumentException("Cannot represent types that do not support literal notation to literals.")
        };
    }
    
    public static string ToLiteral(this bool input)
    {
        return $"{input.ToString().ToLower()}";
    }

}