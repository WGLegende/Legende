using UnityEngine;
using System.Collections;
using Cinemachine;
using Cinemachine.Utility;



public class Camera_control : MonoBehaviour
{
    public static Camera_control instance;
    public CinemachineFreeLook player_camera;

    float right_stick_x;
    float right_stick_y;
   
    void Start(){ 

        instance = this;   
    }

   
    public void CameraBehindPlayer(){

        right_stick_x = hinput.anyGamepad.rightStick.position.x;
        right_stick_y = hinput.anyGamepad.rightStick.position.y;

        if( right_stick_x == 0 && right_stick_y == 0){ // si pas de rotation camera avec joystick, on recentre

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