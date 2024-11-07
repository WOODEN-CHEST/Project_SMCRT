using GHEngine;
using GHEngine.Frame;
using GHEngine.IO.JSON;
using Microsoft.Xna.Framework.Graphics;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class SpriteDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_SPRITES = "sprites";

    public const string KEY_MASK = "mask";
    public const string KEY_COLOR = "color";
    public const string KEY_BRIGHTNESS = "brightness";
    public const string KEY_OPACITY = "opacity";

    public const string KEY_SIZE = "size";
    public const string KEY_OFFSET = "offset";

    public const string KEY_EFFECTS = "effects";
    public const string VALUE_NONE = "none";
    public const string VALUE_FLIP_HORIZONTALLY = "flip_horizontally";
    public const string VALUE_FLIP_VERTICALLY = "flip_vertically";

    public const string KEY_Z_INDEX = "z_index";

    public const string KEY_ANIMATION = "animation";


    // Private methods.
    private IEnumerable<EntitySprite> GetSprites(JSONList list, GenericJSONDeconstructor genericDeconstructor)
    {
        List<EntitySprite> Sprites = new();
        foreach (object? ListObject in list)
        {
            if (ListObject is not JSONCompound Compound)
            {
                throw new PackContentException("Expected compound in list of entity sprites.");
            }
            Sprites.Add(GetSingleSprite(Compound, genericDeconstructor));
        }
        return Sprites;
    }

    private EntitySprite GetSingleSprite(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        DVector2 Offset = GetOffset(compound, genericDeconstructor);
        DVector2 Size = GetSize(compound, genericDeconstructor);
        GenericColorMask Mask = GetColorMask(compound, genericDeconstructor);
        string AnimName = compound.GetVerified<string>(KEY_ANIMATION);
        SpriteEffects Effects = GetEffects(compound);
        float ZIndex = GetZIndex(compound, genericDeconstructor);

        return new EntitySprite( Offset, Size, Mask, Effects, ZIndex, AnimName);
    }

    private DVector2 GetOffset(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        if (compound.GetOptionalVerified(KEY_OFFSET, out JSONList? VertexList))
        {
            return genericDeconstructor.GetVector(VertexList!);
        }
        return DVector2.Zero;
    }

    private DVector2 GetSize(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        if (compound.GetOptionalVerified(KEY_SIZE, out JSONList? VertexList))
        {
            return genericDeconstructor.GetVector(VertexList!);
        }
        return DVector2.Zero;
    }


    private GenericColorMask GetColorMask(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        GenericColorMask ColorMask = new();
        if (!compound.GetOptionalVerified(KEY_MASK, out JSONList? MaskCompound))
        {
            return ColorMask;
        }

        if (compound.GetOptionalVerified(KEY_COLOR, out object? Color))
        {
            ColorMask.Mask = genericDeconstructor.GetColor(Color!);
        }
        if (compound.GetOptionalVerified(KEY_BRIGHTNESS, out object? Brightness))
        {
            ColorMask.Brightness = (float)genericDeconstructor.GetAsDouble(Brightness!);
        }
        if (compound.GetOptionalVerified(KEY_OPACITY, out object? Opacity))
        {
            ColorMask.Opacity = (float)genericDeconstructor.GetAsDouble(Opacity!);
        }
        return ColorMask;
    }

    private SpriteEffects GetEffects(JSONCompound compound)
    {
        if (!compound.GetOptionalVerified(KEY_EFFECTS, out string? Name))
        {
            return SpriteEffects.None;
        }

        return Name switch
        {
            VALUE_FLIP_VERTICALLY => SpriteEffects.FlipVertically,
            VALUE_FLIP_HORIZONTALLY => SpriteEffects.FlipHorizontally,
            _ => throw new PackContentException($"Invalid sprite effects type: \"{Name}\"")
        };
    }

    private float GetZIndex(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        if (compound.GetOptionalVerified(KEY_Z_INDEX, out object? Index))
        {
            return (float)genericDeconstructor.GetAsDouble(Index!);
        }
        return 0f;
    }


    // Methods. 
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        SpriteComponent Component = new();

        if (compound.GetOptionalVerified(KEY_SPRITES, out JSONList? SpriteList))
        {
            foreach (EntitySprite Sprite in GetSprites(SpriteList!, genericDeconstructor))
            {
                Component.AddSprite(Sprite);
            }
        }

        return Component;
    }
}