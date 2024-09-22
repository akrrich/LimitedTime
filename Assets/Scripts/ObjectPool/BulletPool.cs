using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private Cola<Bullet> bulletPool = new Cola<Bullet>();

    private int initialPoolSize = 30;

    private int counterBullets = 0;
    public int CounterBullets {  get { return counterBullets; } set { counterBullets = value; } }

    public static event Action onReloading;

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Bullet bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.gameObject.SetActive(false);
            bulletPool.Enqueue(bulletInstance);
        }
    }

    void Update()
    {
        print(counterBullets);

        if (counterBullets >= 15)
        {
            onReloading?.Invoke();
        }
    }

    public Bullet GetBullet()
    {
        Bullet bullet;

        if (!bulletPool.Empty())
        {
            bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true); 
            return bullet;
        }

        else
        {
            bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); 
        bulletPool.Enqueue(bullet);
    }
}

