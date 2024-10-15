using UnityEngine;

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
    private BoxCollider box;
    private SphereCollider sphere;
    private CapsuleCollider capsule;

    public MeshRenderer Ms { get { return ms; } set { ms = value; } }
    public BoxCollider Box { get { return box; } set { box = value; } }
    public SphereCollider Sphere { get { return sphere; } set { sphere = value; } }
    public CapsuleCollider Capsule { get { return capsule; } set { capsule = value; } }


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

        Vector3 colliderSize = new Vector3(

            bounds.size.x / transform.localScale.x,
            bounds.size.y / transform.localScale.y,
            bounds.size.z / transform.localScale.z
        );

        Vector3 colliderCenter = new Vector3(

            (bounds.center.x - transform.position.x) / transform.localScale.x,
            (bounds.center.y - transform.position.y) / transform.localScale.y,
            (bounds.center.z - transform.position.z) / transform.localScale.z
        );

        box.size = colliderSize;
        box.center = colliderCenter;
    }

    private void AddAndConfigureSphereCollider(MeshRenderer meshRenderer)
    {
        sphere = gameObject.GetComponent<SphereCollider>();

        if (sphere == null)
        {
            sphere = gameObject.AddComponent<SphereCollider>();
        }

        Bounds bounds = meshRenderer.bounds;

        float radius = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) / 2f;
        float colliderRadius = radius / Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        Vector3 colliderCenter = new Vector3(

            (bounds.center.x - transform.position.x) / transform.localScale.x,
            (bounds.center.y - transform.position.y) / transform.localScale.y,
            (bounds.center.z - transform.position.z) / transform.localScale.z
        );

        sphere.radius = colliderRadius;
        sphere.center = colliderCenter;
    }

    private void AddAndConfigureCapsuleCollider(MeshRenderer meshRenderer)
    {
        capsule = gameObject.GetComponent<CapsuleCollider>();

        if (capsule == null)
        {
            capsule = gameObject.AddComponent<CapsuleCollider>();
        }

        Bounds bounds = meshRenderer.bounds;

        float radius = Mathf.Max(bounds.size.x, bounds.size.z) / 2f;
        float height = bounds.size.y;

        float colliderRadius = radius / Mathf.Max(transform.localScale.x, transform.localScale.z);
        float colliderHeight = height / transform.localScale.y;

        Vector3 colliderCenter = new Vector3(

            (bounds.center.x - transform.position.x) / transform.localScale.x,
            (bounds.center.y - transform.position.y) / transform.localScale.y,
            (bounds.center.z - transform.position.z) / transform.localScale.z
        );

        capsule.radius = colliderRadius;
        capsule.height = colliderHeight;
        capsule.center = colliderCenter;
    }
}
