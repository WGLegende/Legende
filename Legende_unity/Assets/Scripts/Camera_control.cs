using UnityEngine;
using System.Collections;
using Cinemachine;
using Cinemachine.Utility;



public class Camera_control : MonoBehaviour
{
    public static Camera_control instance;
    CinemachineFreeLook Cm_cam;

    float right_stick_x;
    float right_stick_y;
   
    void Start(){ 

        instance = this;   
        Cm_cam = GetComponent<CinemachineFreeLook>();
    }

   
 
    public void CameraBehindPlayer(){

        right_stick_x = hinput.anyGamepad.rightStick.position.x;
        right_stick_y = hinput.anyGamepad.rightStick.position.y;

        if( right_stick_x == 0 && right_stick_y == 0){ // si pas de rotation camera avec joystick, on recentre

        StartCoroutine(RecenterCamera());
        }
    }

    
    public IEnumerator RecenterCamera(){

        Cm_cam.m_RecenterToTargetHeading.m_enabled = true;
        Cm_cam.m_YAxisRecentering.m_enabled = true;
       
        yield return new WaitForSeconds(0.5f);

        Cm_cam.m_RecenterToTargetHeading.m_enabled = false;
        Cm_cam.m_YAxisRecentering.m_enabled = false;
        
        yield return null;
    }
     
}