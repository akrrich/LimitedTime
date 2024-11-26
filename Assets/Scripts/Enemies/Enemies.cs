using UnityEngine;
using System;

public abstract class Enemies : MonoBehaviour
{
    protected PlayerController playerController;

    [SerializeField] protected EnemyScriptable enemyScriptable;

    private BoxCollider boxCollider;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    protected Animator anim;
    protected AudioSource[] enemiesAudios;
    private SpriteRenderer spriteMiniMap;

    protected int life;

    protected bool isMovinmgForAttack = false;
    private bool dieAnimation = false;

    private static event Action onPlayerDefeated;
    public static Action OnPlayerDefeated { get => onPlayerDefeated; set => onPlayerDefeated = value; }


    protected virtual void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        boxCollider = GetComponent<BoxCollider>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        enemiesAudios = GetComponentsInChildren<AudioSource>();
        spriteMiniMap = GetComponentInChildren<SpriteRenderer>();

        anim.transform.LookAt(playerController.transform);

        GameManager.Instance.GameStatePlaying += UpdateEnemies;
    }

    protected virtual void UpdateEnemies()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            if (!dieAnimation)
            {
                anim.transform.position = transform.position;
            }

            if (playerController.IsGrounded && playerController.PlayerAlive)
            {
                anim.transform.LookAt(playerController.transform);

                RotateMiniMapSprite();
            }

            if (isMovinmgForAttack && !dieAnimation)
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
            PauseManager.Instance.PauseAndUnPauseSounds(audios);
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateEnemies;
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
            PlayerController.AddScore(50);

            dieAnimation = true;
            isMovinmgForAttack = true;

            anim.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            anim.transform.SetParent(null);

            anim.SetFloat("Movements", 1.5f);
             
            enemiesAudios[1].Play();

            int randomNumer = UnityEngine.Random.Range(0, 5);

            if (randomNumer == UnityEngine.Random.Range(0, 5))
            {
                AbstractFactory.CreatePowerUp(UnityEngine.Random.Range(0, 3), transform);
            }

            boxCollider.enabled = false;
            spriteMiniMap.enabled = false;

            Destroy(gameObject, enemiesAudios[1].clip.length);
            Destroy(anim.gameObject, 3f);
        }
    }

    private void StopAnimationWhenPlayerDeaths()
    {
        anim.SetFloat("Movements", 0f);
    }

    private void RotateMiniMapSprite()
    {
        Vector3 direction = playerController.transform.position - spriteMiniMap.transform.position;
        direction.y = 0;
        spriteMiniMap.transform.rotation = Quaternion.LookRotation(direction);
        spriteMiniMap.transform.rotation *= Quaternion.Euler(-90, 130, 0);
    }

    protected abstract void Movement();

    protected abstract void Attack(Collision collision);
}
