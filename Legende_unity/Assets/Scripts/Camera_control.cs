using UnityEngine;
using System.Collections;
using Cinemachine;

public class Camera_control : MonoBehaviour
{
    public static Camera_control instance;

    public CinemachineFreeLook player_camera;
    public CinemachineFreeLook player_kart_camera;

   
    void Start(){ 

        if(instance == null){
            instance = this; 
        }  
    }

    public void CameraBehindPlayer(){

        if( hinput.anyGamepad.rightStick.position.x == 0 &&  hinput.anyGamepad.rightStick.position.y == 0){ // si pas de rotation camera avec joystick, on recentre
        StartCoroutine(RecenterCamera());
        }
    }

    
    public IEnumerator RecenterCamera(){
        
        player_camera.m_RecenterToTargetHeading.m_enabled = true;
        player_camera.m_YAxisRecentering.m_enabled = true;

        yield return new WaitForSeconds(0.5f);

        player_camera.m_RecenterToTargetHeading.m_enabled = false;
        player_camera.m_YAxisRecentering.m_enabled = false;
        yield return null;
    }
     
}