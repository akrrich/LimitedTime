using UnityEngine;

public class Manzillado : Enemies
{
    [SerializeField] private BulletManzillado bulletManzillado;

    private bool canShoot = false;

    private float counterForAttack = 0f;


    protected override void Start()
    {
        base.Start();

        life = 5;
    }

    protected override void UpdateEnemies()
    {
        base.UpdateEnemies();

        Attack(null);

        if (playerController.Life <= 0)
        {
            OnPlayerDefeated?.Invoke();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            canShoot = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isMovinmgForAttack = true;

            counterForAttack += Time.deltaTime;

            if (counterForAttack >= 3f)
            {
                counterForAttack = 0f;
                canShoot = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            canShoot = false;
            isMovinmgForAttack = false;
            counterForAttack = 0f;
        }
    }


   

    protected override void Attack(Collision collision)
    {
        if (canShoot && life >= 1)
        {
            canShoot = false;

            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            float offsetDistanceX = 1f;
            float OffsetDistanceY = 2f;

            Vector3 spawnPosition = transform.position + direction * offsetDistanceX + new Vector3(0, OffsetDistanceY, 0);

            Quaternion bulletRotation = Quaternion.LookRotation(direction);
            bulletRotation *= Quaternion.Euler(90, 90, 90);

            BulletManzillado newBullet = Instantiate(bulletManzillado, spawnPosition, bulletRotation);
            newBullet.Direction = direction;
        }
    }
}
