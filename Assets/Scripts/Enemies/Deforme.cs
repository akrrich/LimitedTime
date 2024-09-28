using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Deforme : Enemies
{
    protected override void Start()
    {
        base.Start();

        life = 1;
        speed = 2.5f;
        radius = 15f;
    }

    protected override void Attack()
    { 
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
