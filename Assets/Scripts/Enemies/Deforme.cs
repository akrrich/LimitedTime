using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Deforme : Enemies
{
    private float damageDelay = 1.5f;

    private bool canDamage = true;


    private static event Action onPlayerDefeated;
    public static Action OnPlayerDefeated { get => onPlayerDefeated; set => onPlayerDefeated = value; } 


    protected override void Start()
    {
        base.Start();

        life = 5;
        speed = 2.5f;
        radius = 25f;
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

        Vector3 direction = (playerController.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    protected override void Attack(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            isMovinmgForAttack = true;
            playerController.Life -= 1;

            if (playerController.Life <= 0)
            {
                canDamage = false;
                isMovinmgForAttack = false;
                onPlayerDefeated?.Invoke();
            }

            else if (playerController.Life >= 1)
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
