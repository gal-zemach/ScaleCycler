using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CopyMomentum
{
    public static void Copy(UnicycleController origin, UnicycleController destination)
    {
        destination.wheelRb.angularVelocity = origin.wheelRb.angularVelocity;
        destination.wheelRb.totalTorque = origin.wheelRb.totalTorque;
        destination.wheelRb.rotation = origin.wheelRb.rotation;
        destination.wheelRb.velocity = origin.wheelRb.velocity;

        destination.bodyRb.totalTorque = origin.bodyRb.totalTorque;
        destination.bodyRb.rotation = origin.bodyRb.rotation;
        destination.bodyRb.velocity = origin.bodyRb.velocity;
    }
}
