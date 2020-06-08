using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public static ButtonAction instance;
    Animator anim;
    

    public Transform cam;
   


    void Start(){

        instance = this;
        anim = GetComponent<Animator>();
       
    } 


    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

  
    public void Action(string value){
       
        anim.SetBool("afficheButtonAction",true);
        GameObject.Find("ButtonTextAction").GetComponent<Text>().text = value;
        
    }

    public void Hide(){
       
        anim.SetBool("afficheButtonAction",false);
        
    }





}
