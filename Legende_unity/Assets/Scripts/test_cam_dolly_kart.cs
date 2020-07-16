using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class test_cam_dolly_kart : MonoBehaviour
{

    CinemachineVirtualCamera cinemachine;
    CinemachineTrackedDolly dolly;
   
    void Start(){
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        dolly = cinemachine.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update(){


        if(dolly.m_PathPosition > 47){

            cinemachine.Priority = 12;

        }

         if(dolly.m_PathPosition > 90){

            cinemachine.Priority = 8;

        }

         if(dolly.m_PathPosition > 480){

            cinemachine.Priority = 12;

        }

        if(dolly.m_PathPosition > 575){

            cinemachine.Priority = 8;

        }
        if(dolly.m_PathPosition > 776){

            cinemachine.Priority = 12;

        }

        if(dolly.m_PathPosition > 815){

            cinemachine.Priority = 8;

        }
        
    }
}
