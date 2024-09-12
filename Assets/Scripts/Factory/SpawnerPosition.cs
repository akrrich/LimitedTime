using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPosition : MonoBehaviour
{
    public int id;

    private void Start()
    {
        FactoryObjects.CreateObject(id, transform);
    }
}
