using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class enemy : MonoBehaviour
 
{
    public static enemy instance;

    public enemy_manager.comportement comportement_actuel = new enemy_manager.comportement();

    [Header("Characteristic Enemy")]
    public float maxPv = 20f;
    public float degatMin = 1f;
    public float degatMax = 5f;
    public float rayon_d_attaque = 10f;
    public float moveSpeed = 3.5f;
    public float poids = 1f;
    public float rayon_d_actionMax = 20f;
    public bool isModeSentinel;
    public bool isFlying;

    [HideInInspector] public bool activeSentinel = true;
    public bool isPatrouille;
    [HideInInspector] public bool activePatrouille = true;
    public Transform[] WayPoint;
    private int current;
    
   
    [Header("Mode Bowman")]
    public bool isBowman;
    public GameObject Projectile;
    public Transform OriginProjectile;
    public float power_projectile = 10f;

    [HideInInspector] public Animator anim; 
    [HideInInspector] public Rigidbody poids_enemy;
    [HideInInspector] public Vector3 sentinelTarget; // position aletoire sentinelle
    [HideInInspector] public Vector3 startPosition;  // position initiale
    [HideInInspector] public float currentPv;

    public Slider slider;
     public Text TX_Pv;
    public Text TX_PvMax;
     public GameObject HealthBar;


    [HideInInspector] public Transform target; // positionPlayer pour navmesh
    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public float distancePlayer; // distance avec le player
    [HideInInspector] public float distanceBase ; // distance avec la base
    [HideInInspector] public float distancePlayerRayonAction ;

    [HideInInspector] public bool isAlive = true;
    [HideInInspector] public bool directionBase = false;

    public GameObject particule; // for the death
    public GameObject[] groupeEnemy;

 
    void Start(){
        
        instance = this;
        enemy_manager.instance.addToList(GetComponent<enemy>());

        CharacteristicEnemyPv(maxPv); // on met a jour la barre de vie avec le pvMax

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        poids_enemy = GetComponent<Rigidbody>();
        poids_enemy.mass = poids;

        anim = GetComponent<Animator>();
        HealthBar.GetComponent<Canvas>().enabled = false;

        target = player_main.instance.player.transform; // on recupere la position du player
        startPosition = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z); // on stocke la position intiale

        directionBase = false;
    }

  
    void Update(){

        if(isAlive){
            
            anim.SetFloat("SpeedMove", agent.desiredVelocity.magnitude); // on adapte l'anim en fonction de sa vitesse

            distancePlayer = Vector3.Distance(target.position, transform.position); // distance avec le player
            distanceBase = Vector3.Distance(startPosition, transform.position); // distance avec la position initiale
            distancePlayerRayonAction = Vector3.Distance(startPosition, target.position); 


            if (distancePlayer <= rayon_d_attaque +rayon_d_attaque*40/100 && !directionBase ){  // on se dirige vers le player en alerte  zone jaune
                FaceTarget(); 
                activeSentinel = false;  
                activePatrouille = false;
                comportement_actuel = enemy_manager.comportement.alerte;  
            
                if (distancePlayer <= rayon_d_attaque){  // on se dirige vers le player zone rouge
                    comportement_actuel = enemy_manager.comportement.cible_detectee; 

                    if(isFlying){

                        if(agent.baseOffset <= 0.5f) {return;}
                        agent.baseOffset -= Time.deltaTime * 6f;  
                    }
                }
             
                foreach (GameObject Mesenemy in groupeEnemy){
                    Mesenemy.GetComponent<enemy>().activeSentinel = false; 
                    Mesenemy.GetComponent<enemy>().activePatrouille = false; 
                    Mesenemy.GetComponent<enemy>().comportement_actuel = enemy_manager.comportement.cible_detectee;    
                }

                if (isBowman){
                    agent.stoppingDistance = 15; // distance d'attaque
                }  
              
                if (distancePlayer <= agent.stoppingDistance){ // attack player
                    FaceTarget();
                    anim.SetTrigger("attack"+Random.Range(1, 3));
                    comportement_actuel = enemy_manager.comportement.attack;  
                    StopCoroutine(horsZone());  
                }      
            } 


            if (distancePlayer > rayon_d_attaque +rayon_d_attaque*40/100 && !directionBase){  //si joueur s'eloigne on repart a la position initiale
                if(isPatrouille){
                    comportement_actuel = enemy_manager.comportement.patrouille;
                }

                else if (isFlying){
                    if(agent.baseOffset >= 5f) {return;}
                        agent.baseOffset += Time.deltaTime * 8f;  
                       print("on remonte");
                    }
                 
                 else{
                    comportement_actuel = enemy_manager.comportement.retour_base;
                    directionBase = true; 
                }
               
            }
            
            if (distanceBase > rayon_d_actionMax && distancePlayer > 4){  // on repart si position initiale est trop loin 
              
                if(isPatrouille){
                    comportement_actuel = enemy_manager.comportement.patrouille;
                 }else{
                    StartCoroutine(horsZone());  
                    directionBase = true;
                }   
            } 
           

            if (isModeSentinel && activeSentinel){ // deplacement enemy aleatoire
                comportement_actuel = enemy_manager.comportement.sentinel;
            }

            if (isPatrouille && activePatrouille){ // deplacement enemy sur trajet predefini

                agent.speed = 1f;
                agent.SetDestination(WayPoint[current].position);
                comportement_actuel = enemy_manager.comportement.patrouille;
              
                if(!agent.pathPending && agent.remainingDistance <= 3f){

                    current = (current + 1) % WayPoint.Length;
                }
            }
        }
      

    }

    IEnumerator horsZone(){

        yield return new  WaitForSeconds(5f);
        directionBase = true;
        comportement_actuel = enemy_manager.comportement.retour_base; 
    }
    
         
    public void FaceTarget(){

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void ShootArrow(){ // declenchee par anim attack Bowman

        enemy_manager.instance.degatForPlayer = Random.Range(degatMin, degatMax);
        GameObject ProjectileClone = Instantiate(Projectile,OriginProjectile.position, OriginProjectile.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(OriginProjectile.right *power_projectile, ForceMode.Impulse);
        Destroy(ProjectileClone,4);      
    }

    public void CharacteristicEnemyPv(float valueMax){ // declenchee par le void start

        maxPv = valueMax;
        currentPv  = maxPv;
        slider.value = currentPv;
        TX_Pv.text = currentPv.ToString();
        TX_PvMax.text = maxPv.ToString();
        slider.maxValue = maxPv; // maj du slider en fonction du pvMax
    }

   
    public void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "degatEnemy"){

            DegatEnemy();   
        }   
    }

   
    public void DegatEnemy(){ //degat inflige a enemy

        currentPv -= PlayerGamePad.instance.degat_sword;
        currentPv = Mathf.Clamp(currentPv, 0, maxPv);
        slider.value = Mathf.Clamp(currentPv, 0, maxPv);
        TX_Pv.text = currentPv.ToString("f0");
        HealthBar.GetComponent<Canvas>().enabled = true;

        anim.SetTrigger("getHit");
       
        if (currentPv <= 0){

            HealthBar.GetComponent<Canvas>().enabled = false;
            isAlive = false;
            anim.SetBool("isAlive",false);
        }  
    }


    void DisparitionEnemy(){ // declenchee par anim Die

        Vector3 positionSave = transform.position; // on recupere la derniere positon enemy pour la creation de la particule
        Quaternion rotationSave = transform.rotation; 
        GameObject particuleDeath = Instantiate(particule, positionSave, rotationSave);

        Destroy(gameObject,1.5f);
        Destroy(particuleDeath,5f);
        enemy_manager.instance.mesEnemyList.Remove(this); 

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // rayon attaque
        Gizmos.DrawWireSphere(transform.position,rayon_d_attaque); 

        Gizmos.color = Color.green; // rayon action max
        Gizmos.DrawWireSphere(startPosition,rayon_d_actionMax);

        Gizmos.color = Color.yellow; // rayon d'alerte
        Gizmos.DrawWireSphere(transform.position,rayon_d_attaque+rayon_d_attaque*40/100);

         for (int i = 0; i < WayPoint.Length - 1; i++)
             {
                 Debug.DrawLine(WayPoint[i].position,WayPoint[i + 1].position, Color.magenta);
             }
        
    }




}
  