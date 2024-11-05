using GHEngine;
using GHEngine.IO.JSON;
using Microsoft.Xna.Framework;
using Project_SMCRT_Server.World.Component;
using System.Globalization;

namespace Project_SMCRT_Server.Pack.JSON;

public class GenericJSONDeconstructor
{
    // Static fields.
    public const string KEY_INPUT_ACTION_ID = "id";


    // Private methods.
    private byte GetAsColorComponent(object value)
    {
        if (value is long LongValue)
        {
            return (byte)LongValue;
        }
        if (value is double DoubleValue)
        {
            return (byte)((double.IsNaN(DoubleValue) ? 0d : Math.Clamp(DoubleValue, 0d, 1d)) * byte.MaxValue);
        }
        throw new PackContentException("Expected number for color component.");
    }


    // Methods.
    public double GetAsDouble(object value)
    {
        if (value is long)
        {
            return (double)(long)value;
        }
        if (value is double)
        {
            return (double)value;
        }
        throw new PackContentException("Expected number, got invalid bullshit. I am tired of writing these error messages " +
            "which don't really even help at this point..");
    }

    public InputAction GetInputAction(JSONCompound compound)
    {
        return new InputAction((int)compound.GetVerified<long>(KEY_INPUT_ACTION_ID));
    }

    public DVector2 GetVector(JSONList list)
    {
        return new(GetAsDouble(list.GetVerified<object>(0)), GetAsDouble(list.GetVerified<object>(1)));
    }

    public Color GetColor(object color)
    {
        if (color is string StringValue)
        {
            bool CanTrim = StringValue.StartsWith('#') && (StringValue.Length > 1);
            try
            {
                return new Color(Convert.ToUInt32(CanTrim ? StringValue.Substring(1) : StringValue, CultureInfo.InvariantCulture));
            }
            catch (Exception e)
            {
                throw new PackContentException($"Invalid number for color: \"{StringValue}\"");
            }
            
        }
        else if (color is JSONList ComponentList)
        {
            return new Color(
                GetAsColorComponent(ComponentList.GetVerified<object>(0)),
                GetAsColorComponent(ComponentList.GetVerified<object>(1)),
                GetAsColorComponent(ComponentList.GetVerified<object>(2)),
                GetAsColorComponent(ComponentList.GetVerified<object>(3)));
        }
        throw new PackContentException("Invalid value for color component," +
            " expected number in range [0-255] or [0;1] or string color");
        return new Color();
    }
}