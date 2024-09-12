using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    private DynamicCollider dynamicCollider;

    [SerializeField] private ColliderType colliderType;

    [SerializeField] private int id;
    public int Id { get { return id; } }

    void Start()
    {
        dynamicCollider = gameObject.AddComponent<DynamicCollider>();
        dynamicCollider.SetColliderType(colliderType);
        dynamicCollider.InitializeCollider();
    }
}
