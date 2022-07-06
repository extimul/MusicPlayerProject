using System.Reflection;

namespace ServerApp.Base.Extensions;

public static class AssemblyExtensions
{
    public static string? GetAssemblyDirectory(this Assembly assembly) => 
        Path.GetDirectoryName(assembly.Location);
}