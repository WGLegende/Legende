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
                    enemy.directionBase = false; 
                    enemy.activeSentinel = true;
                    enemy.activePatrouille = true;
                }

                if (enemy.comportement_actuel == comportement.patrouille){
                    enemy.activePatrouille = true; 
                    enemy.HealthBar.GetComponent<Canvas>().enabled = false; // on desactive la barre de vie  
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


    public void alerte(enemy enemy){ // on se dirige vers le player

        enemy.agent.speed = 1f;
        enemy.agent.SetDestination(enemy.target.position); 
        enemy.HealthBar.GetComponent<Canvas>().enabled = true; // on active la barre de vie 
    }


    public void detection_player(enemy enemy){  // On se dirige vers le Player en courant

        enemy.activeSentinel = false;
        enemy.activePatrouille = false;
        enemy.agent.speed = enemy.moveSpeed; // on fait courir enemy
        enemy.agent.SetDestination(enemy.target.position);
        enemy.HealthBar.GetComponent<Canvas>().enabled = true; // on active la barre de vie  
    }


    public void enemy_attack(enemy enemy){ // on renseigne les degats infliges au Player

        enemy_manager.instance.degatForPlayer = Random.Range(enemy.degatMin, enemy.degatMax);   
    }


    public void retour_a_la_base(enemy enemy){ // retour a la position initiale

        enemy.agent.stoppingDistance = 2.5f;
        enemy.agent.speed = 8f;
        enemy.agent.SetDestination(enemy.startPosition);

        if ((enemy.distanceBase <= enemy.agent.stoppingDistance) && enemy.directionBase){  //enemy dans sa position initiale
            enemy.comportement_actuel = comportement.attente;
            enemy.directionBase = false;
            enemy.HealthBar.GetComponent<Canvas>().enabled = false; // on desactive la barre de vie 
        }
       
    }

   

    public void mode_sentinelle(enemy enemy){ // enemy patrouille dans son rayon max

        enemy.directionBase = false;

        if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){

            enemy.agent.speed = 1f; 

            float x = enemy.startPosition.x;
            float z = enemy.startPosition.z;
            float xPos = Random.Range(x - enemy.rayon_d_actionMax, x + enemy.rayon_d_actionMax);
            float zPos = Random.Range(z - enemy.rayon_d_actionMax, z + enemy.rayon_d_actionMax);
            
            enemy.sentinelTarget = new Vector3(xPos,transform.position.y,zPos);
            enemy.agent.SetDestination(enemy.sentinelTarget);

                 
        }
            
    }

    


}
