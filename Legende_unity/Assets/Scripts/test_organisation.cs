using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_organisation : MonoBehaviour
{
      public enum type_collision 
    {
        aiguillage, 
        fait_des_degats,
        joueur_doit_sauter
    };
    public type_collision _type_collision;

    void Start(){

        if(_type_collision == type_collision.aiguillage){
                Debug.Log("c'est aiguillage !");
        }else{
            Debug.Log("c'est pas aiguillage........");
        }
    }



}
