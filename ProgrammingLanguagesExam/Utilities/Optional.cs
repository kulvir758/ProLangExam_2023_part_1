namespace Utilities;

using System;

public class Optional<T> where T : class
{
    private readonly T _value;

    public Optional(T value)
    {
        _value = value;
    }

    public bool HasValue => _value != null;

    public T Value
    {
        get
        {
            if (_value == null)
            {
                throw new InvalidOperationException("Optional object has no value.");
            }

            return _value;
        }
    }

    public override string ToString()
    {
        return _value?.ToString() ?? string.Empty;
    }
}
