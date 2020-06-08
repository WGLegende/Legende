using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class enemy : MonoBehaviour

{

    public EnemyAttack EnemyAttackScript;
    public EnemyDefense EnemyDefenseScript;

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
    public float rayon_d_actionMax = 30f;
    public int courage;
    public float distance_attack = 2.5f;
    public Rigidbody poids_enemy;
    int pourcentage_rayonAlerte = 60;
    public bool isDefense;

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

    public race _race;
    public enum race{

        robot,
        human
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

    public GameObject effectSword;
    public GameObject origin_impact;

    public GameObject bouclier;

    void Start(){

        startPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z); // on stocke la position intiale
        startRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w); // on stocke la position intiale
        enemy_manager.instance.mesEnemyList.Add(GetComponent<enemy>());
        saveEnemy.instance.SaveEnemyList.Add(GetComponent<enemy>()); 
        Initialize();
    }

    public void Initialize(){

        gameObject.SetActive(true); 
        isAlive = true;
        GetComponentInChildren<Animator>().SetBool("isAlive",true);  
        activate_enemy();
    }


   // nest appelee par manager pour creation enemy
    public void RandomizeEnemy(){
        CharacteristicEnemyPv(Random.Range(30,200));
        move_speed_attack = Random.Range(2f,8f);
        EnemyAttackScript.cadence_de_frappe = Random.Range(1,4);  
        courage = Random.Range(1,100);  
        nbrEnemy = 0;
    }




    public void activate_enemy(){

        anim.SetBool("activate",true);

        gameObject.transform.position =  startPosition;
        transform.rotation = startRotation;

        EnemyAttackScript = GetComponentInParent<EnemyAttack>();
        EnemyDefenseScript = GetComponentInParent<EnemyDefense>();
        effectSmokeDegat = GetComponentInChildren<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        poids_enemy = GetComponent<Rigidbody>();

        CharacteristicEnemyPv(maxPv); // on met a jour la barre de vie avec le pvMax

        agent.speed = move_speed_attack;

        poids_enemy.mass = poids;

        HealthBar.GetComponent<Canvas>().enabled = false;

        target = player_main.instance.player.transform; // on recupere la position du player
                
        Trajet = new Transform[WayPoint.Length];
        System.Array.Copy(WayPoint, Trajet, WayPoint.Length);

        if (isFlying){
            agent.baseOffset = altitude;
        }
        
                  
        agent.stoppingDistance = distance_attack;

    
        switch(_deplacement){
            case deplacement.Patrouille :
                current_comportement = enemy_manager.comportement.patrouille;
                break;
            case deplacement.Sentinel :
                current_comportement = enemy_manager.comportement.sentinel; 
                break;
        }
        
        enemy_ready = true;
        StartCoroutine(check_if_new_comportement()); 
    }


    void Update(){

        if(enemy_ready){

            distancePlayer = Vector3.Distance(target.position, transform.position);

            if(distancePlayer < rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100 + 10){ // si player se rapproche on accelere le check comportement
                timerNewComportement = 0.02f;   
            }
            else{
                timerNewComportement = 1f;  
            }    
        }
    }

    IEnumerator check_if_new_comportement(){

        while(enemy_ready && isAlive){

            anim.SetFloat("SpeedMove", agent.desiredVelocity.magnitude);
            distancePlayer = Vector3.Distance(target.position, transform.position);
            distanceBase = Vector3.Distance(startPosition, transform.position);
            angleVision = Vector3.Angle(transform.forward, target.position - transform.position);
        
            if(check_if_alert_walk()) {current_comportement = enemy_manager.comportement.alerte;}
            if(check_if_cible_detectee()) {current_comportement = enemy_manager.comportement.cible_detectee;}
            if(check_if_attack()) {current_comportement = enemy_manager.comportement.attack;}
            if(check_if_defense()) {current_comportement = enemy_manager.comportement.defense;}
            if(check_if_return_base()) {current_comportement = enemy_manager.comportement.retour_base;}
           // if(check_if_too_far()) { }
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
        if(distancePlayer <= rayon_d_attaque + rayon_d_attaque*pourcentage_rayonAlerte/100 && !Alerte && distancePlayer > rayon_d_attaque && check_if_target_visible() &&
           current_comportement != enemy_manager.comportement.cible_detectee){
            Alerte = true;
            return true;
        }else{
            return false;
        }
    }

    // on se dirige vers le player en courant
    public bool check_if_cible_detectee(){
        if (distancePlayer <= rayon_d_attaque && distancePlayer > agent.stoppingDistance && current_comportement != enemy_manager.comportement.retour_base){  
            Alerte = true;
            return true;
        }else{
            return false;
        }
    }

    // Attaque
    public bool check_if_attack(){
        if (distancePlayer <= agent.stoppingDistance && !isDefense){
            StopCoroutine("horsZone");
            print("attack");
            return true;
        }else{
            return false;
        }
    }

    // Defense
    public bool check_if_defense(){       
        if (
            !EnemyAttackScript.attack_special_is_active &&
             current_comportement == enemy_manager.comportement.attack &&
              enemy_manager.instance.player_is_attack &&
               !isDefense &&
                distancePlayer < 5){
            return true;
        }else{
            return false;
        }
    }

    // Retour vers la base
    public bool check_if_return_base(){
        if (distancePlayer > rayon_d_attaque + rayon_d_attaque * pourcentage_rayonAlerte/100 && Alerte && courage < 100){ 
            Alerte = false;
            return true;
        }else{
            return false;
        }
    }

    // Retour vers la base si trop loin 
    public bool check_if_too_far(){
        if (distanceBase > rayon_d_actionMax  && courage < 100 && current_comportement == enemy_manager.comportement.cible_detectee){
            StartCoroutine("horsZone");
            return true;
        }else{
            return false;
        }
    }

    IEnumerator horsZone(){
        yield return new  WaitForSeconds(courage-courage*90/100);
        current_comportement = enemy_manager.comportement.retour_base;
        Alerte = false; 
        StopCoroutine("horsZone");
    }

    // face au player
    public void FaceTarget(){

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
    }



    public void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "degatEnemy" && isAlive && !isDefense){
            DegatEnemy();
        }
    }

    //degat inflige a enemy
    public void DegatEnemy(){

            currentPv -= 5; // a adapter

        if (currentPv > 0){

            freezeEffect.instance.Freeze();

            GameObject effect_sword = Instantiate(effectSword, origin_impact.transform.position, Quaternion.identity);
            Destroy(effect_sword, 2f); 

            currentPv = Mathf.Clamp(currentPv, 0, maxPv);
            slider.value = Mathf.Clamp(currentPv, 0, maxPv);
            TX_Pv.text = currentPv.ToString("f0");
            HealthBar.GetComponent<Canvas>().enabled = true;

            anim.SetTrigger("getHit");
            PlaySound(5);

            if( effectSmokeDegat != null && currentPv < maxPv/2){ 

                effectSmokeDegat.startSize += 1f;  
                effectSmokeDegat.Play();   
            }
        }

        else if (currentPv <= 0){

            isAlive = false;
            current_comportement = enemy_manager.comportement.dead;
            HealthBar.GetComponent<Canvas>().enabled = false;
            hinput.gamepad[0].StopVibration();
        }  
         
    }

    // declenche par manager
    public void DisparitionEnemy(){

        switch (_race){ //enum type race

            case race.robot: AudioSource.PlayClipAtPoint(Enemy_sound.instance.Robot[9], transform.position);
                             hinput.gamepad[0].Vibrate(0.4f);
                             GameObject particuleDeath = Instantiate(particule, transform.position, transform.rotation);
                             Destroy(particuleDeath,5f);
                             this.enabled = false;
                             gameObject.SetActive(false);  
            break;

            case race.human: PlaySound(9);
                             anim.SetBool("isAlive",false);
                             this.enabled = false;
            break;
        }
    }


   
    public void PlaySound(int i){

        switch (_race){ //enum type son
            case race.robot: Enemy_sound.instance.PlaySound(gameObject,Enemy_sound.instance.Robot[i]); 
                             break;

            case race.human: Enemy_sound.instance.PlaySound(gameObject,Enemy_sound.instance.Human[i]);
                             break;
        }
    }

    public void StopSound(int i){

        switch (_race){
            case race.robot: Enemy_sound.instance.StopSound(gameObject,Enemy_sound.instance.Robot[i]);
                             break;
                             
            case race.human: Enemy_sound.instance.StopSound(gameObject,Enemy_sound.instance.Human[i]);
                             break;
        }
    }


    // declenchee par le void start et par le saveEnemy
    public void CharacteristicEnemyPv(float valueMax){

        maxPv = valueMax;
        slider.maxValue = maxPv; // maj du slider en fonction du pvMax
        currentPv  = maxPv;
        slider.value = currentPv;
        TX_Pv.text = currentPv.ToString();
        TX_PvMax.text = maxPv.ToString();

        if(effectSmokeDegat != null ){
            effectSmokeDegat.Stop();
            effectSmokeDegat.startSize = 0f;
        }

        if(bouclier == null){
            return;
        }

        bouclier.SetActive(true);
        GetComponentInChildren<EnemyBouclier>().resistance_Shield = EnemyDefenseScript.resistance_bouclier;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // rayon attaque
        Gizmos.DrawWireSphere(transform.position,rayon_d_attaque);

        Gizmos.color = Color.green; // rayon action max
        Gizmos.DrawWireSphere(startPosition,rayon_d_actionMax);

        Gizmos.color = Color.yellow; // rayon d'alerte
        Gizmos.DrawWireSphere(transform.position,rayon_d_attaque+rayon_d_attaque*pourcentage_rayonAlerte/100);

        for (int i = 0; i < WayPoint.Length - 1; i++){ // trace pour patrouille
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
