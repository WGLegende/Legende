using UnityEngine;
using System.Collections;
using Cinemachine;


public class Camera_control : MonoBehaviour
{
    public static Camera_control instance;

    public CinemachineFreeLook player_camera;
    public CinemachineFreeLook player_kart_camera;
    public CinemachineFreeLook current_camera;
    public CinemachineVirtualCamera cam_crash;
    public CinemachineVirtualCamera cam_ame;
    public CinemachineVirtualCamera cam_bow;
    GameObject player;
    CharacterController player_controller;
    

    [Header("EarthQuake Effect")]
    public bool activate_earthquake_effect;
    public float duree_max = 5f;
    public float force_max = 3f;
    public float frequence_min = 10f;
    public float frequence_max = 60f;

    
    void Start(){ 

        if(instance == null){
            instance = this; 
        }  
        // Camera Player
        player = player_main.instance.player;
        player_controller = player_main.instance.player.GetComponent<CharacterController>();
        player_camera = GameObject.Find("PlayerCameraController").GetComponent<CinemachineFreeLook>();
        player_camera.LookAt = player_main.instance.player.GetComponent<Transform>();
        player_camera.Follow = player_main.instance.player.GetComponent<Transform>();

        // Camera bow
        cam_bow = GameObject.Find("cam_bow").GetComponent<CinemachineVirtualCamera>();
        cam_bow.LookAt = player_main.instance.player.GetComponent<Transform>();
        cam_bow.Follow = player_main.instance.player.GetComponent<Transform>();


        // Camera Navy
        cam_ame = GameObject.Find("cam_navy").GetComponent<CinemachineVirtualCamera>();
        cam_ame.LookAt = GameObject.Find("ame_container").GetComponent<Transform>();
        cam_ame.Follow = player_main.instance.player.GetComponent<Transform>();

        // Camera Kart si playerKArt attache au player_main
        if(player_main.instance.playerKart != null){

            player_kart_camera = GameObject.Find("KartCameraController").GetComponent<CinemachineFreeLook>();
            player_kart_camera.LookAt = GameObject.Find("PlayerKart_container").GetComponent<Transform>();
            player_kart_camera.Follow = GameObject.Find("PlayerKart_container").GetComponent<Transform>();
            cam_crash = GameObject.Find("cam_crash").GetComponent<CinemachineVirtualCamera>();
            cam_crash.LookAt = GameObject.Find("kart").GetComponent<Transform>();
            cam_crash.Follow = GameObject.Find("Chariot_Container").GetComponent<Transform>();
         }else{
            Debug.Log("Pas de playerKart dans cette scene ?");
        }

        if(activate_earthquake_effect){
            StartCoroutine(start_earthquake());
        }
    }



    public void CameraBehindPlayer(){
        if(Hinput.anyGamepad.rightStick.position.x == 0 &&  Hinput.anyGamepad.rightStick.position.y == 0){ // si pas de rotation camera avec joystick, on recentre
            StartCoroutine(RecenterCamera());
        }
    }

    public IEnumerator CameraBehindKart(){
        
        player_kart_camera.m_RecenterToTargetHeading.m_enabled = true;
        player_kart_camera.m_YAxisRecentering.m_enabled = true;
        yield return new WaitForSeconds(0.5f);
        player_kart_camera.m_RecenterToTargetHeading.m_enabled = false;
        player_kart_camera.m_YAxisRecentering.m_enabled = false;
        yield return null;
    }

    public IEnumerator RecenterCamera(){ 
        player_camera.m_RecenterToTargetHeading.m_enabled = true;
        player_camera.m_YAxisRecentering.m_enabled = true;
        yield return new WaitForSeconds(0.5f);
        player_camera.m_RecenterToTargetHeading.m_enabled = false;
        player_camera.m_YAxisRecentering.m_enabled = false;
        yield return null;
    }


    
    public IEnumerator start_earthquake(){

        while(true){
            
            float _duree_max = Random.Range(1,duree_max);
            float _force_max = Random.Range(0.5f,force_max);
            float _cycle = Random.Range(frequence_min,frequence_max);
            StartCoroutine(shake_camera(_duree_max,_force_max));
            yield return new WaitForSeconds(_cycle);
        }  
    }


    public IEnumerator shake_camera(float duration, float magnitude){

        current_camera = player.activeSelf ? current_camera = player_camera : current_camera = player_kart_camera;

        float elapsed = 0.0f;
        float value = 0f;

        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[2]); 

        // fade in de la force
        while(value < magnitude){
            for(int i = 0; i < 3; i++){
                current_camera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = value; 
            }
            value += Time.deltaTime;
            yield return null;
        } 
      
        // duree de l'effet
        while(elapsed < duration){
            for(int i = 0; i < 3; i++){
                current_camera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = magnitude; 
            }
            elapsed += Time.deltaTime;
            player_controller.Move(Vector3.up * Time.deltaTime * 6f);  // on fait sauter le player
            yield return null;
        } 

        Player_sound.instance.FadeOutAndStopEvent(3f);
        // fade out de la force
        while(magnitude > 0){
            for(int i = 0; i < 3; i++){
                current_camera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = magnitude; 
            }
            magnitude -= Time.deltaTime;
            yield return null;
        }
        // stop effet
        for(int i = 0; i < 3; i++){
            current_camera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f; 
        }
    }
 
}