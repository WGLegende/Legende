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
    }
    
    void OnTriggerExit(){

       area = false;  
    }



    void Action(){
       
       if (area){
         toggle =!toggle;
         anim.SetBool("pushLevier", toggle);
          GameObject.Find("DollyCart1").GetComponent<Animator>().SetBool("startWagon",toggle);
        }
    }
}
