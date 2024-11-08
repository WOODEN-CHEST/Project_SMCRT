using GHEngine.Assets.Def;
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
    public void AddWeaponDefinition(WeaponDefinition definition);
    void SetPlanet(EntitySpawnProperties planet);
    void SetName(string name);
    void SetDescription(string description);
    void SetVersion(Version version);
    void AddAssetDefinition(AssetDefinition definition);
}