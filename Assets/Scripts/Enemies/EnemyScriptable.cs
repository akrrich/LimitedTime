using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyName", menuName = "Flyweight/EnemyData", order = 1)]

public class EnemyScriptable : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    public float Speed { get => speed; set => speed = value;}
    public float Radius { get => radius; set => radius = value;}
}
