using System.Reflection;

namespace Modulith.Modules.Users;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Extension).Assembly;
}