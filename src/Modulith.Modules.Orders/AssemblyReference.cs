using System.Reflection;

namespace Modulith.Modules.Orders;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Extension).Assembly;
}