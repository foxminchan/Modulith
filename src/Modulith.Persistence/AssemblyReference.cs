using System.Reflection;

namespace Modulith.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
}