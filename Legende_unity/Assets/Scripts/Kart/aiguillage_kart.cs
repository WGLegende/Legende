using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiguillage_kart : MonoBehaviour{

    public static aiguillage_kart instance; 

    public ChoixCircuit _choix_circuit;
    public enum ChoixCircuit{
        Droite,
        Gauche        
    }

    public Battlehub.MeshDeformer2.SplineBase rails, right_rails, left_rails;
   
    [HideInInspector] public bool toggle;
    [HideInInspector] public Animator anim;
    [HideInInspector] public AudioSource aiguillage_audio;

    [HideInInspector]public ParticleSystem indicator_particule;
    public GameObject particule_container;
    public Transform view_right_rails;
    public Transform view_left_rails;
    public Transform img_minimap;


    void Start(){
        if(instance == null){
            instance = this;
        } 
        camera_mini_map.instance.list_img_minimap.Add(img_minimap); 
         
        anim = GetComponent<Animator>();
        aiguillage_audio = GetComponentInChildren<AudioSource>();
        indicator_particule = GetComponentInChildren<ParticleSystem>();

        if(_choix_circuit == ChoixCircuit.Gauche){ // on initialise si chg de rail au demarrage
            AiguillageManager.instance.next_rails = left_rails;
            toggle = true;
            anim.SetBool("switch",true);
            StartCoroutine(moveParticuleView(particule_container,view_left_rails.transform.position, 0f));    
        }   
    }


    
    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "PlayerKart"){

            player_actions.instance.display_actions(this,collider); 
            indicator_particule.Play();  

                if(_choix_circuit == ChoixCircuit.Gauche){ 
                    AiguillageManager.instance.next_rails = left_rails;
                    StartCoroutine(moveParticuleView(particule_container,view_left_rails.transform.position, 0.4f));
                }
                else{
                    AiguillageManager.instance.next_rails = right_rails;
                    StartCoroutine(moveParticuleView(particule_container,view_right_rails.transform.position, 0.4f));
                }
          
            if(!AiguillageManager.instance.List_spline_rails.Contains(rails)){ // on save d'ou on vient
                AiguillageManager.instance.List_spline_rails.Add(rails); 
            }
        }   
    }

    void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "PlayerKart"){
            player_actions.instance.clear_action_kart(true); 
            indicator_particule.Stop(); 
        }    
    }



    IEnumerator moveParticuleView (GameObject objectToMove, Vector3 end, float seconds){

        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds){

        objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
        elapsedTime += Time.deltaTime;
        yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    public void switchLeft(){
        StartCoroutine(moveParticuleView(particule_container,view_left_rails.transform.position, 0.4f));
    }

    public void switchRight(){
        StartCoroutine(moveParticuleView(particule_container,view_right_rails.transform.position, 0.4f));
    }
}
