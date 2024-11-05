using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

public interface IModifiableDataPack : IDataPack
{
    // Methods.
    public void AddEntityDefinition(EntityDefinition definition);
    void SetPlanet(NamespacedKey planet);
    void SetName(string name);
    void SetDescription(string description);
    void SetVersion(Version version);
}