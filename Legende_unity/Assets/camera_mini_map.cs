using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mini_map : MonoBehaviour
{
   public Transform playerkart;
    

   
    void LateUpdate(){
        
          Vector3 newPosition = playerkart.position;
          newPosition.y = transform.position.y;
          transform.position = newPosition;

          transform.rotation = Quaternion.Euler(90f,playerkart.eulerAngles.y,0f);
        
    }
}
