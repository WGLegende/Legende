using UnityEngine;
using System.Collections;
  
public class player_foot_step: MonoBehaviour{

    public static player_foot_step instance;
    CharacterController controller;

    public float distanceGround = 0.5f;
    public Transform player;

    void Start(){

        if(instance == null){
            instance = this;
        }

        controller = player_main.instance.player.GetComponent<CharacterController>(); 
        StartCoroutine(checkTypeGround());
    }


    public IEnumerator checkTypeGround() {


        while(true){

            RaycastHit hit = new RaycastHit();

           // Debug.Log(isGrounded());
           // Debug.DrawRay((new Vector3(player.transform.position.x, transform.position.y + 1f,transform.position.z)), Vector3.down, Color.red, 5);

            if(controller.isGrounded == true){

                if(Physics.Raycast(transform.position, Vector3.down,out hit, 1.5f)){

                    Player_sound.instance.TypeSol = hit.collider.tag;

                }  
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.02f);    
        }
    }


    public void tets(){ // test anim coup d'epee
    
        Player_sound.instance.PlayFightFx(gameObject,Player_sound.instance.FightFx[0]);
    }

    bool isGrounded(){

        return Physics.Raycast(player.transform.position,Vector3.down, distanceGround);
    }

}