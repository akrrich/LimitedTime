using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;   
    
    private Vector3 offset;           

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
