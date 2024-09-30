using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    protected PlayerController player;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    protected Animator anim;
    private AudioSource[] enemiesAudios;

    protected int life;
    protected float speed;
    
    protected float radius;

    protected bool isMovinmgForAttack = false;


    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        enemiesAudios = GetComponentsInChildren<AudioSource>();

        anim.transform.LookAt(player.transform);
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            anim.transform.position = transform.position;

            if (player.IsGrounded && player.playerAlive)
            {
                anim.transform.LookAt(player.transform);
            }

            if (isMovinmgForAttack)
            {
                anim.SetFloat("Movements", 1f);
            }

            CalculateDistance();

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
            life -= 1;

            if (life >= 1)
            {
                enemiesAudios[0].Play();
            }

            die();
        }
    }

    private void CalculateDistance()
    {
        if (!isMovinmgForAttack && player.playerAlive)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= radius)
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

            AbstractFactory.CreatePowerUp(Random.Range(0, 0), transform);

            skinnedMeshRenderer.enabled = false;
            boxCollider.enabled = false;

            Destroy(gameObject, enemiesAudios[1].clip.length);
        }
    }

    protected abstract void Movement();

    protected abstract void Attack(Collision collision);

}
