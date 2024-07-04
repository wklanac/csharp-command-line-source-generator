namespace CommandLineGenerator.Writer;

/// <summary>
///     Interface representing common functionality of a source code writer.
/// </summary>
public interface ISourceWriter
{
    public void OpenBlock(string openingString);
    public void Indent();
    public void WriteLine(string line);
    public void Unindent();
    public void CloseBlock(string closingString);
}