using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGroundBlock : MonoBehaviour
{
    [SerializeField] int jumpsToBreak = 1;
    [SerializeField] ParticleSystem ParticleSystem;

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

    private void OnTriggerExit(Collider other)
    {
        Break();
    }

    private void Break()
    {
        if (--jumpsToBreak <= 0)
        {
            // TODO
            PlayBreakingVFX();
            Destroy(gameObject);
        }
    }

    private void PlayBreakingVFX()
    {
        ParticleSystem particle = Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
    }
}
