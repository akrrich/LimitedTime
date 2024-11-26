using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Enemies : MonoBehaviour
{
    protected PlayerController playerController;

    [SerializeField] private List<PatrolNode> patrolNodes; // Lista de nodos configurables en el Inspector
    private int currentNodeIndex = 0; // Índice del nodo actual

    [SerializeField] private float patrolSpeed = 2f; // Velocidad de patrullaje
    [SerializeField] private float followSpeed = 3f; // Velocidad al seguir al jugador

    private bool isPatrolling = true; // Controla si el enemigo patrulla
    private bool playerDetected = false; // Controla si el jugador está en el radio
    protected bool isMovinmgForAttack = false;  
    protected Animator anim;
    protected AudioSource[] enemiesAudios;

    [SerializeField] protected EnemyScriptable enemyScriptable;

    private BoxCollider boxCollider;
    private SpriteRenderer spriteMiniMap;

    protected int life;
    private bool dieAnimation = false;

    private static event System.Action onPlayerDefeated;
    public static System.Action OnPlayerDefeated { get => onPlayerDefeated; set => onPlayerDefeated = value; }

    protected virtual void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponentInChildren<Animator>();
        enemiesAudios = GetComponentsInChildren<AudioSource>();

        boxCollider = GetComponent<BoxCollider>();
        spriteMiniMap = GetComponentInChildren<SpriteRenderer>();

        Patrol();

        anim.transform.LookAt(playerController.transform);

        GameManager.Instance.GameStatePlaying += UpdateEnemies;
    }

    protected virtual void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            CheckPlayerDetection();

            if (playerDetected)
            {
                FollowPlayer();
            }
            else
            {
                Patrol();
            }
        }

        foreach (AudioSource audios in enemiesAudios)
        {
            PauseManager.Instance.PauseAndUnPauseSounds(audios);
        }
    }

    private void Patrol()
    {
        if (patrolNodes == null || patrolNodes.Count == 0) return;

        // Obtener el nodo actual
        PatrolNode targetNode = patrolNodes[currentNodeIndex];

        MoveTowardsNode(targetNode);

        // Si llegamos al nodo, avanzar al siguiente
        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.2f)
        {
            currentNodeIndex = (currentNodeIndex + 1) % patrolNodes.Count; // Ciclo en la lista
        }
    }

    private void MoveTowardsNode(PatrolNode node)
    {
        Vector3 direction = (node.transform.position - transform.position).normalized;
        transform.position += direction * patrolSpeed * Time.deltaTime;

        anim.SetFloat("Movements", 0.5f); // Ajustar animación según la velocidad de movimiento
    }

    private void FollowPlayer()
    {
        Vector3 direction = (playerController.transform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        anim.SetFloat("Movements", 0.5f); // Ajustar animación para seguir al jugador
        anim.transform.LookAt(playerController.transform);
    }

    private void CheckPlayerDetection()
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) <= enemyScriptable.Radius)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            life -= playerController.Damage;
            if (life <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        PlayerController.AddScore(50);
        dieAnimation = true;

        anim.SetFloat("Movements", 1.5f); // Animación de muerte
        enemiesAudios[1].Play(); // Sonido de muerte
        boxCollider.enabled = false;
        spriteMiniMap.enabled = false; // Desactivar sprite del minimapa

        Destroy(gameObject, enemiesAudios[1].clip.length);
        Destroy(anim.gameObject, 3f);
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateEnemies;
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

            if (!dieAnimation)
            {
                anim.SetFloat("Movements", 1f);
            }
        }
        else
        {
            StopAnimationWhenPlayerDeaths();
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

    protected abstract void Attack(Collision collision);
}
