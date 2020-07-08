using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_obstacle_belier : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("a")){   // Rempli la jauge vapeur TRICHE todo

    
          //  rb.isKinematic = false;
          //    rb.useGravity = true;
            rb.AddForce(Vector3.right,ForceMode.Impulse);
            // rb.AddExplosionForce(100f, transform.position, 50f);
           //  rb.AddForce(new Vector3(19f,0,0)); 
        }
    }
}
