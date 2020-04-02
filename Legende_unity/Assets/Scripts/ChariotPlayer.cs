using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotPlayer : MonoBehaviour
{
  
    public static bool canForward;
    public static bool stop_obstacle;
    
    public bool equipement_Bouteille;
    public bool equipement_Belier;
    public bool equipement_light;
      
    public float vitesse_stop_du_chariot;
    public float vitesse_avancer_chariot;
    public float vitesse_reculer_chariot;
    public float vitesse_boost_du_chariot;

    public static float vitesse_demandee;
    public static float vitesse_actuelle;
    public float velocity_chariot;
   

    public Battlehub.MeshDeformer2.SplineFollow SplineFollow;

    Transform Chariot_ContainerRot;
    float angleChariot;

    Animation anim;

    void Start(){

        anim = gameObject.GetComponent<Animation>();
        canForward = true;
        vitesse_actuelle = 0;
        Chariot_ContainerRot = GameObject.Find("Chariot_Container").GetComponent<Transform>();   
    }


void FixedUpdate(){

       
}
    void Update(){

      // PHYSICS CHARIOT
        angleChariot = Chariot_ContainerRot.localEulerAngles.x;
        angleChariot = (angleChariot > 180) ? angleChariot - 360 : angleChariot;
        angleChariot = (Mathf.Round(angleChariot * 100f) / 100f);
        vitesse_demandee = angleChariot;
 

    //  SI BESOIN POUR MAPPING JOYCON
    //      for (KeyCode i = 0; i <= KeyCode.Joystick8Button19; i++)
    //      {
    //          if (Input.GetKey(i))
    //              Debug.Log(i);
    //  }


      // GESTION DES BOUTON
        if (hinput.gamepad[0].A.justPressed || Input.GetKey(KeyCode.Joystick3Button0)){ // Recul
                vitesse_demandee = vitesse_reculer_chariot;
        }
        
        if (hinput.gamepad[0].B.justPressed || Input.GetKey(KeyCode.Joystick3Button1)){ // Avance
            if(canForward){
                vitesse_demandee = vitesse_avancer_chariot;
            }
        }
        
        if (hinput.gamepad[0].X.justPressed || Input.GetKey(KeyCode.Joystick3Button2)){ // Jump 
            anim.Play("JumpChariot");
        }
        
        if (hinput.gamepad[0].Y.justPressed || Input.GetKey(KeyCode.Joystick3Button3)){ // Stop
            vitesse_demandee = vitesse_stop_du_chariot;
        }
       

        if (hinput.gamepad[0].leftTrigger.justPressed){ 
           
        }

        if (hinput.gamepad[0].rightTrigger.justPressed || Input.GetKey(KeyCode.Joystick3Button5)){ // Boost
            if(canForward){
                vitesse_demandee = vitesse_boost_du_chariot;        
            }
        }

     // COMMANDE DE LA VITESSE DU CHARIOT
        if (vitesse_actuelle < vitesse_demandee && canForward){
            
            vitesse_actuelle += Time.deltaTime* velocity_chariot;
            vitesse_actuelle = Mathf.Round(vitesse_actuelle * 1000f) / 1000f;

            if (vitesse_actuelle - vitesse_demandee < 0.1 && vitesse_demandee - vitesse_actuelle < 0.1 && vitesse_demandee ==0){
                vitesse_actuelle = 0;
                SplineFollow.Speed = vitesse_actuelle;
     
            }else{
                SplineFollow.Speed = vitesse_actuelle;
             
            }
        }

        if (vitesse_actuelle > vitesse_demandee){
            
            vitesse_actuelle -= Time.deltaTime* velocity_chariot;
            vitesse_actuelle = Mathf.Round(vitesse_actuelle * 1000f) / 1000f;
            SplineFollow.Speed = vitesse_actuelle;
          
            if(vitesse_actuelle < -2){
                canForward = true;
                stop_obstacle = false;
            }    
        }   

        if (stop_obstacle  && canForward && !equipement_Belier){
            canForward = false;
            SplineFollow.Speed = 0;
            vitesse_demandee = 0;
            vitesse_actuelle = 0;
        }
              
        

    }


    void OnTriggerEnter(Collider collider){
        Debug.Log("enterTag : " + collider.gameObject.tag); 

        if(collider.gameObject.tag == "CollisionRails"){ 
            collider.gameObject.GetComponent<rails_triggers>().touching_chariot(GetComponent<ChariotPlayer>());
        }
    }

    void OnTriggerStay(Collider collider){
    }
    
    void OnTriggerExit(Collider collider){

         //  Debug.Log("exit : " + collider.gameObject.name);
        if(collider.gameObject.layer == 10){
            Debug.Log("TOUCHE!!!");
        //     Player_Animator.SetBool("Grounded", false);
        //     Player_Animator.SetBool("initiate_jump", true); 
        }
    }


    
}
