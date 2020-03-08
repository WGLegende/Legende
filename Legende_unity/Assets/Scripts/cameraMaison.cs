using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMaison : MonoBehaviour
{

     public GameObject cameraHouse;
    

    void OnTriggerEnter(){

      cameraHouse.SetActive(true);
      // GameObject.Find ("player_camera").GetComponent<Animator>().SetBool("insideMaison", true); 
      
    }

    void OnTriggerExit(){

       // GameObject.Find ("player_camera").GetComponent<Animator>().SetBool("insideMaison", false); 
       cameraHouse.SetActive(false);
      

    }
}
