using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Player;

public interface IServerPlayer
{
    // Fields.
    ulong ID { get; }



    // Methods.
    void Kick();
}