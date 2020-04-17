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
  
    public Vector3 Target;
    public float timer;
    public int newTarget;

    bool modeSentinelle;
    bool directionToBase;


    public List<GameObject> mesEnemyList = new List<GameObject>();



    void Start(){

        instance = this;
       // InvokeRepeating("CreateEnemy", 0f, 5f);
    }

    void Update(){


        if (Input.GetKeyDown("e")){
            CreateEnemy();
        }
   
        if (mesEnemyList != null){

            foreach (GameObject CloneEnemy in mesEnemyList){

                NavMeshAgent agent = CloneEnemy.GetComponent<NavMeshAgent>();
                Transform agentPosition =  CloneEnemy.GetComponent<Transform>();
              
                float distancePlayer = Vector3.Distance(Player.transform.position,agentPosition.transform.position);
                float distanceOrigin = Vector3.Distance(Enemy_Container.transform.position,agentPosition.transform.position);
               
                if (distancePlayer <= 20 && distanceOrigin < 40 && !directionToBase){
                    agent.SetDestination(Player.position);
                
                    if (distancePlayer <= agent.stoppingDistance){
                        print("Fight !");
                    }
                    modeSentinelle = false;
                }

                if (distanceOrigin > 40){
                    agent.SetDestination(Enemy_Container.position);
                    directionToBase = true;
                }
                  if (distancePlayer <= 3){ 
                        directionToBase = false;  
                    }

                if(distanceOrigin <= agent.stoppingDistance){ 
                    modeSentinelle = true;
                    directionToBase = false;
                }
                   
                if (modeSentinelle){

                    timer += Time.deltaTime;

                    if (timer >= newTarget){

                        float x = agentPosition.transform.position.x;
                        float y = agentPosition.transform.position.y;
                        float xPos = Random.Range(x - 20,x +10);
                        float yPos = Random.Range(y - 20,y +10);

                        Target = new Vector3(xPos,agentPosition.transform.position.y,yPos);
                        agent.SetDestination(Target);
                        timer = 0;
                    }
                }

               

             
            }
        }
    }

  
    void CreateEnemy(){
        
        CloneEnemy = Instantiate(Enemy,Enemy_Container.position, Enemy_Container.rotation);
        // int max = (Random.Range(20,100)/10)*10;
        // enemy.instance.EnemyCharacteristic(max);
        NavMeshAgent agent = CloneEnemy.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(3f, 6f);
        mesEnemyList.Add(CloneEnemy); 
    }

   


}
