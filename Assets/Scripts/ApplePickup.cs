using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickup : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleSystem;

    [SerializeField] float zRotationSpeed = 1f;

    private bool isPickedup = false;

    Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        isPickedup = true;
        Rigidbody.velocity = Vector3.up;
        
        BurstVFX();
    }

    private void BurstVFX()
    {
        ParticleSystem particle = Instantiate(ParticleSystem, Rigidbody.position, Quaternion.identity);
        Destroy(particle, particle.main.duration);
        Destroy(gameObject);
    }
}
