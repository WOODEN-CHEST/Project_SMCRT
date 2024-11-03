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


    // Fields.
    public string Namespace { get; private init; }
    public string Key { get; private init; }


    // Constructors.
    public NamespacedKey(string nameSpace, string key)
    {
        Namespace = nameSpace ?? throw new ArgumentNullException(nameof(nameSpace));
        Key = key ?? throw new ArgumentNullException(nameof(key));
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