using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
   private Transform target;
   public float speed = 3f;

   public void seek(Transform value){

           target = value;
   }
    void Update()
    {
       
        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

       transform.Translate(dir.normalized * distance, Space.World);
    }
}
