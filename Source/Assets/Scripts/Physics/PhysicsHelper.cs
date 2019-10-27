using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Physics
{
    public static class PhysicsHelper
    {
        public static float GetPendulumDisplacement(float a, float l, float t)
        {
            return (float) (a * Math.Cos((Math.Sqrt(l / PhysicsConstants.Gravity)) * t));
        }

        public static float GetAmplitude(Vector3 centerPosition, Vector3 objectPosition)
        {
            Vector2 p1 = new Vector2(centerPosition.x, centerPosition.z);
            Vector2 p2 = new Vector2(objectPosition.x, objectPosition.z);
            return Vector2.Distance(p1, p2);
        }
    }
}