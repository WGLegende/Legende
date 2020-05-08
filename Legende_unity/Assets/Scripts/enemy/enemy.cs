using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class enemy : MonoBehaviour

{
    public enemy_manager.comportement comportement_actuel = new enemy_manager.comportement();

   [Header("Characteristic Enemy")]
    public float currentPv;
    public float maxPv = 20f;
    public float degatMin = 1f;
    public float degatMax = 5f;
    public float rayon_d_attaque = 10f;
    public float angle_de_vison = 90f;
    public float move_speed_attack = 3.5f;
    public float move_speed_walk = 1f;
    public float poids = 1f;
    public float rayon_d_actionMax = 20f;
    public int courage;
    public float cadence_de_frappe = 1;
    public float distance_attack = 2.5f;
    public Rigidbody poids_enemy;
    int pourcentage_rayonAlerte = 80;
    public Collider colliderTrigger;


    [Header("Comportement")]
    public deplacement _deplacement;
    public enum deplacement{
        Sentinel,
        Patrouille,
        Garde
    }

    public parcours _parcours;
    public enum parcours{
        boucle,
        allerRetour
    }

    public float speed_sentinelle = 1f;
    public float speed_patrouille = 1f;
    public Transform[] WayPoint;
    public int current_patrol_point;
    public Transform[] Trajet;
    public int point_de_trajet;

    public bool Finparcours = false; // bool pour trajet patrouille
    public Vector3 sentinelTarget; // position aleatoire sentinelle

    public bool isFlying;
    public float altitude;

    public bool isBowman;
    public GameObject Projectile;
    public Transform OriginProjectile;
    public float power_projectile = 10f;
    
    [Header("Renfort")]
    public GameObject newEnemy;
    public int nbrEnemy = 0;
    public float cadence_enemy = 2f;
    public bool goCreateEnemy = true;
    public Transform emplacement_caserne;
    public enemy[] groupe_soutien;

    public Animator anim;

    public Slider slider;
    public Text TX_Pv;
    public Text TX_PvMax;
    public GameObject HealthBar;

    public Transform target; // positionPlayer pour navmesh
    public NavMeshAgent agent;
    public Vector3 startPosition;  // position initiale
    public Quaternion startRotation;  // rotation initiale
    public float distancePlayer; // distance avec le player
    public float angleVision; // angle de vision
    public float distanceBase ; // distance avec la base

    public bool isAlive = true;
    public bool Alerte = false;

    [Header("Death Particule")]
    public GameObject particule; // for the death

    public enemy_manager.comportement current_comportement = new enemy_manager.comportement();
    public enemy_manager.comportement old_comportement = new enemy_manager.comportement();

    public bool enemy_ready = false;
    float timerNewComportement= 2f;

    ParticleSystem effectSmokeDegat;

    void Start(){
       enemy_manager.instance.addToList(GetComponent<enemy>()); 
    }

    public void activate_enemy(){

        effectSmokeDegat = GetComponentInChildren<ParticleSystem>();

        CharacteristicEnemyPv(maxPv); // on met a jour la barre de vie avec le pvMax

        agent = GetComponent<NavMeshAgent>();
        agent.speed = move_speed_attack;

        poids_enemy = GetComponent<Rigidbody>();
        poids_enemy.mass = poids;

        HealthBar.GetComponent<Canvas>().enabled = false;

        target = player_main.instance.player.transform; // on recupere la position du player
        startPosition = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z); // on stocke la position intiale
        startRotation = transform.rotation; // on stocke la rotation intiale (pour retour mode garde)
                
        Trajet = new Transform[WayPoint.Length];
        System.Array.Copy(WayPoint, Trajet, WayPoint.Length);

        if (isFlying){ agent.baseOffset = altitude; }
        agent.stoppingDistance = distance_attack;

        if(current_comportement != enemy_manager.comportement.cible_detectee){
            if(_deplacement == deplacement.Patrouille){
                current_comportement = enemy_manager.comportement.patrouille;
            }
            else if(_deplacement == deplacement.Sentinel){
                current_comportement = enemy_manager.comportement.sentinel; 
            }
        }
          
        enemy_ready = true;
        StartCoroutine(check_if_new_comportement()); 
    }


    void Update(){

        if(enemy_ready){

            distancePlayer = Vector3.Distance(target.position, transform.position);

            if(distancePlayer < rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100 + 10){
                timerNewComportement = 0.02f;   
            }
            else{
                timerNewComportement = 1f;  
            }    
        }
    }

    IEnumerator check_if_new_comportement(){

        while(enemy_ready){
      
            anim.SetFloat("SpeedMove", agent.desiredVelocity.magnitude);
            distancePlayer = Vector3.Distance(target.position, transform.position);
            distanceBase = Vector3.Distance(startPosition, transform.position);
            angleVision = Vector3.Angle(transform.forward, target.position - transform.position);
        
            if(check_if_alert_walk()) {current_comportement = enemy_manager.comportement.alerte;}
            if(check_if_cible_detectee()) {current_comportement = enemy_manager.comportement.cible_detectee;}
            if(check_if_attack() && isAlive) {current_comportement = enemy_manager.comportement.attack;}
            if(check_if_return_base()) {current_comportement = enemy_manager.comportement.retour_base;}
            //if(check_if_too_far()) { print("TROP LOIN");}
            yield return new  WaitForSeconds(timerNewComportement); 
        }
    }


     // On check si Target est dans son angle de vision
    public bool check_if_target_visible(){
        if (Mathf.Abs(angleVision) <= angle_de_vison/2 && distancePlayer <= rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100){
            return true;
        }else{
            return false;
        }
    }

    // On se dirige vers le player
    public bool check_if_alert_walk(){
        if(distancePlayer <= rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100 && !Alerte && distancePlayer > rayon_d_attaque && check_if_target_visible()){
            Alerte = true;
            return true;
        }else{
            return false;
        }
    }
    // on se dirige vers le player en courant
    public bool check_if_cible_detectee(){
        if (distancePlayer <= rayon_d_attaque && distancePlayer > agent.stoppingDistance + 1){
            return true;
        }else{
            return false;
        }
    }
    // Attaque
    public bool check_if_attack(){
        if (distancePlayer <= agent.stoppingDistance){
                return true;
        }else{
            return false;
        }
    }
    // Retour vers la base
    public bool check_if_return_base(){
        if (distancePlayer > rayon_d_attaque + rayon_d_attaque *pourcentage_rayonAlerte/100 && Alerte && courage < 100){ 
            Alerte = false;
            return true;
        }else{
            return false;
        }
    }

 // Retour vers la base si trop loin
    public bool check_if_too_far(){
        if (distanceBase > rayon_d_actionMax && Alerte && courage < 100){
            Alerte = false;
            return true;
        }else{
            return false;
        }
    }

    IEnumerator horsZone(){
        yield return new  WaitForSeconds(courage-courage*90/100);
        comportement_actuel = enemy_manager.comportement.retour_base;
    }

    // face au player
    public void FaceTarget(){

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
    }


    // declenchee par anim attack Bowman
    public void ShootArrow(){

        enemy_manager.instance.degatForPlayer = Random.Range(degatMin, degatMax);
        GameObject ProjectileClone = Instantiate(Projectile,OriginProjectile.position, OriginProjectile.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(OriginProjectile.right *power_projectile, ForceMode.Impulse);
        Destroy(ProjectileClone,3);
    }


    public void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "degatEnemy"){
            DegatEnemy();
        }
    }

    //degat inflige a enemy
    public void DegatEnemy(){

        currentPv -= 8;
        currentPv = Mathf.Clamp(currentPv, 0, maxPv);
        slider.value = Mathf.Clamp(currentPv, 0, maxPv);
        TX_Pv.text = currentPv.ToString("f0");
        HealthBar.GetComponent<Canvas>().enabled = true;

        anim.SetTrigger("getHit");

        if(this.name == "Acolyte Mecanique"){
            if(currentPv < maxPv/2){
                effectSmokeDegat.Play();   
            }
        }
        if (currentPv <= 0 && isAlive){
          
            isAlive = false;
            enemy_manager.instance.StopAllCoroutines();
            HealthBar.GetComponent<Canvas>().enabled = false;
            anim.SetBool("isAlive",false);
            DisparitionEnemy();
        }
    }

     // declenchee par anim Die
    void DisparitionEnemy(){

        Vector3 positionSave = transform.position; // on recupere la derniere positon enemy pour la creation de la particule
        Quaternion rotationSave = transform.rotation;
        GameObject particuleDeath = Instantiate(particule, positionSave, rotationSave);

        Destroy(gameObject);
        Sound_manager.Instance.lanceMusiquetest();
        Destroy(particuleDeath,5f); 
        enemy_manager.instance.mesEnemyList.Remove(this);    
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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // rayon attaque
        Gizmos.DrawWireSphere(transform.position,rayon_d_attaque);

        Gizmos.color = Color.green; // rayon action max
        Gizmos.DrawWireSphere(startPosition,rayon_d_actionMax);

        Gizmos.color = Color.yellow; // rayon d'alerte
        Gizmos.DrawWireSphere(transform.position,rayon_d_attaque+rayon_d_attaque*pourcentage_rayonAlerte/100);

        for (int i = 0; i < WayPoint.Length - 1; i++){ // trace pour patrouile
            Debug.DrawLine(WayPoint[i].position,WayPoint[i + 1].position, Color.magenta);
        }

        // trace pour angle de vision
        Gizmos.color = Color.white;
        float totalFOV = angle_de_vison;
        float halfFOV = totalFOV / 2.0f;

        Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFOV, Vector3.up );
        Quaternion rightRayRotation = Quaternion.AngleAxis( halfFOV, Vector3.up );
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay( transform.position, leftRayDirection * (rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100));
        Gizmos.DrawRay( transform.position, rightRayDirection * (rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100));
    }




}
