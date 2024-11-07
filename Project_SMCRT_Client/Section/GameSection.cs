using GHEngine;
using GHEngine.Assets;
using GHEngine.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_SMCRT_Client.Section;

public abstract class GameSection
{
    // Fields.
    public abstract IGameFrame Frame { get; }


    // Methods.
    public abstract void Update(IProgramTime time);
    public abstract void Load(IAssetProvider assetProvider);
    public abstract void Unload(IAssetProvider assetProvider);
    public abstract void Start();
    public abstract void End();
}