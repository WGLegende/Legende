using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_rails : MonoBehaviour{
    


    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "CollisionRails" && !kart_manager.instance.equipement_belier){ 
            kart_manager.instance.SplineFollow.IsRunning = false ;
            kart_manager.instance.canMoveAvance = false;
            //collider.gameObject.GetComponent<rails_triggers>().touching_chariot(GetComponent<ChariotPlayer>());
        }

        if(collider.gameObject.tag == "collision_lateral_left"){

            kart_manager.instance.anim_kart.enabled = false;
            kart_manager.instance.impact_left.Play();
            kart_manager.instance.audio_kart.clip = kart_manager.instance.clip_fx[3];
            kart_manager.instance.audio_kart.Play();
            Camera_control.instance.cam_crash.Priority = 12;
            kart_manager.instance.rb.isKinematic = false;
            kart_manager.instance.rb.useGravity = true;
            kart_manager.instance.rb.mass = 0.05f;
            kart_manager.instance.rb.AddForce(Vector3.back,ForceMode.Impulse);
            kart_manager.instance.canMoveRecul = false;
            kart_manager.instance.canMoveAvance = false;
            StartCoroutine(level_main.instance.MoveKartToCheckpoint()); // delai au start
        }

        if(collider.gameObject.tag == "collision_lateral_right"){

            kart_manager.instance.rb.isKinematic = false;
            kart_manager.instance.rb.useGravity = true;
            kart_manager.instance. rb.mass = 0.05f;
            kart_manager.instance.rb.AddForce(-Vector3.back,ForceMode.Impulse);
            kart_manager.instance.canMoveRecul = false;
            kart_manager.instance.canMoveAvance = false;
            StartCoroutine(level_main.instance.MoveKartToCheckpoint()); // delai au start
           
        }

        if(collider.gameObject.name == "Sphere"){ // todo test enemy rails

            kart_manager.instance.anim_kart.enabled = false;
            kart_manager.instance.impact_left.Play();
            kart_manager.instance.audio_kart.clip = kart_manager.instance.clip_fx[3];
            kart_manager.instance.audio_kart.Play();
            Camera_control.instance.cam_crash.Priority = 12;

            kart_manager.instance.rb.isKinematic = false;
            kart_manager.instance.rb.useGravity = true;
            kart_manager.instance.rb.mass = 0.05f;
            kart_manager.instance.rb.AddExplosionForce(60, transform.position,5);
            kart_manager.instance.canMoveRecul = false;
            kart_manager.instance.canMoveAvance = false;
            StartCoroutine(level_main.instance.MoveKartToCheckpoint()); // delai au start
        }
    }
}
