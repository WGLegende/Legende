using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destructable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown("space")){

             destroy();
             print("space");
         }
    }


    public void destroy(){

        Instantiate(destructable,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
