using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    private float speed = 20f;
    private float lifetime = 2.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Initialize(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed; 
    }

    public void Shoot(Transform cameraTransform, Bullet bulletPrefab)
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, cameraTransform.position, Quaternion.identity);
        Vector3 cameraForward = cameraTransform.forward;
        bulletInstance.Initialize(cameraForward.normalized);
        bulletInstance.transform.position = cameraTransform.position + cameraForward;
    }
}
