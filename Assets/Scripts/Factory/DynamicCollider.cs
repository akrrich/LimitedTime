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

    private MeshRenderer ms;
    public MeshRenderer Ms { get { return ms; } set { ms = value; } }

    private BoxCollider box;
    public BoxCollider Box { get { return box; } set { box = value; } }

    private SphereCollider spc;
    public SphereCollider Spc { get { return spc; } set { spc = value; } }

    private CapsuleCollider cpc;
    public CapsuleCollider Cpc { get { return cpc; } set { cpc = value; } }


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
        ms = GetComponent<MeshRenderer>();

        if (ms != null)
        {
            switch (colliderType)
            {
                case ColliderType.Box:
                    AddAndConfigureBoxCollider(ms);
                    break;

                case ColliderType.Sphere:
                    AddAndConfigureSphereCollider(ms);
                    break;

                case ColliderType.Capsule:
                    AddAndConfigureCapsuleCollider(ms);
                    break;
            }
        }
    }

    private void AddAndConfigureBoxCollider(MeshRenderer meshRenderer)
    {
        box = gameObject.GetComponent<BoxCollider>();

        if (box == null)
        {
            box = gameObject.AddComponent<BoxCollider>();
        }

        Bounds bounds = meshRenderer.bounds;
        box.size = bounds.size;
        box.center = bounds.center - transform.position;
    }

    private void AddAndConfigureSphereCollider(MeshRenderer meshRenderer)
    {
        spc = gameObject.GetComponent<SphereCollider>();

        if (spc == null)
        {
            spc = gameObject.AddComponent<SphereCollider>();
        }

        Bounds bounds = meshRenderer.bounds;
        float radius = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) / 2f;
        spc.radius = radius;
        spc.center = bounds.center - transform.position;
    }

    private void AddAndConfigureCapsuleCollider(MeshRenderer meshRenderer)
    {
        cpc = gameObject.GetComponent<CapsuleCollider>();

        if (cpc == null)
        {
            cpc = gameObject.AddComponent<CapsuleCollider>();
        }

        Bounds bounds = meshRenderer.bounds;
        cpc.radius = Mathf.Max(bounds.size.x, bounds.size.z) / 2f;
        cpc.height = bounds.size.y;
        cpc.center = bounds.center - transform.position;
    }
}
