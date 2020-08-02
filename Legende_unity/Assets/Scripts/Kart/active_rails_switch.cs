using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class active_rails_switch : MonoBehaviour{

    
    public Transform rails;
    public BoxCollider collider_chute;
    public float duree;
    public AudioClip[] clips;

    AudioSource audio_source_rail;
    BoxCollider collider_switch;
    CinemachineVirtualCamera cam_rails;
    
    [HideInInspector] public Animator anim_levier;
    [HideInInspector] public AudioSource sound_levier;
   
    Vector3 rail_in_position = new Vector3 (0f,0f,0f);
  
    float elapsedTime = 0;
    

    void Start(){

      
        anim_levier = GetComponent<Animator>(); 
        sound_levier = GetComponent<AudioSource>();

        audio_source_rail = rails.GetComponentInChildren<AudioSource>(); 
        collider_switch = GetComponent<BoxCollider>();
        cam_rails = GetComponentInChildren<CinemachineVirtualCamera>();
        cam_rails.LookAt = audio_source_rail.GetComponent<Transform>();  
    }


    private void Update(){

        if(Input.GetKeyDown("a"))  {
           
        }       

    }

    void OnTriggerEnter(Collider collider){ 
        player_actions.instance.display_actions(this,collider);     
    }
  
   
    void OnTriggerExit(Collider collider){
        player_actions.instance.clear_action(collider.tag == "Player");  
    }

    
    public IEnumerator active_rail(){

        player_gamePad_manager.instance.PlayerCanMove(false);
        sound_levier.Play();
        anim_levier.SetBool("active_levier",true);

        yield return new WaitForSeconds(1f);

        cam_rails.Priority = 12;

         yield return new WaitForSeconds(1f);

        audio_source_rail.clip = clips[0];
        audio_source_rail.Play();
        collider_switch.enabled = false;

        Vector3 currentPosition = rails.transform.localPosition;
        while(elapsedTime < 1){

            elapsedTime += Time.deltaTime / duree;
            rails.transform.localPosition = Vector3.Lerp(currentPosition,rail_in_position, elapsedTime);
            yield return null;
        }
       
        collider_chute.enabled = false;
        audio_source_rail.clip = clips[1];
        audio_source_rail.Play();

        yield return new WaitForSeconds(1f);

        player_gamePad_manager.instance.PlayerCanMove(true);
        cam_rails.Priority = 0;
    }

}
