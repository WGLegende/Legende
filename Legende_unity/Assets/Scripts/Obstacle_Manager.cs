using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Manager : MonoBehaviour
{
  
  public static Obstacle_Manager instance;
 


    void Start(){
        instance = this;
    }

   
    void Update(){
       
    }


    public void DegatPlayer(int value){

        Barre_de_Vie.instance.PvPlayer(value);
    }



}
