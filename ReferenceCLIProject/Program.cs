using System.Diagnostics;

namespace ReferenceCLIProject;

public partial class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await InvokeFrontend(args);
    }

    public static void cat(string filePath)
    {
        var commandProcess = Process.Start("cat", filePath);
        commandProcess.WaitForExit();
    }
}