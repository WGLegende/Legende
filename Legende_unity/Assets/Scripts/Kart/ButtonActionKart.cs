using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionKart : MonoBehaviour
{
    public static ButtonActionKart instance;
   
    public Transform cam;
    public Text text_button_action;
     public GameObject container;
   
    void Start(){

        instance = this; 

    } 


    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

  
    public void Action(string value){
       
         text_button_action.text = value;
        container.SetActive(true);
        
    }

    public void Hide(){
       
         container.SetActive(false);

        
    }





}
