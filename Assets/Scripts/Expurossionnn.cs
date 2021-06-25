using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expurossionnn : MonoBehaviour
{
    bool hasExploded = false;
    public float radius = 5f;
    public float force = 700f;

    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    void Explode()
    {
        Debug.Log("boom");
        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation),1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        //get nearby objects
        //add force
        Destroy(gameObject);
    }
}
