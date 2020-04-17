using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class tests_manager : MonoBehaviour

{
 
 
    void Start() {

      string toto = "fludffe";

      Debug.Log(superTest(toto));





    }


    void Update () {

      
    
    }

    public string superTest(string entree){

        if(entree == "fluffe"){
            return "FLUFFALE";
        }else{
            return "TAPIR";
        }
    }

            
}
