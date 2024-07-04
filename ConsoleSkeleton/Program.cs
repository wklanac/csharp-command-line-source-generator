namespace ConsoleSkeleton;

using System.Threading.Tasks;

public partial class Program
{
    public async static Task<int> Main(string[] args)
    {
        return await InvokeFrontend(args);
    }

    public static void cat(string filePath)
    {
        Console.WriteLine("Hello");
    }
}