using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Turret : MonoBehaviour{

  Transform target;
  float timer;


 [Header("Characteristics")]
  public bool isActive;
  public float maxPv = 20f;
  float currentPv;
  public float puissance_de_tir = 10f;
  public float cadence_de_tir = 1f;
  public float degat_projectile = 16f;
  public float speed_move_detection = 6f;
  public float distance_attack = 10f;
  public float angle_de_vision = 70f;
  public float speed_auto_rotation = 10f;
  bool detection;

  [Header("Edition")]
  public Transform pan;
  public Transform canon;
  public Transform OriginShoot;
  public GameObject Projectile;
  public ParticleSystem particule;
  public Slider slider;
  public Text TX_Pv;
  public Text TX_PvMax;
 // public GameObject HealthBar;
  BoxCollider colliderDetection;


    void Start()
    {
        target = player_main.instance.player.transform; 
        colliderDetection = GetComponent<BoxCollider>();
        colliderDetection.size = new Vector3(angle_de_vision, colliderDetection.size.y,distance_attack);
        colliderDetection.center = new Vector3(colliderDetection.center.x, colliderDetection.center.y,distance_attack/2); // deplacement axe pour etre devant la tower
    }

  
    void Update(){ 

        if(!detection && isActive){
            pan.transform.Rotate(Vector3.down * Time.deltaTime * speed_auto_rotation, Space.World);
        }
    }

    void Shoot(){  

        enemy_manager.instance.degatForPlayer = degat_projectile;
        GameObject ProjectileClone = Instantiate(Projectile,OriginShoot.position, pan.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(OriginShoot.forward *puissance_de_tir, ForceMode.Impulse);
        particule.Play();
        Destroy(ProjectileClone,5);  
    }


    void OnTriggerStay(Collider other) {

        if(other.gameObject.tag == "Player"){
        
            if(isActive){
    
                float distance = Vector3.Distance(target.position, pan.transform.position);

                if (distance <= distance_attack){

                    detection = true;
                   
                    Vector3 direction = (target.position - pan.transform.position).normalized;
                    Vector3 directionTilt = (target.position - canon.transform.position).normalized;

                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0, direction.z));
                    Quaternion lookTilt = Quaternion.LookRotation(new Vector3(direction.x, directionTilt.y + 0.10f,direction.z));

                    pan.transform.rotation = Quaternion.Slerp(pan.transform.rotation, lookRotation, Time.deltaTime * speed_move_detection);
                    canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation, lookTilt, Time.deltaTime * speed_move_detection);

                    timer += Time.deltaTime;

                    if (timer >= cadence_de_tir){
                        Shoot();
                        timer = 0;
                    }    
                }
            }
        }
    }


    void OnTriggerExit(){
    
        float distance = Vector3.Distance(target.position, pan.transform.position);

        if (distance > distance_attack){
            detection = false;
        }
    }


    // void OnDrawGizmosSelected(){

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(canon.transform.position,distance_attack);
    // }

    void OnDrawGizmosSelected(){

        Gizmos.color = Color.red;
        float totalFOV = angle_de_vision;
        float halfFOV = totalFOV / 2.0f;

        Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFOV, Vector3.up );
        Quaternion rightRayRotation = Quaternion.AngleAxis( halfFOV, Vector3.up );
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay( transform.position, leftRayDirection * distance_attack );
        Gizmos.DrawRay( transform.position, rightRayDirection * distance_attack );
     }


}
