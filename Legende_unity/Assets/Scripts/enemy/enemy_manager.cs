using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class enemy_manager : MonoBehaviour

{
    public static enemy_manager instance;

    public enum comportement{
        attente,
        alerte,
        cible_detectee,
        attack, 
        sentinel,
        retour_base,
        patrouille
    }

    public float degatForPlayer;
    public bool boolAppelSoutien = false;

    public List<enemy> mesEnemyList = new List<enemy>();
    bool trigger = false;

    void Awake(){

        instance = this;
    }

    void Update(){
       
        foreach (enemy enemy in mesEnemyList){

            if(!enemy.isAlive){
                StopAllCoroutines();
            }
        
            if(enemy.old_comportement != enemy.current_comportement){

                                      
                if(enemy.current_comportement == enemy_manager.comportement.alerte){
                    StartCoroutine(do_alert_walk(enemy));
                }

                else if (enemy.current_comportement == enemy_manager.comportement.cible_detectee){
                    StartCoroutine(detection_player(enemy));         
                }

                else if (enemy.current_comportement == enemy_manager.comportement.attack){
                    StartCoroutine(enemy_attack(enemy)); 
                    StartCoroutine(degatParSeconde(enemy));   
                }

                else if (enemy.current_comportement == enemy_manager.comportement.patrouille){
                    StartCoroutine(mode_patrouille(enemy));     
                }

                else if (enemy.current_comportement == enemy_manager.comportement.sentinel){ 
                    StartCoroutine(mode_sentinelle(enemy));  
                }

                else if (enemy.current_comportement == enemy_manager.comportement.retour_base){
                    StartCoroutine(retour_a_la_base(enemy));   
                }

                else{
                    enemy.current_comportement = enemy_manager.comportement.attente;
                } 
                enemy.old_comportement = enemy.current_comportement;
            }
        }    
    }

  
    public void addToList(enemy enemy){
        enemy.activate_enemy();
        mesEnemyList.Add(enemy); 
    }



 // on se dirige vers le player en marchant
    IEnumerator do_alert_walk(enemy enemy){ 
        while(enemy.current_comportement == enemy_manager.comportement.alerte){
            enemy.FaceTarget();    
            enemy.agent.SetDestination(enemy.target.position); 
            enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
            yield return new  WaitForSeconds(0.02f); 
        } 
        yield return null; 
    }



    // on se dirige vers le player en courant en appelant les renforts si y a 
    IEnumerator detection_player(enemy enemy){

        enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
        enemy.agent.stoppingDistance = enemy.distance_attack; 
        enemy.agent.speed = enemy.move_speed_attack; 
        appelRenfort(enemy);
        StartCoroutine(nest(enemy));
      
        while(enemy.current_comportement == enemy_manager.comportement.cible_detectee){
                     
            enemy.agent.SetDestination(enemy.target.position);
          
            if(enemy.isFlying){// on descend pour attaquer
                float levelPlayer = enemy.target.position.y;
                if(enemy.agent.baseOffset >= levelPlayer+1) { enemy.agent.baseOffset -= Time.deltaTime * 8f;} 
            }  
            yield return null; 
        } 
        yield return null; 
    }



    //on attaque la cible
    IEnumerator enemy_attack(enemy enemy){ 
        while(enemy.current_comportement == enemy_manager.comportement.attack){
            enemy.FaceTarget();
            enemy.agent.SetDestination(enemy.target.position);
            yield return new  WaitForSeconds(0.02f); 
        } 
        yield return null; 
    }

    IEnumerator degatParSeconde(enemy enemy){ 
        while(enemy.distancePlayer <= enemy.agent.stoppingDistance){
            enemy.anim.SetTrigger("attack"+Random.Range(1, 3));
            enemy_manager.instance.degatForPlayer = Random.Range(enemy.degatMin, enemy.degatMax); 
            if(enemy.isFlying){ StartCoroutine(ShootPlayer(enemy));}
            yield return new  WaitForSeconds(enemy.cadence_de_frappe); 
        }    
        yield return null; 
    }
    



    // retour a la position initiale
    IEnumerator retour_a_la_base(enemy enemy){

        enemy.agent.stoppingDistance = 2.5f;
        enemy.agent.SetDestination(enemy.startPosition);
        enemy.HealthBar.GetComponent<Canvas>().enabled = false;
        if (enemy.distanceBase > enemy.rayon_d_actionMax){
            enemy.agent.speed = player_gamePad_manager.instance.SpeedMove + 0.05f; 
        }
  
        while(enemy.current_comportement == enemy_manager.comportement.retour_base){

            if(enemy.isFlying){// on remonte  
                StartCoroutine(remonte(enemy));
            }  
            if(enemy.agent.remainingDistance <= 3f){  //enemy dans sa position initiale
                enemy.current_comportement =  enemy_manager.comportement.attente;    
            }
            yield return new WaitForSeconds(0.02f);// apres la boucle

            if(enemy._deplacement == enemy.deplacement.Sentinel){ // si sentinel on rebascule en mode sentinel  
                enemy.current_comportement = enemy_manager.comportement.sentinel;
            } 
            else if(enemy._deplacement == enemy.deplacement.Patrouille){  // si patrouille on rebascule en mode patrouille 
                enemy.agent.SetDestination(enemy.sentinelTarget);  
                enemy.current_comportement = enemy_manager.comportement.patrouille;   
            }
        }     
        yield return null; 
    }



    IEnumerator remonte(enemy enemy){ 
        while(enemy.agent.baseOffset < enemy.altitude) {
            enemy.agent.baseOffset += Time.deltaTime * 4f;
            print("on remonte");
            yield return new WaitForSeconds(Time.deltaTime); 
        }    
        yield return new WaitForSeconds(0.02f); 
    }

   

    // enemy se deplace en aleatoire dans son rayon max
    IEnumerator mode_sentinelle(enemy enemy){

        enemy.agent.stoppingDistance = 3f;

        while(enemy.current_comportement == enemy_manager.comportement.sentinel){
              
            if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){

                enemy.agent.speed = enemy.speed_sentinelle;
        
                float x = enemy.startPosition.x;
                float z = enemy.startPosition.z;
                float xPos = Random.Range(x - enemy.rayon_d_actionMax, x + enemy.rayon_d_actionMax);
                float zPos = Random.Range(z - enemy.rayon_d_actionMax, z + enemy.rayon_d_actionMax);
                
                enemy.sentinelTarget = new Vector3(xPos,transform.position.y,zPos);
                enemy.agent.SetDestination(enemy.sentinelTarget);
            }          
                yield return new  WaitForSeconds(0.02f); 
        } 
        yield return null; 
    }




    // enemy patrouille suivant un trajet predini
    IEnumerator mode_patrouille(enemy enemy){
           
        while(enemy.current_comportement == enemy_manager.comportement.patrouille){

            enemy.agent.SetDestination(enemy.Trajet[enemy.point_de_trajet].position);
                  
            if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){  // prochaine destination
                enemy.agent.speed = enemy.speed_patrouille;

                if (enemy._parcours == enemy.parcours.allerRetour){ // mode aller retour

                    if(enemy.point_de_trajet == 0){   
                        enemy.Finparcours = false;
                        StartCoroutine(waitTimeFinParcours(enemy));
                        yield return new  WaitForSeconds(8f);  
                    }
                    if(enemy.point_de_trajet == enemy.Trajet.Length-1){
                        enemy.Finparcours = true;
                        StartCoroutine(waitTimeFinParcours(enemy));
                        yield return new  WaitForSeconds(8f); 
                    }
                    if(enemy.Finparcours){
                        enemy.point_de_trajet--; 
                    }
                    else{
                        enemy.point_de_trajet++;
                    }
                }
                if(enemy._parcours == enemy.parcours.boucle){   // mode Boucle
                    enemy.point_de_trajet = (enemy.point_de_trajet + 1) % enemy.Trajet.Length;
                } 
            }  
            yield return new  WaitForSeconds(0.02f); 
        } 
        yield return null; 
    }

    // petit check en fin de parcours aux alentours
    public IEnumerator waitTimeFinParcours(enemy enemy){ 

        yield return new  WaitForSeconds(2f);
        int count = 2;
        int rayon_alentour = 10;

        while(count > 0){
            float xPos = Random.Range(enemy.agent.transform.position.x + rayon_alentour, enemy.agent.transform.position.x - rayon_alentour);
            float zPos = Random.Range(enemy.agent.transform.position.z + rayon_alentour, enemy.agent.transform.position.z - rayon_alentour);      
            enemy.sentinelTarget = new Vector3(xPos,transform.position.y,zPos);
            enemy.agent.SetDestination(enemy.sentinelTarget);
            count--;
            yield return new  WaitForSeconds(3f);
        }
      
    }
               



    // On appelle les potes si y en a
    public void appelRenfort(enemy enemy){

        foreach (enemy renfort in enemy.groupe_soutien){ // soutien
            renfort.current_comportement = enemy_manager.comportement.cible_detectee;       
        } 
    }



    public IEnumerator nest(enemy enemy){ 

        while(enemy.nbrEnemy > 0){ // nest
        // logique en byucle PENDANT

            enemy Clone = Instantiate(enemy.newEnemy, enemy.emplacement_caserne.position, enemy.emplacement_caserne.rotation).GetComponent<enemy>();
            Clone.CharacteristicEnemyPv(Random.Range(30,80));
            Clone.move_speed_attack = Random.Range(2f,6f);
            Clone.cadence_de_frappe = Random.Range(1,4);  
            Clone.courage = Random.Range(1,100);  
            Clone.nbrEnemy = 0;
            Clone.current_comportement = enemy_manager.comportement.sentinel;
            Clone.current_comportement = enemy_manager.comportement.cible_detectee;  
            enemy.nbrEnemy--;
            yield return new  WaitForSeconds(enemy.cadence_enemy);    
        }         
        // logique qui se fait une seule fois APRES  
        yield return null; 
    }

    

  // declenchee par anim attack Bowman
    public IEnumerator ShootPlayer(enemy enemy){

        degatForPlayer = Random.Range(enemy.degatMin,enemy. degatMax);
        GameObject ProjectileClone = Instantiate(enemy.Projectile,enemy.OriginProjectile.position,enemy. OriginProjectile.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(enemy.OriginProjectile.right *enemy.power_projectile, ForceMode.Impulse);
        Destroy(ProjectileClone,3);
        yield return null;  
    }
           
}
