using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierButton : MonoBehaviour
{

    PlayerControls controls;
    Animator anim;
    bool area;
    bool toggle;


    void Start(){

        anim = GetComponent<Animator>(); 
        area = false;
        toggle = false;
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
        GameObject.Find("DollyCart1").GetComponent<Animator>().SetBool("startWagon",toggle);
        GameObject.Find("TextButtonB").GetComponent<UnityEngine.UI.Text>().text = "Activer"; // maj du text
        PlayerGamePad.canAttack = false;

    }
    
    void OnTriggerExit(){

       area = false; 
       GameObject.Find("TextButtonB").GetComponent<UnityEngine.UI.Text>().text = "Attaquer"; // maj du text
       PlayerGamePad.canAttack = true;
    }



    void Action(){
       
       if (area){
         toggle =!toggle;
         anim.SetBool("pushLevier", toggle);
          GameObject.Find("DollyCart1").GetComponent<Animator>().SetBool("startWagon",toggle);
        }
    }
}
