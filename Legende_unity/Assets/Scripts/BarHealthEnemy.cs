using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHealthEnemy : MonoBehaviour
{
 
  public static  BarHealthEnemy instance;
  Transform cam;

    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        instance=this;
    }

    void Update()
    {
        
    }

      void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
    



    public void Hide(){

      gameObject.GetComponent<Canvas>().enabled = false; 
      print("hide bar")                ;
    }

    public void Show(){

      gameObject.GetComponent<Canvas>().enabled = true;                 
    }
}
