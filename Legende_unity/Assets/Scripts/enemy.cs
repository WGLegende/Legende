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
    public float move_speed_attack = 3.5f;
    public float poids = 1f;
    public float rayon_d_actionMax = 20f;
    public int courage;


    [Header("Mode Volant")]
    public bool isFlying;
    public float altitude;

    [Header("Mode Sentinelle")]
    public bool isSentinel;
    public float speed_sentinelle = 1f;
    [HideInInspector] public bool activeSentinel = true;
    [HideInInspector] public Vector3 sentinelTarget; // position aleatoire sentinelle

    [Header("Mode Patrouille")]
    public bool isPatrouille;
    public bool loop_trajet = true;
     public float speed_patrouille = 1f;
    [HideInInspector] public bool activePatrouille = true;
    [HideInInspector] public bool allerparcours = true;
    public Transform[] WayPoint;
    [HideInInspector]public int current;
    
    [Header("Mode Bowman")]
    public bool isBowman;
    public GameObject Projectile;
    public Transform OriginProjectile;
    public float power_projectile = 10f;
    public float distance_attack_bowman = 15f;

    [HideInInspector] public Animator anim; 
    [HideInInspector] public Rigidbody poids_enemy;
   
    [HideInInspector] public Vector3 startPosition;  // position initiale
    [HideInInspector] public float currentPv;

    [HideInInspector]public Slider slider;
    [HideInInspector]public Text TX_Pv;
    [HideInInspector]public Text TX_PvMax;
    [HideInInspector]public GameObject HealthBar;

    [HideInInspector] public Transform target; // positionPlayer pour navmesh
    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public float distancePlayer; // distance avec le player
    [HideInInspector] public float distanceBase ; // distance avec la base

    [HideInInspector] public bool isAlive = true;
    [HideInInspector] public bool directionBase = false;
    [HideInInspector] public bool Alerte = false;

    [Header("Mode Renfort")]
    public bool caserne = false;
    public int nbrEnemy = 3;
    int countEnemy = 0;
    bool goCreateEnemy = true;
    public Transform Position_Caserne;
    public GameObject newEnemy;
    public GameObject[] groupe_soutien;

    [Header("Death Particule")]
    public GameObject particule; // for the death

    bool forEachDone = false;
    [HideInInspector]public bool justOnceCoroutine = false;

 
    void Start(){
        
        instance = this;
        enemy_manager.instance.addToList(GetComponent<enemy>());

        CharacteristicEnemyPv(maxPv); // on met a jour la barre de vie avec le pvMax

        agent = GetComponent<NavMeshAgent>();
        agent.speed = move_speed_attack;

        poids_enemy = GetComponent<Rigidbody>();
        poids_enemy.mass = poids;

        anim = GetComponent<Animator>();
        HealthBar.GetComponent<Canvas>().enabled = false;

        target = player_main.instance.player.transform; // on recupere la position du player
        startPosition = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z); // on stocke la position intiale

        if (isFlying){ agent.baseOffset = altitude; }
        if (isBowman){ agent.stoppingDistance = distance_attack_bowman; } // distance d'attaque
        directionBase = false;
                
    }

  
    void Update(){

        if(isAlive){
            
            anim.SetFloat("SpeedMove", agent.desiredVelocity.magnitude); // on adapte l'anim en fonction de sa vitesse

            distancePlayer = Vector3.Distance(target.position, transform.position); // distance avec le player
            distanceBase = Vector3.Distance(startPosition, transform.position); // distance avec la position initiale

            // on se dirige vers le player en alerte  zone jaune
            if (distancePlayer <= rayon_d_attaque +rayon_d_attaque*40/100 && !directionBase && !Alerte){ 
                comportement_actuel = enemy_manager.comportement.alerte;
                FaceTarget(); 
            }

            // on se dirige vers le player en courant zone rouge Activation Alerte
            if (distancePlayer <= rayon_d_attaque && !directionBase){  
                Alerte = true;
                appelleRenfort();

                if(isFlying){// on descend pour attaquer
                    float levelPlayer = target.transform.position.y;
                    if(agent.baseOffset >= levelPlayer+1) { agent.baseOffset -= Time.deltaTime * 8f;} 
                }

                comportement_actuel = enemy_manager.comportement.cible_detectee;    
            }

            // attack player
            if (distancePlayer <= agent.stoppingDistance && !directionBase){ 
                comportement_actuel = enemy_manager.comportement.attack;
                StopAllCoroutines();
                justOnceCoroutine = false;
                FaceTarget();
            
                if(isFlying && agent.baseOffset <= altitude){// on descend pour attaquer
                    agent.baseOffset += Time.deltaTime * 2f;
                } 
            }   
                                 
            // si joueur s'eloigne on repart a la position initiale
            if (distancePlayer > rayon_d_attaque +rayon_d_attaque*40/100 && Alerte && courage < 100 ){  
                comportement_actuel = enemy_manager.comportement.retour_base; 
                Alerte = false;
            }

            //on repart si enemy hors zone est trop loin apres  coroutine
            if (distanceBase > rayon_d_actionMax  && Alerte && courage < 100 && !justOnceCoroutine){ 

                StartCoroutine(horsZone());
                justOnceCoroutine = true;
            } 
          
        
           
            // deplacement enemy aleatoire
            if (isSentinel && activeSentinel){ 
                comportement_actuel = enemy_manager.comportement.sentinel;
                activeSentinel = false;
            }

            // deplacement enemy sur trajet predefini
            if (isPatrouille && activePatrouille){  
                comportement_actuel = enemy_manager.comportement.patrouille;
                activePatrouille = false;
            }
        }
    }

    IEnumerator horsZone(){ 
       
        yield return new  WaitForSeconds(courage-courage*90/100);
        directionBase = true;
        comportement_actuel = enemy_manager.comportement.retour_base; 
    }

    // face au player
    public void FaceTarget(){

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }


     // declenchee par anim attack Bowman
    void ShootArrow(){

        enemy_manager.instance.degatForPlayer = Random.Range(degatMin, degatMax);
        GameObject ProjectileClone = Instantiate(Projectile,OriginProjectile.position, OriginProjectile.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(OriginProjectile.right *power_projectile, ForceMode.Impulse);
        Destroy(ProjectileClone,4);      
    }


     // declenchee par le void start
    public void CharacteristicEnemyPv(float valueMax){

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

    //degat inflige a enemy
    public void DegatEnemy(){ 
        comportement_actuel = enemy_manager.comportement.cible_detectee;
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

     // declenchee par anim Die
    void DisparitionEnemy(){

        Vector3 positionSave = transform.position; // on recupere la derniere positon enemy pour la creation de la particule
        Quaternion rotationSave = transform.rotation; 

        GameObject particuleDeath = Instantiate(particule, positionSave, rotationSave);

        Destroy(gameObject,1.5f);
        Destroy(particuleDeath,5f);
        enemy_manager.instance.mesEnemyList.Remove(this); 

    }


    // On appelle les potes si y en a
    void appelleRenfort(){

        if(!forEachDone){
            foreach (GameObject Mesenemy in groupe_soutien){ // soutien
                Mesenemy.GetComponent<enemy>().comportement_actuel = enemy_manager.comportement.cible_detectee;   
              
            }
        forEachDone = true;
        }

        if(caserne){ // mode caserne
            if (goCreateEnemy){
                InvokeRepeating("createEnemyCaserne",0f,2.0f);
                goCreateEnemy = false;
            }          
        }   
    }


    // creation des enemy caserne
    void createEnemyCaserne(){

        GameObject Clone = Instantiate(newEnemy,Position_Caserne.position,Position_Caserne.rotation);
        Clone.GetComponent<enemy>().comportement_actuel = enemy_manager.comportement.cible_detectee;  
        countEnemy++;

        if (countEnemy >= nbrEnemy){
            CancelInvoke();           
        }
        
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
  