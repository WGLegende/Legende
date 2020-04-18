using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    FireWall instance ;
    ParticleSystem fireGround;
  
    public int degat;
    public float dureeCycle;
    public bool isActive;

    float timer;
    bool toggle;
   
    void Start()
    {
        instance = this;
        fireGround = GetComponent<ParticleSystem>();
    }

   
    void Update(){

        if (isActive){
            timer += Time.deltaTime;
                if (timer >= dureeCycle){
                    timer = 0;
                    toggle = !toggle;
                    if (toggle){
                        fireOn();
                    }else{
                        fireOff();
                    }
                }
        }
        if(!isActive){
            fireOff();
        }
    }


    void fireOn(){
        fireGround.Play();   
    }

    void fireOff(){
        fireGround.Stop();
    }

    void OnParticleCollision(GameObject other){

        if (other.name == "Player"){
            Obstacle_Manager.instance.DegatPlayer(-degat);
        }
    }
    
        
    
}
