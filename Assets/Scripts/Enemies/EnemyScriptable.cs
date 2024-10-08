using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyName", menuName = "Flyweight/EnemyData", order = 1)]

public class EnemyScriptable : ScriptableObject
{
    [SerializeField] private int life;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    public int Life { get =>  life; set => life = value;}
    public float Speed { get => speed; set => speed = value;}
    public float Radius { get => radius; set => radius = value;}
}
