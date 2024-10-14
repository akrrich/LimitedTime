using UnityEngine;

public class Manzillado : Enemies
{
    [SerializeField] private BulletManzillado bulletManzillado;

    private bool canShoot = false;

    private float counterForAttack = 0f;


    protected override void Start()
    {
        base.Start();

        life = 4;
    }

    protected override void Update()
    {
        base.Update();

        Attack();
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

            if (counterForAttack > 3f)
            {
                canShoot = true;
                counterForAttack = 0f;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isMovinmgForAttack = false;
            counterForAttack = 0f;
        }
    }


    protected override void Movement()
    {
        anim.SetFloat("Movements", 0.5f);

        Vector3 direction = (playerController.transform.position - transform.position).normalized;
        transform.position += direction * enemyScriptable.Speed * Time.deltaTime;   
    }

    protected override void Attack(Collision collision = null)
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
