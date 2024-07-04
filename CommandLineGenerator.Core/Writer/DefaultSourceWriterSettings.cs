using System.Diagnostics.CodeAnalysis;

namespace CommandLineGenerator.Writer;

/// <summary>
///     Settings used for default source writer.
/// </summary>
/// <param name="UseSpacesForTabs">If true, use spaces to represent tabs in written source</param>
/// <param name="SpacesPerTab">If <see cref="UseSpacesForTabs" /> is true, the number of spaces to use</param>
/// <param name="NewlineBeforeBlock">If true, write newline before opening block</param>
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