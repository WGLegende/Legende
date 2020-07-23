using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mini_map : MonoBehaviour
{
    public static camera_mini_map instance;
    public Transform target;
    public Transform cam_target;
    public bool rotation;
    public Transform player_img_minimap;
   // public Transform playerkart_img_minimap;

    public List<Transform> list_img_minimap = new List<Transform>();
    

    void Awake(){

        if(instance == null){
            instance = this;
        }
        player_img_minimap = GameObject.Find("player_img_minimap").GetComponent<Transform>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }


    void LateUpdate(){
        
        Vector3 newPosition = target.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //playerkart_img_minimap.rotation = Quaternion.Euler(90f,cam_target.eulerAngles.y,0f);


        if(rotation){

            transform.rotation = Quaternion.Euler(90f,cam_target.eulerAngles.y,0f);

            foreach(Transform img in list_img_minimap){

                img.rotation = Quaternion.Euler(90f,cam_target.eulerAngles.y,0f);
            }
        }
        
    }
}
