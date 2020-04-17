using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class enemy_manager : MonoBehaviour
{

    
    public static enemy_manager instance;

    public Transform Player;
    public GameObject Enemy;
    public Transform Enemy_Container;
    
    GameObject CloneEnemy;
    bool follow;
    public List<GameObject> mesEnemyList = new List<GameObject>();



    void Start(){

        instance = this;
       // InvokeRepeating("CreateEnemy", 0f, 5f);
    }

    void Update(){


        if (Input.GetKeyDown("space")){
            CreateEnemy();
        }
   
        if (mesEnemyList != null){

            foreach (GameObject CloneEnemy in mesEnemyList){

                NavMeshAgent agent = CloneEnemy.GetComponent<NavMeshAgent>();
                Transform agentPosition =  CloneEnemy.GetComponent<Transform>();
              
                float distancePlayer = Vector3.Distance(Player.transform.position,agentPosition.transform.position);
                float distanceOrigin = Vector3.Distance(Enemy_Container.transform.position,agentPosition.transform.position);
               
                if (distancePlayer <= 20f){
                    agent.SetDestination(Player.position);

                    if (distancePlayer <= agent.stoppingDistance){
                        print("Fight !");
                    }
                }

                if (distanceOrigin >= 20 && distancePlayer > 20f){
                    agent.SetDestination(Enemy_Container.position);
                }

             
            }
        }
    }

  
    void CreateEnemy(){
        
        CloneEnemy = Instantiate(Enemy,Enemy_Container.position, Enemy_Container.rotation);
        NavMeshAgent agent = CloneEnemy.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(3f, 6f);
        mesEnemyList.Add(CloneEnemy); 
    }


}
