using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class ComponentRemoveEventArgs : ComponentEventArgs
{
    public ComponentRemoveEventArgs(EntityComponent component, ulong entity) : base(component, entity) { }
}