using System.Reflection;

namespace Modulith.Modules.Products;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Extension).Assembly;
}