using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.Script;

public interface IWorldScript
{
    // Fields.
    NamespacedKey Key { get; }
    string Name { get; }
    string Description { get; }
    TimeSpan CurrentTime { get; }



    // Methods.
    void Reset();
    IEnumerable<IScriptEvent> Move(TimeSpan time);
    void Skip(TimeSpan time);
    IWorldScript CreateCopy();
}