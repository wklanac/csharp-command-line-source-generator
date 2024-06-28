namespace CommandLineGenerator.SourceWriter;

using System.Diagnostics.CodeAnalysis;

public record DefaultSourceWriterSettings(
    bool UseSpacesForTabs = true,
    int? SpacesPerTab = 4,
    bool NewlineBeforeBlock = true
    )
{

    [MemberNotNullWhen(true, nameof(SpacesPerTab))]
    public bool UseSpacesForTabs { get; } = UseSpacesForTabs;
    
    public int? SpacesPerTab { get; } = SpacesPerTab;
    
    public bool NewlineBeforeBlock { get; } = NewlineBeforeBlock;
    
}