using System.Diagnostics.CodeAnalysis;

namespace NewsNode.Shared.Abstractions.CommandValidators;

public static class NullValidator
{
    public static void ValidateNotNull<T>([NotNull] T value)
    {
        if (value is null)
            throw new ArgumentNullException($"{typeof(T).Name} not found");
    }
}