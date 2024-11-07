using GHEngine;
using GHEngine.Frame;
using GHEngine.Frame.Animation;
using GHEngine.Frame.Item;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Client.Section.Component;

public class GameBackground : IRenderableItem, ITimeUpdatable
{
    // Fields.
    public bool IsVisible { get; set; }


    // Private fields.
    private readonly SpriteItem _sprite;


    // Constructors.
    public GameBackground(IAnimationInstance animation)
    {
        _sprite = new(animation);
        _sprite.Position = DVector2.Zero;
        _sprite.IsPositionAdjusted = false;
        _sprite.IsSizeAdjusted = false;
    }


    // Inherited methods,
    public void Render(IRenderer renderer, IProgramTime time)
    {
        float TextureAspectRatio = _sprite.FrameSize.X / _sprite.FrameSize.Y;

        if (renderer.AspectRatio > TextureAspectRatio)
        {
            _sprite.Size = new(1f, renderer.AspectRatio / TextureAspectRatio);
        }
        else
        {
            _sprite.Size = new(TextureAspectRatio / renderer.AspectRatio, 1f);
        }

        _sprite.Render(renderer, time);
    }

    public void Update(IProgramTime time)
    {
        _sprite.Update(time);
    }
}