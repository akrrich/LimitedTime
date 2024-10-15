using UnityEngine;
using System;

public class Objects : MonoBehaviour
{
    private DynamicCollider dynamicCollider;

    private AudioSource itemPickUpSound;
    private SpriteRenderer spriteMiniMap;

    [SerializeField] private ColliderType colliderType;

    [SerializeField] private int id;
    public int Id { get { return id; } }


    private static event Action onInfoTextHide;
    public static Action OnInfoTextHide { get { return onInfoTextHide; } set { onInfoTextHide = value; } }

    private static event Action onInfoTextShow;
    public static Action OnInfoTextShow { get { return onInfoTextShow; } set { onInfoTextShow = value; } }

    private static event Action onObjectDestroy;
    public static Action OnObjectDestroy { get { return onObjectDestroy; } set { onObjectDestroy = value; } }


    private bool isPlayerInRange = false;


    void Start()
    {
        dynamicCollider = gameObject.AddComponent<DynamicCollider>();
        dynamicCollider.SetColliderType(colliderType);
        dynamicCollider.InitializeCollider();

        itemPickUpSound = GetComponent<AudioSource>();
        spriteMiniMap = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        spriteMiniMap.transform.rotation = Quaternion.Euler(-90, 0, 0);

        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired && isPlayerInRange && Input.GetKey(KeyCode.E))
        {
            isPlayerInRange = false;

            itemPickUpSound.Play();
            spriteMiniMap.enabled = false;

            dynamicCollider.Ms.enabled = false;

            switch (colliderType)
            {
                case ColliderType.Box:
                    dynamicCollider.Box.enabled = false;
                    break;

                case ColliderType.Sphere:
                    dynamicCollider.Sphere.enabled = false;
                    break;

                case ColliderType.Capsule:
                    dynamicCollider.Capsule.enabled = false;
                    break;
            }

            Destroy(gameObject, itemPickUpSound.clip.length);
        }

        PauseManager.PauseAndUnPauseSounds(itemPickUpSound);
    }

    void OnDestroy()
    {
        onObjectDestroy?.Invoke();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            onInfoTextShow?.Invoke();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            onInfoTextHide?.Invoke();
        }
    }
}
