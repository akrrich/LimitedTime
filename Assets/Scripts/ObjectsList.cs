using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectsList : MonoBehaviour
{
    private GameObject imageList;

    [SerializeField] private string[] objectsTag;

    [SerializeField] private TMP_Text[] objectsTextAmount;

    private GameObject[] objectsWithTag;


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


    public void ObjectsAmount()
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag(objectsTag[i]);

            objectsTextAmount[i].text = objectsWithTag.Length.ToString();

            if (objectsTextAmount[i].text == "0")
            {
                objectsTextAmount[i].text = "";

                Transform parentTransform = objectsTextAmount[i].transform;
                Transform childTransform = parentTransform.Find("Tilde");

                GameObject childGameObject = childTransform.gameObject;
                childGameObject.SetActive(true);
            }
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
                    listMode = true;
                }
            }

            else if (listMode)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    listMode = false;
                }
            }
        }
    }
}
