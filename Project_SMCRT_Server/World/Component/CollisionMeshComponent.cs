using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class CollisionMeshComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = new(NamespacedKey.NAMESPACE_SMCRT, "collision_mesh");


    // Fields.
    public IEnumerable<DEdge> Edges => _edges;
    public bool IsCollisionEnabled { get; set; }
    public IEnumerable<ulong> ExcludedCollisionEntities => _excludedCollisionEntities;


    // Private fields.
    private readonly List<DEdge> _edges = new();
    private readonly HashSet<ulong> _excludedCollisionEntities = new();


    // Constructors.
    public CollisionMeshComponent(NamespacedKey key) : base(key) { }


    // Methods.
    public void AddEdge(DEdge egde)
    {
        _edges.Add(egde);
    }

    public void RemoveEdge(DEdge edge)
    {
        _edges.Remove(edge);
    }

    public void ClearEdges()
    {
        _edges.Clear();
    }

    public void ExcludeCollision(ulong entity)
    {
        _excludedCollisionEntities.Add(entity);
    }

    public void IncludeCollision(ulong entity)
    {
        _excludedCollisionEntities.Remove(entity);
    }

    public void ClearExcludes()
    {
        _edges.Clear();
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        throw new NotImplementedException();
    }
}