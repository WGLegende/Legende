using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHealthEnemy : MonoBehaviour
{
  Transform cam;
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

}
