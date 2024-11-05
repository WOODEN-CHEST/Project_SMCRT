using GHEngine;
using GHEngine.Frame;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public record class EntitySprite(DVector2 Offset, 
    DVector2 Size, 
    GenericColorMask ColorMask,
    float ZIndex,
    string AnimationName);