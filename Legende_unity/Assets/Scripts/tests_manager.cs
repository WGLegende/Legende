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

        this.transform.Translate(Vector3.right * Time.deltaTime * 0.5f, Space.World);
    
    }

    public string superTest(string entree){

        if(entree == "fluffe"){
            return "FLUFFALE";
        }else{
            return "TAPIR";
        }
    }

            
}
