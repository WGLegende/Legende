using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChariot : MonoBehaviour
{
    PlayerControls controls;
    public GameObject Player;
    Rigidbody Chariot_rigidBody;
    Rigidbody player_rigidBody;
    Animator Chariot_Animator;
    public int ForceJump;

    bool area;


    void Start(){

        Chariot_rigidBody = GetComponent<Rigidbody>();
        Chariot_Animator = GetComponent<Animator>();  
        player_rigidBody = Player.GetComponent<Rigidbody>();
       
    }




    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject == Player){

            Player.transform.parent = transform;
            player_rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            PlayerGamePad.canJump = false;  
        }
        area = true;
    }
    
        
    

    private void OnTriggerExit(Collider other){
        
        if(other.gameObject == Player){

            Player.transform.parent = null;
            player_rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            PlayerGamePad.canJump = true; 
        }
        area = false;
    }

    void Awake(){

        controls = new PlayerControls();
        controls.Gameplay.ButtonX.started += ctx => Jump();
    }

    void OnEnable(){

        controls.Gameplay.Enable();
    }

    void OnDisable(){

        controls.Gameplay.Disable();
    }


    void Jump(){

        if(area){

            Chariot_rigidBody.AddForce(new Vector3(0,ForceJump,0), ForceMode.Impulse);
            print("ChariotJump");
        }
    }
    
}
