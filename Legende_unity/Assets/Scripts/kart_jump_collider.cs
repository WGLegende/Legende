using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kart_jump_collider : MonoBehaviour
{

    public Rigidbody rb;

  
    void Start(){
        rb = GameObject.Find("kart").GetComponentInChildren<Rigidbody>();  
    }

    void OnTriggerEnter(Collider collider){

        if(collider.name == "kart"){

            rb.isKinematic = false;
            rb.useGravity = true;
            kart_manager.instance.canMoveRecul = false;
            kart_manager.instance.canMoveAvance = false;
            Invoke("repositionkart",1f);
        }
    }


    void repositionkart(){

        rb.isKinematic = true;
        rb.useGravity = false;
        level_main.instance.MoveKartToCheckpoint();  
    }










}
