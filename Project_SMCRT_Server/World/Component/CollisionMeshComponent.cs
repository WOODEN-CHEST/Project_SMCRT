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
    public static readonly NamespacedKey KEY = NamespacedKey.Default("collision_mesh");


    // Fields.
    public IEnumerable<DEdge> Edges => _edges;
    public bool IsCollisionEnabled { get; set; }
    public IEnumerable<ulong> ExcludedCollisionEntities => _excludedCollisionEntities;
    public IEnumerable<ulong> RecentlyCollidedEntities => _recentlyCollidedEntities;
    public double BoundingCircleRadius { get; private set; }
    public bool IsReactionAlwaysDone { get; set; } = false;


    // Private fields.
    private readonly List<DEdge> _edges = new();
    private readonly HashSet<ulong> _excludedCollisionEntities = new();
    private readonly HashSet<ulong> _recentlyCollidedEntities = new();


    // Constructors.
    public CollisionMeshComponent() : base(KEY) { }


    // Private methods.
    private void UpdateBoundingCircleRadius()
    {
        double RadiusSquared = 0d;

        foreach (DVector2 Edge in _edges.SelectMany(edge => new DVector2[] { edge.Vertex1, edge.Vertex2 }))
        {
            double Distance = Edge.LengthSquared;
            if (Distance > RadiusSquared)
            {
                RadiusSquared = Distance;
            }
        }

        BoundingCircleRadius = Math.Sqrt(RadiusSquared);
    }


    // Methods.
    public void AddEdge(DEdge egde)
    {
        _edges.Add(egde);
        UpdateBoundingCircleRadius();
    }

    public void RemoveEdge(DEdge edge)
    {
        _edges.Remove(edge);
        UpdateBoundingCircleRadius();
    }

    public void ClearEdges()
    {
        _edges.Clear();
        UpdateBoundingCircleRadius();
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

    public void AddRecentlyCollidedEntity(ulong entity)
    {
        _recentlyCollidedEntities.Add(entity);
    }

    public void RemoveRecentlyCollidedEntity(ulong entity)
    {
        _recentlyCollidedEntities.Remove(entity);
    }

    public void ClearRecentlyCollidedEntity()
    {
        _recentlyCollidedEntities.Clear();
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        MotionComponent NewComponent = new();
        NewComponent.SetFrom(this);
        return NewComponent;
    }

    public override bool SetFrom(EntityComponent component)
    {
        if (component is not CollisionMeshComponent Target)
        {
            return false;
        }

        _edges.Clear();
        _excludedCollisionEntities.Clear();
        _recentlyCollidedEntities.Clear();

        foreach (DEdge TargetEdge in Target._edges)
        {
            _edges.Add(TargetEdge);
        }
        foreach (ulong Entity in Target._excludedCollisionEntities)
        {
            _excludedCollisionEntities.Add(Entity);
        }
        foreach (ulong Entity in Target._recentlyCollidedEntities)
        {
            _recentlyCollidedEntities.Add(Entity);
        }
        IsCollisionEnabled = Target.IsCollisionEnabled;
        BoundingCircleRadius = Target.BoundingCircleRadius;
        IsReactionAlwaysDone = Target.IsReactionAlwaysDone;

        return true;
    }
}