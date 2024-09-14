using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryObjects : MonoBehaviour
{
    [SerializeField] private Objects[] objects;

    private static Dictionary<int, Objects> idObjects = new Dictionary<int, Objects>();

    void Awake()
    {
        foreach (var obj in objects)
        {
            idObjects.Add(obj.Id, obj);
        }
    }

    public static Objects CreateObject(int id, Transform pos)
    {
        if (!idObjects.TryGetValue(id, out Objects obj))
        {
            return null;
        }

        return Instantiate(obj, pos.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        idObjects.Clear();
    }
}
