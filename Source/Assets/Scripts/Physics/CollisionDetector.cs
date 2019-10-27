using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action<Vector3> OnCollied;

    private void OnCollisionEnter(Collision other)
    {
        OnCollied?.Invoke(other.contacts[0].point);
    }
}
