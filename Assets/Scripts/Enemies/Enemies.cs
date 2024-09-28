using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    private MeshRenderer mr;
    private Animator anim;
    private AudioSource[] enemiesAudios;

    protected int life;
    protected float speed;

    protected float radius;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mr = GetComponent<MeshRenderer>();
        //anim = GetComponentInChildren<Animator>();
        enemiesAudios = GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
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

            die();
        }
    }

    private void CalculateDistance()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= radius)
        {
            Attack();
        }
    }

    private void die()
    {
        if (life <= 0)
        {
            enemiesAudios[0].Play();

            AbstractFactory.CreatePowerUp(Random.Range(0, 0), transform);

            mr.enabled = false;
            boxCollider.enabled = false;

            Destroy(gameObject, enemiesAudios[0].clip.length);
        }
    }

    private void InstantiatePowerUp()
    {

    }

    protected abstract void Attack();
}
