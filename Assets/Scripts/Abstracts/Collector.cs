using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collector : MonoBehaviour
{
    public float collectSpeed = 0.25f;
    protected SphereCollider colliderSphere;
    public abstract void SetMagnetRadius(float radius);
    public abstract void Collect(Collider other);

    private void Awake()
    {
        colliderSphere = GetComponent<SphereCollider>();
    }
}
