using System.Reflection;

namespace Modulith.Modules.Baskets;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Extension).Assembly;
}