using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPosition : MonoBehaviour
{
    [SerializeField] private int id;

    private void Start()
    {
        AbstractFactory.CreateObject(id, transform);
    }
}
