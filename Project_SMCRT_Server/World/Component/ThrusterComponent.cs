using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class ThrusterComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("thruster");


    // Fields.
    public IEnumerable<EntityThruster> Thrusters => _thrusers;


    // Private fields.
    private readonly List<EntityThruster> _thrusers = new();


    // Constructors.
    public ThrusterComponent() : base(KEY) { }


    // Methods.
    public void AddThruster(EntityThruster thruster)
    {
        _thrusers.Add(thruster ?? throw new ArgumentNullException(nameof(thruster)));
    }

    public void RemoveThruster(EntityThruster thruster)
    {
        _thrusers.Remove(thruster ?? throw new ArgumentNullException(nameof(thruster)));
    }

    public void ClearThrusters()
    {
        _thrusers.Clear();
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        ThrusterComponent CreatedComponent = new();
        CreatedComponent.SetFrom(this);
        return CreatedComponent;
    }

    public override bool SetFrom(EntityComponent component)
    {
        if (component is not ThrusterComponent Target)
        {
            return false;
        }

        _thrusers.Clear();
        foreach (EntityThruster Thruster in Target._thrusers)
        {
            AddThruster(Thruster.CreateCopy());
        }
        return true;
    }
}