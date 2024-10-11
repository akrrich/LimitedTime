using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    protected PlayerController playerController;

    [SerializeField] protected EnemyScriptable enemyScriptable;

    private BoxCollider boxCollider;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    protected Animator anim;
    private AudioSource[] enemiesAudios;

    protected int life;

    protected bool isMovinmgForAttack = false;


    protected virtual void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        boxCollider = GetComponent<BoxCollider>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        enemiesAudios = GetComponentsInChildren<AudioSource>();

        anim.transform.LookAt(playerController.transform);
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            anim.transform.position = transform.position;

            if (playerController.IsGrounded && playerController.PlayerAlive)
            {
                anim.transform.LookAt(playerController.transform);
            }

            if (isMovinmgForAttack)
            {
                anim.SetFloat("Movements", 1f);
            }

            CalculateDistance();
        }

        else
        {
            StopAnimationWhenPlayerDeaths();
        }

        foreach (AudioSource audios in enemiesAudios)
        {
            PauseManager.PauseAndUnPauseSounds(audios);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            life -= playerController.Damage;

            if (life >= 1)
            {
                enemiesAudios[0].Play();
            }

            die();
        }
    }


    private void CalculateDistance()
    {
        if (!isMovinmgForAttack && playerController.PlayerAlive)
        {
            if (Vector3.Distance(transform.position, playerController.transform.position) <= enemyScriptable.Radius)
            {
                Movement();
            }

            else
            {
                anim.SetFloat("Movements", 0f);
            }
        }

        else if (!isMovinmgForAttack)
        {
            anim.SetFloat("Movements", 0f);
        }
    }

    private void die()
    {
        if (life <= 0)
        {
            enemiesAudios[1].Play();

            int randomNumer = Random.Range(0, 8);

            if (randomNumer == Random.Range(0, 8))
            {
                AbstractFactory.CreatePowerUp(Random.Range(0, 3), transform);
            }

            skinnedMeshRenderer.enabled = false;
            boxCollider.enabled = false;

            Destroy(gameObject, enemiesAudios[1].clip.length);
        }
    }

    private void StopAnimationWhenPlayerDeaths()
    {
        anim.SetFloat("Movements", 0f);
    }

    protected abstract void Movement();

    protected abstract void Attack(Collision collision);
}
