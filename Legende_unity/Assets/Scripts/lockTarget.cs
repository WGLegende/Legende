using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class lockTarget : MonoBehaviour
{
    public static lockTarget instance;

    CinemachineTargetGroup targetGroup;
    Transform player;
    [HideInInspector] public SphereCollider detection_collider;
    
    Transform TargetViewCamera;

    public float distance = 10f;
    float distanceWithTarget;
    public bool target_lock;

    public List<GameObject> enemies = new List<GameObject>();
    public GameObject TargetLock;
    int i = 0;

   
    void Start(){

        instance = this;
        targetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        detection_collider = GetComponent<SphereCollider>();
      
    }

  
    public void ChangeTarget(){

        if(hinput.anyGamepad.rightTrigger.justPressed){

            if(TargetLock != null){

                TargetLock.transform.Find("ArrowSelectionLock").gameObject.SetActive(false);
                targetGroup.RemoveMember(TargetViewCamera);
                
                i = (i + 1) % enemies.Count;
            
                TargetViewCamera = enemies[i].GetComponent<Transform>();
                targetGroup.AddMember(TargetViewCamera, 1, 2);
                enemies[i].transform.Find("ArrowSelectionLock").gameObject.SetActive(true);
                TargetLock = enemies[i];
            }  
        }
    }

    public void EndTargetLock(){

        if(TargetLock != null){

            TargetLock.transform.Find("ArrowSelectionLock").gameObject.SetActive(false);
        }

        detection_collider.enabled = false;
        enemies.Clear();
        TargetLock = null;
        targetGroup.RemoveMember(TargetViewCamera);
        TargetViewCamera = null; 
        target_lock = false;
    }

    
    void OnTriggerEnter(Collider other){

        if(other.tag == "Enemy"){

            for (i = 0; i < enemies.Count; i++){

                if(other.gameObject == enemies[i]){
                    return;
                }
            }
            enemies.Add(other.gameObject);
            FindClosestEnemy();
        }   
    }

    void OnTriggerExit(Collider other){

        if(other.tag == "Enemy"){

            for (i = 0; i < enemies.Count; i++){

                if (other.gameObject == enemies[i]){

                    if (enemies[i] == TargetLock){
                        TargetLock.transform.Find("ArrowSelectionLock").gameObject.SetActive(false);
                        TargetLock = null;
                        targetGroup.RemoveMember(TargetViewCamera);
                        TargetViewCamera = null;
                        detection_collider.enabled = false;
                    }
                    enemies.RemoveAt(i);
                }
             
            } 
        }   
    }


    public void FindClosestEnemy(){

        foreach (GameObject t in enemies){

            distanceWithTarget = Vector3.Distance(t.transform.position, player.transform.position);
            float angleVision = Vector3.Angle(player.transform.forward, t.transform.position - player.transform.position);

            if(distanceWithTarget <= distance  && TargetLock == null && Mathf.Abs(angleVision) <= 140/2){

                TargetLock = t;
                TargetViewCamera = TargetLock.GetComponent<Transform>();
                TargetLock.transform.Find("ArrowSelectionLock").gameObject.SetActive(true);
                targetGroup.AddMember(TargetViewCamera, 1, 2);
            } 
        }
    }


    public void PlayerFaceToTarget(){

        if(TargetViewCamera != null){

            target_lock = true;
            Vector3 direction = (TargetViewCamera.position - player.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, lookRotation, Time.deltaTime * 20f);          
        }
    }

   
   
}
