using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisCone : MonoBehaviour
{
    RatController rc;
    AudioSource aS;

    void Awake()
    {
        rc = GetComponentInParent<RatController>();
        aS = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            aS.pitch = Random.Range(0.8f, 1.2f);
            aS.Play();
            rc.RunFromPlayer();
        }
    }
}
