using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    PlayerControls controls;
    Animation anim;
    bool area;
    bool isPositionUp;
  
    void Start(){

        area = false;
        isPositionUp = true;
        anim = GetComponent<Animation>();
    } 
       

   void Awake(){

        controls = new PlayerControls();
        controls.Gameplay.ButtonB.started += ctx => Action();
    }

 void OnEnable(){

        controls.Gameplay.Enable();
    }

     void OnDisable(){

        controls.Gameplay.Disable();
    }


    void OnTriggerEnter(){

        area = true;  
        GameObject.Find("TextButtonB").GetComponent<Text>().text = "Activer"; // maj du text
        GameObject.Find("ButtonActionText").GetComponent<Animator>().SetBool("actionTextPlayer",true);
        PlayerGamePad.canAttack = false;
    }
    
    void OnTriggerExit(){

       area = false;
       GameObject.Find("ButtonActionText").GetComponent<Animator>().SetBool("actionTextPlayer",false);
       GameObject.Find("TextButtonB").GetComponent<Text>().text = "Attaquer"; // maj du text
       PlayerGamePad.canAttack = true;

    }



    void Action(){ // function appelee par le bouton
       
        if (area){  // uniquement si on est devant le levier
           if (isPositionUp){
            anim.Play("elevatorDown");
           }else{
                anim.Play("elevatorUp");
           }
            GameObject.Find("ButtonActionText").GetComponent<Animator>().SetBool("actionTextPlayer",false);
        }
    }

    void elevatorPositionDown(){ // on appelle la fonction en fin d'amim
        isPositionUp = false;   
    }

    void elevatorPositionUp(){
        isPositionUp = true;  
    }
}
