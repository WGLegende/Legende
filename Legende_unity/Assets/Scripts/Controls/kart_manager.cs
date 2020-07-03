using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kart_manager : MonoBehaviour
{
    public static kart_manager instance;
    public bool equipement_Bouteille;
    public bool equipement_Belier;
    public bool equipement_light;
    public bool equipement_canon;
      
    public float velocity_chariot;

    float vitesse_demandee;
    public float vitesse_actuelle;
    public float vitesse_maximum;
  
    public Battlehub.MeshDeformer2.SplineFollow SplineFollow;
   
    Transform Chariot_ContainerRotation;
    float angleChariot;

    Animation anim;

    float valeur_vitesse_basique = 1f; // X = avance, -X = recule, 0 = sur place . X est limité par la valeur_vitesse_basique_max ci dessous
    public float valeur_vitesse_basique_max = 1f; // 1 = avance, -1 = recule, 0 = sur place

    public float valeur_frein = 1f; // 1 = en roue libre, 0 = a l'arret
    float valeur_boost = 0f; // 0 = pas de boost, x = boost
    public float valeur_boost_max = 20f; // permet de... limiter le boost...
        
    public float resistance_au_freinage = 10f; // Plus la resistance au freinage est élevée, plus le chariot va mettre du temps à freiner

    public ParticleSystem particle_vapeur_front;
    public ParticleSystem particle_vapeur_back;
    public ParticleSystem particle_vapeur_under;

    public ParticleSystem particle_etincelle_left_back;
    public ParticleSystem particle_etincelle_right_back;
    public ParticleSystem particle_etincelle_left_front;
    public ParticleSystem particle_etincelle_right_front;

    public Light light_chariot;
    bool toggle_light_chariot;

    public float camera_speed_rotation;
    public Transform chariot_siege;

    Text SpeedUI;

    public GameObject myBullet;
    public Transform CanonContainer;

    public bool canMoveAvance = true;
    public bool canMoveRecul = true;

    public BoxCollider offset_trigger;
    bool change_center_collider;

    
    void Start(){
        if(instance == null){
            instance = this;
        }   
        anim = gameObject.GetComponent<Animation>(); // Pour le Saut
        vitesse_actuelle = 0;
        Chariot_ContainerRotation = GameObject.Find("Chariot_Container").GetComponent<Transform>(); // On recupere l'angle pour la gravite
        SpeedUI = GameObject.Find("speedValue").GetComponent<Text>(); 

        StartCoroutine(refreshSpeedUI());
    }



    // Gere la rotation du siege
    public void kart_movement(float right_stick_x, float right_stick_y, float left_stick_x, float left_stick_y){
        chariot_siege.Rotate(0,0, right_stick_x * camera_speed_rotation  * Time.deltaTime, Space.Self); // rotate right/left character.
    }



    // GERE LE FREINAGE DU VEHICULE
    public void frein(bool is_frein_active){
        if (is_frein_active){
            valeur_frein -= Time.deltaTime/resistance_au_freinage;
            valeur_frein = valeur_frein < 0f ? 0f : valeur_frein;
        }else{
            valeur_frein += Time.deltaTime;
            valeur_frein = valeur_frein > 1f ? 1f : valeur_frein; 
        }
    }

    // Gestion de la vitesse basique avec le joystick
    public void calcul_vitesse_basique(float left_stick_y){
      
        if (left_stick_y > 0 && canMoveAvance){ // Avance
            valeur_vitesse_basique = valeur_vitesse_basique_max;
            if(SplineFollow.IsRunning == false){
                SplineFollow.IsRunning = true;
            }

        }else if (left_stick_y < 0 && canMoveRecul){ // Recul
            valeur_vitesse_basique = -valeur_vitesse_basique_max;
            if(SplineFollow.IsRunning == false){
                SplineFollow.IsRunning = true;
            }

        }else{
            valeur_vitesse_basique = 0f;
        }
    }



    // Gestion du boost // Fonctionne seulement s'il y a encore de la vapeur
    public void boost(bool boosting){
        if (boosting && VapeurBar.instance.useVapeur(0.05f) && canMoveRecul && canMoveAvance){
            if(valeur_vitesse_basique > 0){
                valeur_boost = valeur_boost_max;
                if(!particle_vapeur_back.isPlaying){
                    particle_vapeur_front.Stop();
                    particle_vapeur_back.Play();
                }
            }else {
                valeur_boost = -valeur_boost_max;
                if(!particle_vapeur_front.isPlaying){
                    particle_vapeur_back.Stop();
                    particle_vapeur_front.Play();
                }
            }
        }else if(valeur_boost != 0f){
            particle_vapeur_front.Stop();
            particle_vapeur_back.Stop();
            valeur_boost = 0f;
        }
    }



    // Gestion du saut du kart
    public void kart_jump(){
        if(VapeurBar.instance.useVapeur(10f)){ // Jump 
            anim.Play("JumpChariot");
            particle_vapeur_under.Play();
        }
    }



    // Gestion attaque du kart
    public void kart_attaque(){
        if (StockBullet.instance.bullet_stock > 0 && equipement_canon){  
            GameObject myBulletClone = Instantiate(myBullet,CanonContainer.position, CanonContainer.rotation);
            bullet bullet = myBulletClone.GetComponent<bullet>();
            bullet.shoot(CanonContainer);
            VapeurBar.instance.useVapeur(1f);  // consommation de vapeur
            StockBullet.instance.update_stock_bullet(-1); // maj du stock bullet
        }
    }



    // Allume lumiere du kart
    public void kart_light(){
        if (equipement_light){  
            toggle_light_chariot = !toggle_light_chariot;
            light_chariot.enabled = toggle_light_chariot; 
            print("light");
        }
    }



    public void speed_and_move(){

        angleChariot = Chariot_ContainerRotation.localEulerAngles.x;
        angleChariot = angleChariot > 180 ? angleChariot - 360 : angleChariot;
        angleChariot = Mathf.Round(angleChariot * 100f) / 100f;
        if(angleChariot < 1){
            angleChariot = 0;
        }

        vitesse_demandee = angleChariot + valeur_vitesse_basique + valeur_boost;

        // Vérifie si on doit accélèrer OU décélere
        vitesse_actuelle += vitesse_actuelle < vitesse_demandee ? 
                                    Time.deltaTime* velocity_chariot : 
                                   -(Time.deltaTime* velocity_chariot);
        
        // Multiplie par la valeur frein qui peut être entre 0 (frein total) et 1 (aucun frein)
        vitesse_actuelle *= valeur_frein; 

        // Permet de limiter la vitesse à la vitesse maximum, qu'elle soit négative ou positive
        vitesse_actuelle = vitesse_actuelle > vitesse_maximum ? vitesse_maximum :
                           vitesse_actuelle < -vitesse_maximum ? -vitesse_maximum : 
                           vitesse_actuelle;


        if(vitesse_actuelle > 0.5 || vitesse_actuelle < -0.5){
        SplineFollow.Speed = Mathf.RoundToInt(vitesse_actuelle);
        Player_sound.instance.PlayKart(Mathf.Abs(vitesse_actuelle));
        }else{
            SplineFollow.Speed = 0f;
        Player_sound.instance.StopKart();

        }
 
    }



    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){   // Rempli la jauge vapeur TRICHE todo
            VapeurBar.instance.fill_vapeur_stock();
        }
        
        speed_and_move();
       
        

        //Gestion des etincelles
        if(vitesse_actuelle >= 20){
            particle_etincelle_left_back.Play();
            particle_etincelle_right_back.Play();
            particle_etincelle_left_front.Stop();
            particle_etincelle_right_front.Stop();
         }else if (vitesse_actuelle < -20){
            particle_etincelle_left_back.Stop();
            particle_etincelle_right_back.Stop();
            particle_etincelle_left_front.Play();
            particle_etincelle_right_front.Play();
        }else{
            particle_etincelle_left_front.Stop();
            particle_etincelle_right_front.Stop();
            particle_etincelle_left_back.Stop();
            particle_etincelle_right_back.Stop();
        }
    } 

   
    IEnumerator refreshSpeedUI(){
        SpeedUI.text = Mathf.Abs(vitesse_actuelle).ToString("f0");
        yield return  new WaitForSeconds(0.2f);
        StartCoroutine(refreshSpeedUI());
    }
   

    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "CollisionRails" && !equipement_Belier){ 
           SplineFollow.IsRunning =false;
           canMoveAvance = false;
        //collider.gameObject.GetComponent<rails_triggers>().touching_chariot(GetComponent<ChariotPlayer>());
        }
    }

    void OnTriggerStay(Collider collider){

    }
    void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "CollisionRails"){ 
           canMoveAvance = true;
       
        }
    }






}
