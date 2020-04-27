using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    ParticleSystem particule;
    Transform checkPointPosition;
    bool justOnce;
    
    void Start(){
        
        instance = this;
        particule = GetComponent<ParticleSystem>();
        checkPointPosition = GetComponent<Transform>();    
    }

    void OnTriggerEnter(Collider other){

        if (other.name == "Player"){

            if(!justOnce){

                particule.startColor = Color.green;
                level_main.instance.CheckpointSavePosition(checkPointPosition);
                justOnce = true; 
            }
            
        }
         
    }


}
