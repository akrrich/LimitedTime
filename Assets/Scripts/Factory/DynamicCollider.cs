using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Tilemaps.Tile;

public enum ColliderType
{
    Box,
    Sphere,
    Capsule
}

public class DynamicCollider : MonoBehaviour
{
    private ColliderType colliderType;


    private void Start()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 1000000;
    }

    public void SetColliderType(ColliderType type)
    {
        colliderType = type;
    }

    public void InitializeCollider()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            switch (colliderType)
            {
                case ColliderType.Box:
                    AddAndConfigureBoxCollider(meshRenderer);
                    break;

                case ColliderType.Sphere:
                    AddAndConfigureSphereCollider(meshRenderer);
                    break;

                case ColliderType.Capsule:
                    AddAndConfigureCapsuleCollider(meshRenderer);
                    break;
            }
        }
    }

    private void AddAndConfigureBoxCollider(MeshRenderer meshRenderer)
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        Bounds bounds = meshRenderer.bounds;
        boxCollider.size = bounds.size;
        boxCollider.center = bounds.center - transform.position;
    }

    private void AddAndConfigureSphereCollider(MeshRenderer meshRenderer)
    {
        SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();

        if (sphereCollider == null)
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
        }

        Bounds bounds = meshRenderer.bounds;
        float radius = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) / 2f;
        sphereCollider.radius = radius;
        sphereCollider.center = bounds.center - transform.position;
    }

    private void AddAndConfigureCapsuleCollider(MeshRenderer meshRenderer)
    {
        CapsuleCollider capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

        if (capsuleCollider == null)
        {
            capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
        }

        Bounds bounds = meshRenderer.bounds;
        capsuleCollider.radius = Mathf.Max(bounds.size.x, bounds.size.z) / 2f;
        capsuleCollider.height = bounds.size.y;
        capsuleCollider.center = bounds.center - transform.position;
    }
}
