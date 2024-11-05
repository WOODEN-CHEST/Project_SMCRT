using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class ComponentAddEventArgs : ComponentEventArgs
{
    public ComponentAddEventArgs(EntityComponent component, ulong entity) : base(component, entity) { }
}