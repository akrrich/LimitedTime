using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Enemies : MonoBehaviour
{
    protected PlayerController playerController;
    public Graph graph; // Referencia al grafo
    public List<PatrolNode> allowedNodes; // Nodos permitidos para este enemigo
    public PatrolNode currentNode;
    public PatrolNode targetNode; // Un solo nodo objetivo
    private List<PatrolNode> path;
    private int currentPathIndex; // Índice del nodo en el camino

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

        if (graph != null && allowedNodes.Count > 0)
        {
            // Elegir un nodo aleatorio tanto para currentNode como para targetNode
            SetRandomCurrentNode();
            SetRandomTargetNode();
            
            
        }

        boxCollider = GetComponent<BoxCollider>();
        spriteMiniMap = GetComponentInChildren<SpriteRenderer>();

        anim.transform.LookAt(targetNode.transform.position);

        GameManager.Instance.GameStatePlaying += UpdateEnemies;
    }

    private void SetRandomCurrentNode()
    {
        if (allowedNodes == null || allowedNodes.Count == 0) return;

        // Elegir un nodo aleatorio para currentNode
        currentNode = allowedNodes[UnityEngine.Random.Range(0, allowedNodes.Count)];
    }

    private void SetRandomTargetNode()
    {
        if (allowedNodes == null || allowedNodes.Count == 0) return;

        PatrolNode newTargetNode;
        do
        {
            newTargetNode = allowedNodes[UnityEngine.Random.Range(0, allowedNodes.Count)];
        }
        while (newTargetNode == currentNode); // Asegurar que no sea igual al nodo actual

        targetNode = newTargetNode;
    }

    private void FindPathToTargetNode()
    {
        if (graph == null || currentNode == null || targetNode == null) return;

        path = graph.FindShortestPath(currentNode, targetNode);

        // Validar que el camino sea válido
        if (path == null || path.Count == 0)
        {
            Debug.LogWarning($"Path not found or empty from {currentNode.name} to {targetNode.name}.");
            path = null; // Asegúrate de que el enemigo no intente moverse con un camino inválido
            return;
        }

        currentPathIndex = 0; //  // Reiniciar índice del camino

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
        anim.SetFloat("Movements", 0.5f);
        if (path == null || path.Count == 0)
        {
            SetRandomTargetNode();
            FindPathToTargetNode();
            return;
        }

        if (currentPathIndex < 0 || currentPathIndex >= path.Count)
        {
            Debug.LogError($"Invalid path index: {currentPathIndex}. Path count: {path.Count}");
            currentPathIndex = 0; // Reiniciar índice si está fuera de rango
            return;
        }

        PatrolNode nextNode = path[currentPathIndex];
        Vector3 direction = (nextNode.transform.position - transform.position).normalized;
        transform.position += direction * 3 * Time.deltaTime;

        if (Vector3.Distance(transform.position, nextNode.transform.position) < 0.5f)
        {
            currentPathIndex++;

            if (currentPathIndex >= path.Count)
            {
                currentPathIndex = 0;
                SetRandomTargetNode();
                FindPathToTargetNode();
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 direction = (playerController.transform.position - transform.position).normalized;
        transform.position += direction * 3 * Time.deltaTime;

        anim.SetFloat("Movements", 0.5f); 
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
        PlayerController.AddScore(150);
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

            if (playerController.IsGrounded && playerController.PlayerAlive && playerDetected)
            {
                anim.transform.LookAt(playerController.transform);
                RotateMiniMapSprite();
            }

            if (!playerDetected)
            {
                anim.transform.LookAt(targetNode.transform.position);
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

