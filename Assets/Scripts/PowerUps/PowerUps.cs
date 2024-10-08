using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    protected PowerUpsManager powerUpsManager;

    private Rigidbody rb;
    private MeshRenderer mr;
    private BoxCollider boxCollider; 
    private AudioSource PowerUpSound;

    [SerializeField] private int id;

    protected float durationPowerUp;

    public int Id { get => id; }


    protected virtual void Awake()
    {        
        powerUpsManager = FindObjectOfType<PowerUpsManager>();

        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        PowerUpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        PauseManager.PauseAndUnPauseSounds(PowerUpSound);

        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
        transform.Rotate(transform.rotation.x,  125 * Time.deltaTime, transform.rotation.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ActivePowerUp(collider);

            mr.enabled = false;
            boxCollider.enabled = false;

            PowerUpSound.Play();

            Destroy(gameObject, PowerUpSound.clip.length);
        }
    }

    protected abstract void ActivePowerUp(Collider collider);
}
