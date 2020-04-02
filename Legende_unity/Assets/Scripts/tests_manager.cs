using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tests_manager : MonoBehaviour
{


    public float vitesse;

    Transform monCube;


    public float vitesse_actuelle;
    public float  vitesse_demandee;
   
   

	// Use this for initialization
	 
	// Update is called once per frame
	void Update () {

            if (hinput.gamepad[0].A.justPressed){ // Recul

             print("A");
            }
             if (hinput.gamepad[0].B.justPressed){ // Avance

 print("b");
            }

             if (hinput.gamepad[0].X.justPressed){ // Jump

 print("x");
            }
             if (hinput.gamepad[0].Y.justPressed){ // Stop

 print("y");
            }
             if (hinput.gamepad[0].rightTrigger.justPressed){ // Boost

 print("bumper");
    
             }
        if (vitesse_actuelle < vitesse_demandee){

        vitesse_actuelle +=  Time.time* vitesse;
       // vitesse_actuelle = Mathf.Round(vitesse_actuelle * 100) / 100f;
       //print(vitesse_actuelle);
        }

        if (vitesse_actuelle > vitesse_demandee){
        
        vitesse_actuelle -=  Time.time* vitesse;
      //  vitesse_actuelle = Mathf.Round(vitesse_actuelle * 100) / 100f;
       // print(vitesse_actuelle);
        }
	}

  
        

            
}
