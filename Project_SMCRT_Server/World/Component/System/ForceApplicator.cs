using GHEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

public class ForceApplicator
{
    // Methods.
    public void ApplyForce(PositionComponent position,
        MotionComponent motion,
        PhysicalPropertiesComponent physicalProperties,
        DVector2 amount,
        DVector2 origin)
    {
        if (amount.LengthSquared == 0d)
        {
            return;
        }

        DVector2 AddedMotion = DVector2.Normalize(amount) * (amount.Length / physicalProperties.Mass);
        double AddedAngularMotion;
        DVector2 OriginToCenter = position.Position - origin;
        if (OriginToCenter.LengthSquared == 0d)
        {
            AddedAngularMotion = 0d;
        }
        else
        {
            AddedAngularMotion = DVector2.Dot(DVector2.Normalize(amount),
                DVector2.Normalize(GHMath.PerpendicularVectorClockwise(OriginToCenter)))
                * OriginToCenter.Length / physicalProperties.Mass;
        }
    }
}