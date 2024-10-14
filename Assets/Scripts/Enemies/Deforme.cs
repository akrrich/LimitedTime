using System.Collections;
using UnityEngine;

public class Deforme : Enemies
{
    private float damageDelay = 1.6f;

    private bool canDamage = true;


    protected override void Start()
    {
        base.Start();

        life = 4;
    }

    protected override void Update()
    {
        base.Update();
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
        transform.position += direction * enemyScriptable.Speed * Time.deltaTime;
    }

    protected override void Attack(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemiesAudios[2].Play();

            isMovinmgForAttack = true;
            playerController.Life -= 1;

            if (playerController.Life <= 0)
            {
                //canDamage = false;
                isMovinmgForAttack = false;

                OnPlayerDefeated?.Invoke();

                //canDamage = true;
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
