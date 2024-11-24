using UnityEngine;
using System.Collections;

public class BulletManzillado : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer mr;
    private CapsuleCollider capsule;
    private AudioSource audioShoot;

    private Vector3 direction;

    private float speed = 17.5f;
    private float lifeTime = 3f;

    private string[] enemiesTags = { "EnemieDeforme", "EnemieManzillado" };

    public Vector3 Direction { get =>  direction; set => direction = value; }   


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        capsule = GetComponent<CapsuleCollider>();
        audioShoot = GetComponent<AudioSource>();

        audioShoot.Play();
        StartCoroutine(DestroyBulletAfterLifeTime());

        GameManager.Instance.GameStatePlaying += UpdateBulletManzillado;
    }

    void UpdateBulletManzillado()
    {
        PauseManager.Instance.PauseAndUnPauseSounds(audioShoot);

        transform.position += direction * speed * Time.deltaTime;        
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateBulletManzillado;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in enemiesTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                return;
            }
        }

        rb.isKinematic = true;
        mr.enabled = false;
        capsule.enabled = false;

        Destroy(gameObject, audioShoot.clip.length);
    }

    private IEnumerator DestroyBulletAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
