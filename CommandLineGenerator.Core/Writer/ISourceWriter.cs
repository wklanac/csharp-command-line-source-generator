namespace CommandLineGenerator.SourceWriter;

public interface ISourceWriter
{
    public void OpenBlock(String openingString);
    public void Indent();
    public void WriteLine(String line);
    public void Unindent();
    public void CloseBlock(String closingString);
}