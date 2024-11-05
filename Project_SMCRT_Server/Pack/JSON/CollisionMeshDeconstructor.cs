using GHEngine.IO.JSON;
using GHEngine;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class CollisionMeshDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_EDGES = "edges";
    public const string KEY_IS_COLLISION_ENABLED = "is_collision_enabled";


    // Private methods.
    private IEnumerable<DEdge> GetMeshEdges(JSONList EdgeList, GenericJSONDeconstructor genericDeconstructor)
    {
        List<DEdge> Edges = new();
        foreach (object? ListEntry in EdgeList)
        {
            if (ListEntry is not JSONList VertexList)
            {
                throw new PackContentException("Collision mesh edge list contains an invalid entry, expected vertex list.");
            }

            DVector2 Vertex1 = new(genericDeconstructor.GetAsDouble(VertexList.GetVerified<object>(0)),
                genericDeconstructor.GetAsDouble(VertexList.GetVerified<object>(1)));
            DVector2 Vertex2 = new(genericDeconstructor.GetAsDouble(VertexList.GetVerified<object>(2)),
                genericDeconstructor.GetAsDouble(VertexList.GetVerified<object>(3)));

            Edges.Add(new DEdge(Vertex1, Vertex2));
        }
        return Edges;
    }


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        CollisionMeshComponent Component = new();

        if (compound.GetOptionalVerified(KEY_EDGES, out JSONList? EdgeList))
        {
            foreach (DEdge MeshEdge in GetMeshEdges(EdgeList!, genericDeconstructor))
            {
                Component.AddEdge(MeshEdge);
            }
        }
        if (compound.GetOptionalVerified(KEY_IS_COLLISION_ENABLED, out bool IsCollisionEnabled))
        {
            Component.IsCollisionEnabled = IsCollisionEnabled;
        }

        return Component;
    }
}