using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPool bulletPool;

    private Rigidbody rb;
    private MeshRenderer mr;
    private SphereCollider sphere;
    private AudioSource audioShoot;

    private float speed = 30f;
    private float lifeTime = 3f;


    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            mr.enabled = false;
            sphere.enabled = false;

            ReturnToPool();
        }
    }

    public void InstantiateBullet(Transform cameraTransform, BulletPool pool)
    {
        bulletPool = pool;
        Vector3 cameraForward = cameraTransform.forward;
        Initialize(cameraForward.normalized);
        transform.position = cameraTransform.position + cameraForward;
    }


    private void Initialize(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        sphere = GetComponent<SphereCollider>();
        audioShoot = GetComponent<AudioSource>();

        mr.enabled = true;
        sphere.enabled = true;


        rb.velocity = direction * speed;

        audioShoot.Play();

        Invoke("ReturnToPool", lifeTime);
    }

    private void ReturnToPool()
    {
        bulletPool.ReturnBulletToPool(this);
    }
}

