using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject fireParticle;
  
    void Start()
    {
        rb.AddForce(transform.forward*2000);
       GameObject a= Instantiate(fireParticle,this.transform.position, Quaternion.identity);
       ParticleSystem fireParticleSystem = a.GetComponent<ParticleSystem>();
       fireParticleSystem.Play();
       Destroy(a,2);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject b= Instantiate(hitParticle,this.transform.position, Quaternion.identity);
        ParticleSystem hitPointParticleSystem = b.GetComponent<ParticleSystem>();
        hitPointParticleSystem.Play();
        Destroy(b,2);
        Destroy(this.gameObject);
    }
}
