using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bu bulletPrefab;
     
    private Cola<Bu> bulletPool = new Cola<Bu>();

    private static event Action onReloadingAutomatic;
    public static Action OnReloadingAutomatic { get { return onReloadingAutomatic; } set { onReloadingAutomatic = value; } }


    private int initialPoolSize = 15;

    private int totalBullets = 60;
    private int counterBullets = 15;

    public int TotalBullets {  get { return totalBullets; } set { totalBullets = value; } }
    public int CounterBullets { get { return counterBullets; } set { counterBullets = value; } }


    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Bu bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.gameObject.SetActive(false);
            bulletPool.Enqueue(bulletInstance); 
        }
    }

    void Update()
    {
        if (counterBullets <= 0 && totalBullets >= 1)
        {
            onReloadingAutomatic?.Invoke();
        }
    }

    public Bu GetBullet()
    {
        Bu bullet;

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

    public void ReturnBulletToPool(Bu bullet)
    {
        bullet.gameObject.SetActive(false); 
        bulletPool.Enqueue(bullet);
    }
}

