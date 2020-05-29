using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbtest : MonoBehaviour{

    public static climbtest instance;
    Transform playerObject;
    Animator animator;
    CharacterController controller;
    public float speed = 1f;
    public bool canClimb;

    void Start(){
        instance = this;
        playerObject = GameObject.Find("Player").GetComponent<Transform>();
        animator =  GameObject.Find("Player").GetComponent<Animator>();
        controller = GameObject.Find("Player").GetComponent<CharacterController> ();
    }

  
    void OnTriggerStay(Collider other){

        if (other.name == "Player"){

            if(canClimb){

                if (hinput.gamepad[0].leftStick.position.y > 0) {
                    playerObject.transform.Translate(Vector3.up * Time.deltaTime*speed);
                    animator.SetBool("stopClimb",false);
                    animator.SetBool("climb",true);
                }

                else if (hinput.gamepad[0].leftStick.position.y < 0 ) {
                    playerObject.transform.Translate(Vector3.down * Time.deltaTime*speed);
                    animator.SetBool("stopClimb",false);
                    animator.SetBool("climb",true);
                }

                if(hinput.gamepad[0].leftStick.position.y == 0 ){
                    animator.SetBool("stopClimb",true);
                    controller.enabled = false;
                }
            }
        }   
    }


    void OnTriggerEnter(Collider other){

        if (other.name == "Player"){

            if (hinput.gamepad[0].leftStick.position.y < 0){

                canClimb = false;
                player_gamePad_manager.canMove = true;
                controller.enabled = true;
                animator.SetBool("climb",false);
            }
            else{

                StartCoroutine(startClimb());}
            }   
    }

    void OnTriggerExit(Collider other){

        if (other.name == "Player"){

           

            if (hinput.gamepad[0].leftStick.position.y > 0 ) {

                StartCoroutine(EndClimb());
                playerObject.transform.Translate(Vector3.forward);
            }
            else{

                StartCoroutine(EndClimb());
                playerObject.transform.Translate(Vector3.back);
            }
        }   
    }

    IEnumerator startClimb(){

        canClimb = true;
        animator.SetBool("climb",true);
        player_gamePad_manager.canMove = false;
        controller.enabled = false;

        float timer = 1;

        while(timer < 2){

            playerObject.transform.rotation = Quaternion.Slerp (playerObject.transform.rotation, gameObject.transform.rotation, 6 * Time.deltaTime);
            playerObject.transform.position = new Vector3(gameObject.transform.position.x, playerObject.transform.position.y,gameObject.transform.position.z);
            timer += timer * Time.deltaTime * 2;
            yield return null;
        }
    }

    IEnumerator EndClimb(){

        animator.SetBool("climb",false);
        canClimb = false;

        yield return new WaitForSeconds(1f);

        player_gamePad_manager.canMove = true;
        controller.enabled = true;   
        yield return null;
    }
 
}
