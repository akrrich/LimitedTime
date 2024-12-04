using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Canvaaa : MonoBehaviour
{
    


    [SerializeField] private string[] objectsTag;

    [SerializeField] private List<TMP_Text> objectsTextAmount;
    [SerializeField] private List<TMP_Text> objectsTextShow2;

    private GameObject[] objectsWithTag;

    private static event Action onAllObjectsFound;
    public static Action OnAllObjectsFound { get => onAllObjectsFound; set => onAllObjectsFound = value; }

    private static event Action onGameComplete;
    public static Action OnGameComplete { get => onGameComplete; set => onGameComplete = value; }

    private Scene currentScene;

    


    void Start()
    {

        

        Objects.OnObjectDestroy += ObjectsAmount;

        for (int i = 0; i < objectsTag.Length; i++)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag(objectsTag[i]);

            objectsTextAmount[i].text = objectsWithTag.Length.ToString();
        }

        
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        ObjectsAmount();
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
            if (currentScene.name == "Level 2")
            {
                onGameComplete?.Invoke();
            }

            else
            {
                onAllObjectsFound?.Invoke();
            }
        }
    }

  
}
