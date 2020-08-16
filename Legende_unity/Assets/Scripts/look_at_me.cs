using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class look_at_me : MonoBehaviour{


    public MultiAimConstraint head;
    public Transform player;
    public Transform target_head;

    public bool player_is_looking;
    public float speed_look = 3f;
    public float angle_de_vision = 160f;
   
    void Start(){
        //head = GameObject.Find("Head").GetComponent<MultiAimConstraint>();
        target_head = GameObject.Find("target_head").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

   

    void OnTriggerStay(Collider collider){ 

        if(collider.tag == "Player"){

            target_head.transform.position = this.transform.position;
            float angleVision = Vector3.Angle(player.transform.forward, this.transform.position - player.transform.position);

            if(!player_is_looking && Mathf.Abs(angleVision) <= angle_de_vision/2){
                StartCoroutine("moveHead");
            }
            if(player_is_looking && Mathf.Abs(angleVision) > angle_de_vision/2){
                StartCoroutine("stopLooking");
            }
        }  
    }

    void OnTriggerExit(Collider collider){ 

        if(collider.tag == "Player" && player_is_looking){
           StartCoroutine("stopLooking");
        }  
    }


    IEnumerator moveHead(){

        StopCoroutine("stopLooking");
        player_is_looking = true;

        while(head.weight < 0.99){
           // head.weight += Time.deltaTime * speed_look;
            head.weight = Mathf.Lerp(head.weight,1f,Time.deltaTime*speed_look);
            yield return null;
        }
    }

    IEnumerator stopLooking(){

        StopCoroutine("moveHead");
        player_is_looking = false;
        
        while(head.weight > 0.01f){
        //    head.weight -= Time.deltaTime * speed_look * 2;
            head.weight = Mathf.Lerp(head.weight,0f,Time.deltaTime*speed_look);

            yield return null;
        }
    }


    
}
