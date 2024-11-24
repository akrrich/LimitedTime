using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPool bulletPool;

    private Rigidbody rb;
    private MeshRenderer mr;
    private CapsuleCollider capsule;
    private AudioSource audioShoot;

    private float speed = 30f;
    private float lifeTime = 3f;


    void Start()
    {
        GameManager.Instance.GameStatePlaying += UpdateBullet;  
    }

    void UpdateBullet()
    {
        PauseManager.Instance.PauseAndUnPauseSounds(audioShoot);

        transform.Rotate(0, 0, 750 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
            mr.enabled = false;
            capsule.enabled = false;

            StartCoroutine(ReturnToPoolAfterAudio());
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateBullet;
    }


    public void InstantiateBullet(Transform cameraTransform, BulletPool pool)
    {
        bulletPool = pool;
        Vector3 cameraForward = cameraTransform.forward;
        Initialize(cameraForward.normalized);
        transform.position = cameraTransform.position + cameraForward;

        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y + 90, 0);
    }


    private void Initialize(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        capsule = GetComponent<CapsuleCollider>();
        audioShoot = GetComponent<AudioSource>();

        rb.isKinematic = false;
        mr.enabled = true;
        capsule.enabled = true;

        rb.velocity = direction * speed;

        audioShoot.Play();

        Invoke("ReturnToPool", lifeTime);
    }

    private IEnumerator ReturnToPoolAfterAudio()
    {
        yield return new WaitWhile(() => audioShoot.isPlaying);

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        bulletPool.ReturnBulletToPool(this);
    }
}

