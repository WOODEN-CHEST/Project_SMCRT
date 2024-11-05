using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class EntityRemoveEventArgs : EntityEventArgs
{
    public EntityRemoveEventArgs(ulong entity) : base(entity) { }
}