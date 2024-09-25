using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform;

    protected Rigidbody rb;
    protected BoxCollider boxCollider;
    protected MeshRenderer mr;
    protected Animator anim;
    protected AudioSource[] enemiesAudios;

    protected int life;
    protected float speed;

    protected float radius;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mr = GetComponent<MeshRenderer>();
        //anim = GetComponentInChildren<Animator>();
        //enemiesAudios = GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        CalculateDistance();
    }

    void OnDestroy()
    {
            
    }

    protected virtual void CalculateDistance()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= radius)
        {
            Attack();
        }
    }

    protected virtual void die()
    {
        if (life <= 0)
        {
            enemiesAudios[3].Play();

            mr.enabled = false;
            boxCollider.enabled = false;

            Destroy(gameObject, enemiesAudios[3].clip.length);
        }
    }

    protected abstract void Attack();
}
