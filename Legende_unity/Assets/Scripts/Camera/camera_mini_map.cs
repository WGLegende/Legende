using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mini_map : MonoBehaviour
{
    public static camera_mini_map instance;

    public Transform target;
    public bool rotation;

    Transform main_camera;
    Transform player_img_minimap;
    RectTransform cam_view_minimap;
    RectTransform north_view_minimap;

    public List<Transform> list_img_minimap = new List<Transform>();

  
    
    void Awake(){

        if(instance == null){
            instance = this;
        }
        player_img_minimap = GameObject.Find("player_img_minimap").GetComponent<Transform>();
        cam_view_minimap = GameObject.Find("pivot_view").GetComponent<RectTransform>();
        north_view_minimap = GameObject.Find("pivot_north").GetComponent<RectTransform>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        main_camera = GameObject.Find("Camera").GetComponent<Transform>();  
    }


    void LateUpdate(){

        // Gere le tracking de la target
        Vector3 newPosition = target.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // Gere la rotation de img vue camera
        if(!rotation){
            Vector3 compassRotation = cam_view_minimap.transform.eulerAngles;
            compassRotation.z = main_camera.eulerAngles.y;
            cam_view_minimap.transform.eulerAngles = compassRotation;
        }

        // gere la rotation de toutes les imgs
        if(rotation){

            transform.rotation = Quaternion.Euler(90f,main_camera.eulerAngles.y,0f);

            Vector3 compassRotation = north_view_minimap.transform.eulerAngles;
            compassRotation.z = main_camera.eulerAngles.y;
            north_view_minimap.transform.eulerAngles = compassRotation;

            foreach(Transform img in list_img_minimap){
                img.rotation = Quaternion.Euler(90f,main_camera.eulerAngles.y,0f);
            }
        }
    
    }


   

   
}
