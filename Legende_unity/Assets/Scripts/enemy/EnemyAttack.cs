using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static EnemyAttack instance;

    enemy enemyScript;
    Animator anim;
    Transform target;

    public typeAttack _type_attack;
    public enum typeAttack{
        Cac,
        distance,
        Cac_et_distance 
    }

    public float degatMin;
    public float degatMax;
    public float cadence_de_frappe = 2f;
    public float distance_C_a_C = 4f;
    public float distance_shoot = 15f;

    [Header("Pourcentage d'Attaque")]
    [Range(0f,100f)]
    public float Pcent_attackSecondaire;
    [Range(0f,100f)]
    public float Pcent_attackSpecial;
    public float Pcent_gain_Supp;
    float Pcent_gain_attackSpecial;

    
    public GameObject Projectile;
    public Transform OriginProjectile;
    public float power_projectile = 10f;
    bool tireur;

    [Header("Special Attack")]
    public float force_aspiration = 5f;
    public int duree_aspiration = 4;
    public float distance_aspiration = 10f;
    public ParticleSystem particule_attackSpecial;

    public bool attack_special_is_active;
    public bool enemyIsAttack;
  
   
    void Start(){

        instance = this;
        anim = GetComponentInChildren<Animator>();
        target = player_main.instance.player.transform;

        enemyScript = GetComponentInParent<enemy>();

        if(_type_attack == typeAttack.Cac){enemyScript.distance_attack = distance_C_a_C;} // on renseigne la distance d'attaque en fonction du type
        else if(_type_attack == typeAttack.Cac_et_distance){enemyScript.distance_attack = distance_shoot; tireur = true;}
        else if(_type_attack == typeAttack.distance){enemyScript.distance_attack = distance_shoot;}

        
        Pcent_gain_attackSpecial = Pcent_attackSpecial;  
    }


    // declenchee par manager
    public void StartAttack(){ 
        enemyIsAttack = true;
        StartCoroutine("attackTarget");
        if(tireur){StartCoroutine(checkPositionPlayer());}
    }

    // declenchee par manager
    public void StopAttack(){
        enemyIsAttack = false;
        StopCoroutine("attackTarget");
    }

    // declenche par manager comportement alerte
    public void animAlerte(bool value){

       StartCoroutine(positionAlerte(value));
    }

    IEnumerator positionAlerte(bool value){

        anim.SetBool("eye_alerte",value);

        if(_type_attack != typeAttack.Cac){ // on passe en mode distance
           
            anim.SetBool("mode_shoot",value);
        }  
        yield return null;
    }


    // declenchee par manager on rebascule en shooter si mode tireur
    public void FinAlerte(){

        if(tireur){
            _type_attack = typeAttack.Cac_et_distance;
            enemyScript.distance_attack = distance_shoot;
        }
    }


    // Si player se rapproche on passe en mode corps a corps seulement si typeAttack = Cac et distance
    public IEnumerator checkPositionPlayer(){
        while(enemyIsAttack){

            if ((Vector3.Distance(target.position, transform.position)) < distance_shoot/2){

                enemyScript.distance_attack = distance_C_a_C;
                _type_attack = typeAttack.Cac;
                anim.SetBool("mode_shoot",false);
            }
            yield return new WaitForSeconds(0.02f); 
        } 
    }


    // Tout se passe ici
    public IEnumerator attackTarget(){

       yield return new WaitForSeconds(0.5f);  // tempo pour eviter permuation trop rapide a tester

        while(enemyIsAttack){

            if(!enemyScript.isDefense && !attack_special_is_active){ // Seulement si enemy n'est pas en attaque special et en defense

            print("attack");
                  
                if(_type_attack == typeAttack.Cac){ // attack Cac

                    float i = Random.value*100;

                    if(i < Pcent_attackSecondaire){ 
                        attackSecondaire(); 
                        if(Pcent_attackSpecial > 0){Pcent_gain_attackSpecial += Pcent_gain_Supp;} // on checke si enemy a  une attaque special avant d'incrementer
                         
                    }
                    else if(i < Pcent_gain_attackSpecial){ 
                        Pcent_gain_attackSpecial = Pcent_attackSpecial;
                        attack_special_is_active = true;
                        attackSpecial(); 
                    }
                    else{
                        attackPrincipal();
                        if(Pcent_attackSpecial > 0){Pcent_gain_attackSpecial += Pcent_gain_Supp;} // on checke si enemy a  une attaque special avant d'incrementer
                    }
                }

                if(_type_attack == typeAttack.Cac_et_distance){// Attack distance
                    attackDistance();
                }
                
                if(_type_attack == typeAttack.distance){// Attack distance
                    attackPrincipal();
                }
            }

            yield return new WaitForSeconds(cadence_de_frappe); 
        } 
    }



    // Attack Estoc
    public void attackPrincipal(){

        anim.SetTrigger("attack1");

        if(_type_attack != typeAttack.distance){  
        enemyScript.PlaySound(0);
        }
        enemy_manager.instance.degatForPlayer = Random.Range(degatMin,degatMax);    
    }



    // Attack DoubleSword
    public void attackSecondaire(){

        anim.SetTrigger("attack2");
        enemyScript.PlaySound(1);
        enemy_manager.instance.degatForPlayer = Random.Range(degatMin,degatMax);    
    }



    // Attack Projectile 
    public void attackDistance(){

        anim.SetTrigger("shoot");
        enemyScript.PlaySound(6);
        GameObject ProjectileClone = Instantiate(Projectile,OriginProjectile.position, OriginProjectile.rotation);
        
        if(ProjectileClone.GetComponent<Rigidbody>() != null){
            ProjectileClone.GetComponent<Rigidbody>().AddForce(OriginProjectile.right *power_projectile, ForceMode.Impulse);
        }
        Destroy(ProjectileClone,3);
        enemy_manager.instance.degatForPlayer = Random.Range(degatMin, degatMax);   
    }

    public void attackBowman(){ //declenche par anim bowman

        anim.SetTrigger("shoot");
        enemyScript.PlaySound(6);
        GameObject ProjectileClone = Instantiate(Projectile,OriginProjectile.position, OriginProjectile.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(OriginProjectile.right *power_projectile, ForceMode.Impulse);
        Destroy(ProjectileClone,3);
        enemy_manager.instance.degatForPlayer = Random.Range(degatMin, degatMax);   
    }



    // Attack Aspirateur
    public void attackSpecial(){
        StartCoroutine("aspirePlayer");
        StartCoroutine("timerAspiration"); 
        enemy_manager.instance.degatForPlayer = Random.Range(degatMin,degatMax); 
    }

    
   
    // deplacement du player 
    IEnumerator aspirePlayer(){

        anim.SetBool("mode_attack3",true);
        enemyScript.PlaySound(2);

        yield return new WaitForSeconds(1f); // le temps de se tourner (anim)
        
        anim.SetBool("attack3",true);
        hinput.gamepad[0].VibrateAdvanced(0.4f, 0.4f);
        particule_attackSpecial.Play();

         
        while((Vector3.Distance(target.position, transform.position)) < distance_aspiration) {

            if(Vector3.Distance(target.position, transform.position) >= 2){

                target.transform.position = Vector3.MoveTowards(target.transform.position,transform.position, force_aspiration * Time.deltaTime);
            } 
            yield return new WaitForSeconds(0.02f);    
        }     
    }



     // temps d'aspiration
    IEnumerator timerAspiration(){ 

        yield return new WaitForSeconds(1f); // le temps de se tourner

        int i = duree_aspiration;

        while( i > 0){  
            i--;   
            yield return new WaitForSeconds(1); 
        }  
        anim.SetBool("attack3",false); // arret ventilateur
        anim.SetBool("mode_attack3",false); // enemy se retourne
        particule_attackSpecial.Stop();
        enemyScript.StopSound(2);
        enemyScript.PlaySound(8);

        StopCoroutine("aspirePlayer");
        StopCoroutine("timerAspiration");  
        attack_special_is_active = false;
        hinput.gamepad[0].StopVibration();
    }

}
