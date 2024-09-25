using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Deforme : Enemies
{
    new void Start()
    {
        base.Start();

        life = 5;
        speed = 2.5f;
        radius = 15f;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= radius)
        {
            Attack();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    protected override void Attack()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
