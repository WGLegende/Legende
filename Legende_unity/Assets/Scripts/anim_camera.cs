using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_camera : MonoBehaviour{

    
    public anim_camera Instance;

    public float rayon_trigger = 10f;
    public float vitesse_de_deplacement = 6f;
    public SphereCollider col;
   
    public Cinemachine.CinemachineDollyCart camControl;




    void Start(){
        Instance = this;
        col.radius = rayon_trigger;
        camControl.m_Speed = vitesse_de_deplacement;
    }

   
    public void OnTriggerEnter(Collider other){

        if (other.name == "Player"){
            print("start cam anim");
        }
       
    }

    void OnDrawGizmosSelected(){

        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(transform.position,rayon_trigger);
    }



}
