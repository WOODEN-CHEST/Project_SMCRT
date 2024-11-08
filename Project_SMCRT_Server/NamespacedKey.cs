using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public class NamespacedKey
{
    // Static fields.
    public const string NAMESPACE_SMCRT = "smcrt";
    public const char SPERATOR = ':';


    // Fields.
    public string Namespace { get; private init; }
    public string Key { get; private init; }


    // Constructors.
    public NamespacedKey(string nameSpace, string key)
    {
        Namespace = nameSpace ?? throw new ArgumentNullException(nameof(nameSpace));
        Key = key ?? throw new ArgumentNullException(nameof(key));

        VerifyString(Namespace);
        VerifyString(Key);
    }

    public NamespacedKey(string fullKey)
    {
        int Separator = fullKey.IndexOf(SPERATOR);
        if (Separator == -1)
        {
            throw new ArgumentException("Missing namespace separator");
        }
        if (Separator == fullKey.Length - 1)
        {
            throw new ArgumentException("Missing value");
        }
        if (Separator == 0)
        {
            throw new ArgumentException("Missing namespace");
        }

        Namespace = fullKey[..Separator];
        Key = fullKey[(Separator + 1)..];

        VerifyString(Namespace);
        VerifyString(Key);
    }


    // Private methods.
    private void VerifyString(string stringToVerify)
    {
        foreach (char Character in stringToVerify)
        {
            if (!(char.IsAsciiLetter(Character) || char.IsAsciiDigit(Character) || (Character == '_')))
            {
                throw new ArgumentException($"Invalid character in string: '{Character}'");
            }
        }
    }


    // Static methods.
    public static NamespacedKey Default(string key)
    {
        return new NamespacedKey(NAMESPACE_SMCRT, key);
    }


    // Inherited methods.
    public override bool Equals(object? obj)
    {
        if (obj is NamespacedKey Target)
        {
            return (Key  == Target.Key) && (Namespace == Target.Namespace);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Namespace.GetHashCode() * Key.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Namespace}:{Key}";
    }
}