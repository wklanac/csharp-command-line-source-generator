using System.Runtime.Serialization;
using System.Text;

namespace CommandLineGenerator.Writer;

/// <summary>
///     Default source writer implementation.
/// </summary>
/// <param name="defaultSourceWriterSettings">Settings to use for writing</param>
public class DefaultSourceWriter(DefaultSourceWriterSettings defaultSourceWriterSettings) : ISourceWriter
{
    private readonly string indentationPrefix = CreateIndentationPrefix(defaultSourceWriterSettings);
    private readonly StringBuilder sourceStringBuilder = new();
    private int currentIndentationLevel;

    public void OpenBlock(string openingString)
    {
        var openingStringLine = openingString +
                                (defaultSourceWriterSettings.NewlineBeforeBlock ? "" : '{');
        WriteLine(openingStringLine);

        if (defaultSourceWriterSettings.NewlineBeforeBlock) WriteLine('{'.ToString());

        Indent();
    }

    public void Indent()
    {
        currentIndentationLevel += 1;
    }

    public void WriteLine(string line)
    {
        var linePrefix = GetLinePrefix();
        sourceStringBuilder.AppendLine(linePrefix + line);
    }

    public void Unindent()
    {
        if (currentIndentationLevel == 0)
            throw new InvalidOperationException("Cannot unindent when the current indentation value is zero valued.");

        currentIndentationLevel -= 1;
    }

    public void CloseBlock(string closingString)
    {
        if (currentIndentationLevel == 0)
            throw new InvalidOperationException("Cannot close block, mismatch between blocks opened and closed found");

        WriteLine(closingString);
        Unindent();
        WriteLine("}");
    }

    private string GetLinePrefix()
    {
        return string.Concat(
            Enumerable.Repeat(
                indentationPrefix,
                currentIndentationLevel
            )
        );
    }

    private static string CreateIndentationPrefix(DefaultSourceWriterSettings defaultSourceWriterSettings)
    {
        return defaultSourceWriterSettings.UseSpacesForTabs
            ? new string(' ', defaultSourceWriterSettings.SpacesPerTab.Value)
            : '\t'.ToString();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return sourceStringBuilder.ToString();
    }
}