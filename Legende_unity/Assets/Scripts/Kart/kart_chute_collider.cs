using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kart_chute_collider : MonoBehaviour
{
    public Transform kart;
    public Rigidbody rb;
    MeshRenderer display_gfx;

   
    void Start(){
        
        rb = GameObject.Find("kart").GetComponentInChildren<Rigidbody>();
        kart = GameObject.Find("kart").GetComponent<Transform>();
        display_gfx = GetComponent<MeshRenderer>();
        display_gfx.enabled = false; 
    }


    void OnTriggerEnter(Collider collider){

        if(collider.name == "kart"){

            rb.isKinematic = false;
            rb.useGravity = true;
            kart_manager.instance.canMoveRecul = false;
            kart_manager.instance.canMoveAvance = false;
            StartCoroutine(level_main.instance.MoveKartToCheckpoint());  
            StartCoroutine(rotateKart());
        }
    }

    IEnumerator rotateKart(){

        float diff = (kart_manager.instance.vitesse_maximum - kart_manager.instance.vitesse_actuelle) + 1;

        while(true){

            kart.Rotate(Time.deltaTime * diff * 3,0,0,Space.Self); 

            if(kart_manager.instance.canMoveAvance){
                break;
            }
 
            yield return null;
        }
 
    }


  










}
