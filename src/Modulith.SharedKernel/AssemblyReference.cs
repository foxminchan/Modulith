using System.Reflection;

namespace Modulith.SharedKernel;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}