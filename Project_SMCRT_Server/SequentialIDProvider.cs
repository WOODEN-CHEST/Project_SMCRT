using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public class SequentialIDProvider : IIDProvider
{
    // Private fields.
    private ulong _id = 1uL;


    // Methods.
    public ulong GetID()
    {
        return _id++;
    }
}