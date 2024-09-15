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

    private static event Action _OnInfoTextHide;
    public static Action OnInfoTextHide { get { return _OnInfoTextHide; } set { _OnInfoTextHide = value; } }

    private static event Action _OnInfoTextShow;
    public static Action OnInfoTextShow { get { return _OnInfoTextShow; } set { _OnInfoTextShow = value; } }

    private static event Action _OnObjectDestroy;
    public static Action OnObjectDestroy { get { return _OnObjectDestroy; } set { _OnObjectDestroy = value; } }


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
                    dynamicCollider.Spc.enabled = false;
                    break;

                case ColliderType.Capsule:
                    dynamicCollider.Cpc.enabled = false;
                    break;
            }

            Destroy(gameObject, itemPickUpSound.clip.length);
        }
    }

    void OnDestroy()
    {
        _OnObjectDestroy?.Invoke();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            _OnInfoTextShow?.Invoke();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            _OnInfoTextHide?.Invoke();
        }
    }
}
