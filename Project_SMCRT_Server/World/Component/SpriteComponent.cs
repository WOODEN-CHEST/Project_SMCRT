using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class SpriteComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("sprite");


    // Fields.
    public IEnumerable<EntitySprite> Sprites => _sprites;
    public bool IsRenderingEnabled { get; set; }


    // Private fields.
    private readonly List<EntitySprite> _sprites = new();


    // Constructors.
    public SpriteComponent() : base(KEY) { }


    // Methods.
    public void AddSprite(EntitySprite sprite)
    {
        _sprites.Add(sprite ?? throw new ArgumentNullException(nameof(sprite)));
    }

    public void RemoveSprite(EntitySprite sprite)
    {
        _sprites.Remove(sprite ?? throw new ArgumentNullException(nameof(sprite)));
    }

    public void ClearSprites()
    {
        _sprites.Clear();
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        SpriteComponent CreatedComponent = new();
        CreatedComponent.SetFrom(this);
        return CreatedComponent;
    }

    public override bool SetFrom(EntityComponent component)
    {
        if (component is not SpriteComponent Target)
        {
            return false;
        }

        _sprites.Clear();

        IsRenderingEnabled = Target.IsRenderingEnabled;
        foreach (EntitySprite Sprite in Target._sprites)
        {
            AddSprite(Sprite);
        }

        return true;
    }
}