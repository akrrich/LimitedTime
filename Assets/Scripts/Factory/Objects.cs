using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Runtime.CompilerServices;

public class Objects : MonoBehaviour
{
    private DynamicCollider dynamicCollider;

    private AudioSource itemPickUpSound;

    [SerializeField] private ColliderType colliderType;

    [SerializeField] private int id;
    public int Id { get { return id; } }

    private bool isPlayerInRange = false;

    private static event Action onInfoTextHide;
    public static Action OnInfoTextHide { get { return onInfoTextHide; } set { onInfoTextHide = value; } }

    private static event Action onInfoTextShow;
    public static Action OnInfoTextShow { get { return onInfoTextShow; } set { onInfoTextShow = value; } }

    private static event Action onObjectDestroy;
    public static Action OnObjectDestroy { get { return onObjectDestroy; } set { onObjectDestroy = value; } }


    void Start()
    {
        dynamicCollider = gameObject.AddComponent<DynamicCollider>();
        dynamicCollider.SetColliderType(colliderType);
        dynamicCollider.InitializeCollider();

        itemPickUpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired && isPlayerInRange && Input.GetKey(KeyCode.E))
        {
            isPlayerInRange = false;

            itemPickUpSound.Play();

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
