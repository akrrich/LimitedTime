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

    protected int id;
    protected float durationPowerUp;

    public int Id { get => id; }


    protected virtual void Start()
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivePowerUp(collision);

            mr.enabled = false;
            boxCollider.enabled = false;

            PowerUpSound.Play();
            
            Destroy(gameObject, PowerUpSound.clip.length);
        }
    }

    protected abstract void ActivePowerUp(Collision collision);
}
