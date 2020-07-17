using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kart_jump_collider : MonoBehaviour
{

    public Rigidbody rb;
    public Animator anim_kart;

  
    void Start(){
        rb = GameObject.Find("kart").GetComponentInChildren<Rigidbody>();  
        anim_kart = GameObject.Find("kart").GetComponentInChildren<Animator>();  
    }

    void OnTriggerEnter(Collider collider){

        if(collider.name == "kart"){

            anim_kart.enabled = false;
            rb.isKinematic = false;
            rb.useGravity = true;
            kart_manager.instance.canMoveRecul = false;
            kart_manager.instance.canMoveAvance = false;
            Invoke("repositionkart",1f);
        }
    }


    void repositionkart(){

        anim_kart.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = false;
        level_main.instance.MoveKartToCheckpoint();  
    }










}
