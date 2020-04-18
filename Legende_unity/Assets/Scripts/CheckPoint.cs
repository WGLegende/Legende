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

    void OnTriggerEnter(){

        if(!justOnce){
            particule.startColor = Color.green;
            Obstacle_Manager.instance.CheckPointSaveManager(checkPointPosition);
            justOnce = true; 
        }
         
    }


}
