using CommandLineGenerator.SourceWriter;

namespace CommandLineGenerator.Testing.Writer;

public class DefaultSourceWriterTest
{
    [TestCase(true, 4, true,
        ExpectedResult = """
                         BlockStart()
                         {
                         
                         """)]
    [TestCase(true, 4, false,
        ExpectedResult = """
                         BlockStart(){
                         
                         """)]
    public string TestOpenBlock(bool useSpacesForTabs, int? spacesPerTab, bool newlineBeforeBlock)
    {
        var defaultSourceWriter = new DefaultSourceWriter(
            new DefaultSourceWriterSettings(useSpacesForTabs, spacesPerTab, newlineBeforeBlock));
        
        defaultSourceWriter.OpenBlock("BlockStart()");
        return defaultSourceWriter.ToString();
    }

    [TestCase(true, 3, ExpectedResult = "   \n")]
    [TestCase(false, null, ExpectedResult = "\t\n")]
    public string TestIndent(bool useSpacesForTabs, int? spacesPerTab)
    {
        var defaultSourceWriter = new DefaultSourceWriter(new DefaultSourceWriterSettings(
            UseSpacesForTabs: useSpacesForTabs,
            SpacesPerTab: spacesPerTab));
        
        defaultSourceWriter.Indent();
        defaultSourceWriter.WriteLine("");

        return defaultSourceWriter.ToString();
    }

    [Test]
    public void TestUnindent()
    {
        var defaultSourceWriter = new DefaultSourceWriter(new DefaultSourceWriterSettings(
            UseSpacesForTabs: false, NewlineBeforeBlock: true));
        defaultSourceWriter.Indent();
        defaultSourceWriter.Indent();
        defaultSourceWriter.Unindent();
        defaultSourceWriter.WriteLine("");
        
        Assert.That(defaultSourceWriter.ToString(), Is.EqualTo("\t\n"));
    }

    [Test]
    public void TestUnindentWhenInvalid()
    {
        var defaultSourceWriter = new DefaultSourceWriter(new DefaultSourceWriterSettings());

        Assert.Throws(typeof(InvalidOperationException), () => defaultSourceWriter.Unindent());
    }

    [Test]
    public void TestCloseBlock()
    {
        var defaultSourceWriter = new DefaultSourceWriter(new DefaultSourceWriterSettings(
            UseSpacesForTabs: false, NewlineBeforeBlock: true));
        defaultSourceWriter.OpenBlock("");
        defaultSourceWriter.CloseBlock("");

        Assert.That(defaultSourceWriter.ToString(), Is.EqualTo("\n{\n\t\n}\n"));
    }
    
    [Test]
    public void TestCloseBlockWhenInvalid()
    {
        var defaultSourceWriter = new DefaultSourceWriter(new DefaultSourceWriterSettings());

        Assert.Throws(typeof(InvalidOperationException), () => defaultSourceWriter.CloseBlock(""));
    }
}