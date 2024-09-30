using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Deforme : Enemies
{
    private float damageDelay = 1f;

    private bool canDamage = true;


    protected override void Start()
    {
        base.Start();

        life = 5;
        speed = 2.5f;
        radius = 15f;
    }

    void OnCollisionStay(Collision collision)
    {
        if (canDamage)
        {
            Attack(collision);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMovinmgForAttack = false;
        }
    }


    protected override void Movement()
    {
        anim.SetFloat("Movements", 0.5f);

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    protected override void Attack(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            isMovinmgForAttack = true;
            player.Life -= 1;

            if (player.Life <= 0)
            {
                canDamage = false;
                isMovinmgForAttack = false;
            }

            if (player.Life >= 1)
            {
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageDelay);
        canDamage = true;
    }
}
