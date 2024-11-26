using UnityEngine;
using System;

public abstract class Enemies : MonoBehaviour
{
    protected PlayerController playerController;

    private PatrolNode currentNode; // Nodo en el que está el enemigo
    private PatrolNode targetNode; // Nodo hacia el cual se dirige el enemigo

    private bool isPatrolling = true; // Controla si el enemigo patrulla
    private bool playerDetected = false; // Controla si el jugador está en el radio

    [SerializeField] private float patrolSpeed = 2f; // Velocidad de patrullaje
    [SerializeField] private float followSpeed = 3f; // Velocidad al seguir al jugador
    protected bool isMovinmgForAttack = false;
    // Para la animación y sonido
    protected Animator anim;
    protected AudioSource[] enemiesAudios;

    [SerializeField] protected EnemyScriptable enemyScriptable;

    private BoxCollider boxCollider;
    private SpriteRenderer spriteMiniMap;

    protected int life;
    private bool dieAnimation = false;

    private static event System.Action onPlayerDefeated;
    public static System.Action OnPlayerDefeated { get => onPlayerDefeated; set => onPlayerDefeated = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponentInChildren<Animator>();
        enemiesAudios = GetComponentsInChildren<AudioSource>();

        boxCollider = GetComponent<BoxCollider>();
        spriteMiniMap = GetComponentInChildren<SpriteRenderer>();

        // Asumimos que el primer nodo es donde empieza el enemigo
        currentNode = FindObjectOfType<PatrolNode>(); // Suponemos que hay al menos un nodo en la escena
        targetNode = currentNode;

        // Comienza patrullando
        Patrol();

        anim.transform.LookAt(playerController.transform);

        GameManager.Instance.GameStatePlaying += UpdateEnemies;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            CheckPlayerDetection(); // Verificar si el jugador está en el radio

            if (playerDetected)
            {
                FollowPlayer(); // Seguir al jugador
            }
            else
            {
                Patrol(); // Patrullar entre los nodos
            }
        }

        foreach (AudioSource audios in enemiesAudios)
        {
            PauseManager.Instance.PauseAndUnPauseSounds(audios);
        }
    }

    // Función de patrullaje
    private void Patrol()
    {
        if (currentNode == null || targetNode == null)
            return;

        MoveTowardsNode(targetNode);

        // Comprobar si hemos llegado al nodo actual
        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.2f)
        {
            // Cambiar al siguiente nodo en el grafo
            targetNode = GetNextPatrolNode(currentNode);
            currentNode = targetNode;
        }
    }

    // Moverse hacia el siguiente nodo
    private void MoveTowardsNode(PatrolNode node)
    {
        Vector3 direction = (node.transform.position - transform.position).normalized;
        transform.position += direction * patrolSpeed * Time.deltaTime;

        anim.SetFloat("Movements", 0.5f); // Ajustar animación según la velocidad de movimiento
    }

    // Obtener el siguiente nodo de patrullaje
    private PatrolNode GetNextPatrolNode(PatrolNode node)
    {
        // Escoge un nodo aleatorio entre los nodos conectados
        int randomIndex = UnityEngine.Random.Range(0, node.connectedNodes.Count);
        return node.connectedNodes[randomIndex];
    }

    // Función para seguir al jugador
    private void FollowPlayer()
    {
        Vector3 direction = (playerController.transform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        anim.SetFloat("Movements", 0.5f); // Ajustar animación para seguir al jugador
        anim.transform.LookAt(playerController.transform);
    }

    // Detectar si el jugador está dentro del radio
    private void CheckPlayerDetection()
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) <= enemyScriptable.Radius)
        {
            playerDetected = true; // El enemigo detecta al jugador
        }
        else
        {
            playerDetected = false; // El enemigo deja de seguir al jugador
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            life -= playerController.Damage;
            if (life <= 0)
            {
                die();
            }
        }
    }

    // Función de muerte
    private void die()
    {
        if (life <= 0)
        {
            PlayerController.AddScore(50);
            dieAnimation = true;
            anim.SetFloat("Movements", 1.5f); // Animación de muerte

            enemiesAudios[1].Play(); // Sonido de muerte
            boxCollider.enabled = false;
            spriteMiniMap.enabled = false; // Desactivar sprite del minimapa

            // Matar al enemigo después de la duración del sonido
            Destroy(gameObject, enemiesAudios[1].clip.length);
            Destroy(anim.gameObject, 3f);
        }
    }
   

    // Detener animaciones cuando el jugador muere
    private void StopAnimationWhenPlayerDeaths()
    {
        anim.SetFloat("Movements", 0f);
    }

    // Función para rotar el sprite en el minimapa
    private void RotateMiniMapSprite()
    {
        Vector3 direction = playerController.transform.position - spriteMiniMap.transform.position;
        direction.y = 0;
        spriteMiniMap.transform.rotation = Quaternion.LookRotation(direction);
        spriteMiniMap.transform.rotation *= Quaternion.Euler(-90, 130, 0);
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateEnemies;
    }

    // Función para actualizar el comportamiento del enemigo
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
    protected abstract void Attack(Collision collision);
}
