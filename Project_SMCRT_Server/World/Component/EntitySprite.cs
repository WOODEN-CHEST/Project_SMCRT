using GHEngine;
using GHEngine.Frame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public record class EntitySprite(DVector2 Offset, 
    DVector2 Size, 
    GenericColorMask ColorMask,
    SpriteEffects Effects,
    float ZIndex,
    string AnimationName);