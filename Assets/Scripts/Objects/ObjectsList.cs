using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectsList : MonoBehaviour
{
    private GameObject imageList;

    [SerializeField] private string[] objectsTag;

    [SerializeField] private List<TMP_Text> objectsTextAmount;

    private GameObject[] objectsWithTag;

    private static event Action onAllObjectsFound;
    public static Action OnAllObjectsFound { get { return onAllObjectsFound; } set { onAllObjectsFound = value; } }


    private bool listMode = false;


    void Start()
    {
        imageList = transform.Find("imageList").gameObject;

        Objects.OnObjectDestroy += ObjectsAmount;

        for (int i = 0; i < objectsTag.Length; i++)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag(objectsTag[i]);

            objectsTextAmount[i].text = objectsWithTag.Length.ToString();
        }
    }

    void Update()
    {
        imageList.SetActive(listMode);

        ObjectsAmount();
        EnabledAndDisabledList();
    }

    void OnDestroy()
    {
        Objects.OnObjectDestroy -= ObjectsAmount;
    }


    private void ObjectsAmount()
    {
        bool allObjectsFound = true;

        for (int i = 0; i < objectsTag.Length; i++)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag(objectsTag[i]);

            objectsTextAmount[i].text = objectsWithTag.Length.ToString();

            if (objectsTextAmount[i].text == "0")
            {
                objectsTextAmount[i].text = null;

                Transform parentTransform = objectsTextAmount[i].transform;
                Transform childTransform = parentTransform.Find("Tilde");

                GameObject childGameObject = childTransform.gameObject;
                childGameObject.SetActive(true);
            }

            if (!string.IsNullOrEmpty(objectsTextAmount[i].text))
            {
                allObjectsFound = false;
            }
        }

        if (allObjectsFound)
        {
            onAllObjectsFound?.Invoke();
        }
    }

    private void EnabledAndDisabledList()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            if (!listMode)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    GameManager.Instance.ShowMira = false;
                    listMode = true;
                }
            }

            else if (listMode)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    GameManager.Instance.ShowMira = true;
                    listMode = false;
                }
            }
        }
    }
}
