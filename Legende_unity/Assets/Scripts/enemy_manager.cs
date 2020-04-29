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
    bool modeAlerte = false;

   
    public List<enemy> mesEnemyList = new List<enemy>();

    void Awake(){

        instance = this;
    }

    void Update(){
       
        foreach (enemy enemy in mesEnemyList){

            if(enemy.isAlive){

                if (enemy.comportement_actuel == comportement.alerte){
                    alerte(enemy);
                }
            
                if (enemy.comportement_actuel == comportement.cible_detectee){
                    detection_player(enemy); 
                }
                
                if (enemy.comportement_actuel == comportement.attack){
                    enemy_attack(enemy); 
                }

                if (enemy.comportement_actuel == comportement.retour_base){
                    retour_a_la_base(enemy);
                }

                if (enemy.comportement_actuel == comportement.attente){// On se la rouille grave
                    enemy.activeSentinel = true;
                    enemy.activePatrouille = true;
                }

                if (enemy.comportement_actuel == comportement.patrouille){
                    mode_patrouille(enemy);  
                }

                if (enemy.comportement_actuel == enemy_manager.comportement.sentinel){ 
                    mode_sentinelle(enemy);
                }

            }

        }    
    }

  
    public void addToList(enemy enemy){
      
        mesEnemyList.Add(enemy); 
    }


    // on se dirige vers le player
    public void alerte(enemy enemy){

        if(!modeAlerte){

            if(enemy.comportement_actuel != comportement.cible_detectee){
            enemy.agent.speed = 1f;
            }
            
            enemy.agent.SetDestination(enemy.target.position); 
            enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
        }
        modeAlerte = true;
    }


    // On se dirige vers le Player en courant
    public void detection_player(enemy enemy){ 

        enemy.agent.speed = enemy.move_speed_attack; 
        enemy.agent.SetDestination(enemy.target.position);
        enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
    }


    //on attaque la cible
    public void enemy_attack(enemy enemy){ 

        enemy.anim.SetTrigger("attack"+Random.Range(1, 3));
        enemy_manager.instance.degatForPlayer = Random.Range(enemy.degatMin, enemy.degatMax); // on renseigne les degats infliges au Player
    }


    // retour a la position initiale
    public void retour_a_la_base(enemy enemy){

        modeAlerte = false;

        if (enemy.distanceBase > enemy.rayon_d_actionMax){
        enemy.agent.speed = PlayerGamePad.instance.SpeedMove + 0.2f; // on rentre plus vite que le player en dehors de la zone max
        }else{
            enemy.agent.speed = enemy.speed_sentinelle; // si dans zone speed normal
        }
        if (enemy.distanceBase < enemy.rayon_d_actionMax){
            enemy.directionBase = false;
            enemy.justOnceCoroutine = false;
        }

        enemy.agent.stoppingDistance = 2.5f;

        if(enemy.isPatrouille) {
            enemy.agent.SetDestination(enemy.WayPoint[enemy.current].position);
         }else{
        enemy.agent.SetDestination(enemy.startPosition);
        }

        if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){  //enemy dans sa position initiale
            enemy.comportement_actuel = comportement.attente;
            enemy.HealthBar.GetComponent<Canvas>().enabled = false; 
        }  
    }

   
    // enemy patrouille dans son rayon max
    public void mode_sentinelle(enemy enemy){

    
        enemy.agent.speed = enemy.speed_sentinelle;
        enemy.HealthBar.GetComponent<Canvas>().enabled = false;
         
        if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){

            

            float x = enemy.startPosition.x;
            float z = enemy.startPosition.z;
            float xPos = Random.Range(x - enemy.rayon_d_actionMax, x + enemy.rayon_d_actionMax);
            float zPos = Random.Range(z - enemy.rayon_d_actionMax, z + enemy.rayon_d_actionMax);
            
            enemy.sentinelTarget = new Vector3(xPos,transform.position.y,zPos);
            enemy.agent.SetDestination(enemy.sentinelTarget);        
        }
            
    }


    public void mode_patrouille(enemy enemy){

        enemy.agent.speed = enemy.speed_patrouille;

        enemy.HealthBar.GetComponent<Canvas>().enabled = false;
        enemy.agent.SetDestination(enemy.WayPoint[enemy.current].position);

         if(enemy.current == enemy.WayPoint.Length-1 && !enemy.loop_trajet){ // on s'arrete en fin de parcours
                    enemy.activePatrouille = false;
                    enemy.comportement_actuel = comportement.attente;
                    return;
            }

        if (enemy.current == enemy.WayPoint.Length-1){ enemy.allerparcours = false;};
        if (enemy.current == 0){ enemy.allerparcours = true;};

      
        if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){  // prochaine destination

            if (enemy.allerparcours){// aller retour
                enemy.current = (enemy.current + 1) % enemy.WayPoint.Length;
             }else{
                enemy.current = (enemy.current - 1) % enemy.WayPoint.Length;
            }
        }
               



            
        
    }

    


}
