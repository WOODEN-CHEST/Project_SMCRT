using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class ResourceContainerComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("resource_container");


    // Fields.
    public double Metal { get; set; } = 0d;
    public double MetalPerSecond { get; set; } = 0d;
    public double Composites { get; set; } = 0d;
    public double CompositesPerSecond { get; set; } = 0d;
    public double Fuel { get; set; } = 0d;
    public double FuelPerSecond { get; set; } = 0d;
    public double Research { get; set; } = 0d;
    public double ResearchPerSecond { get; set; } = 0d;


    // Constructors.
    public ResourceContainerComponent() : base(KEY) { }



    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new ResourceContainerComponent()
        {
            Metal = Metal,
            MetalPerSecond = MetalPerSecond,
            Composites = Composites,
            CompositesPerSecond = CompositesPerSecond,
            Fuel = Fuel,
            FuelPerSecond = FuelPerSecond,
            Research = Research,
            ResearchPerSecond = ResearchPerSecond,
        };
    }
}