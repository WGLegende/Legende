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

    void Awake(){

        instance = this;
    }

    void Update(){
       
        foreach (enemy enemy in mesEnemyList){
            if(enemy.old_comportement != enemy.current_comportement){

                if(enemy.current_comportement == enemy_manager.comportement.alerte){
                    StartCoroutine(do_alert_walk(enemy));
                }
                else if(enemy.current_comportement == enemy_manager.comportement.attack){

                    // ...
                }




                enemy.old_comportement = enemy.current_comportement;
            }

            // if (enemy.comportement_actuel == comportement.alerte){
            //     StartCoroutine(AlerteWalk(enemy));
            // }
        
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
               
            }

            if (enemy.comportement_actuel == comportement.patrouille){
                mode_patrouille(enemy);  
            }

            if (enemy.comportement_actuel == enemy_manager.comportement.sentinel){ 
                mode_sentinelle(enemy);
            }

        }    
    }

  
    public void addToList(enemy enemy){
        mesEnemyList.Add(enemy); 
        enemy.activate_enemy();
    }


    IEnumerator do_alert_walk(enemy enemy){ 
        while(enemy.current_comportement == enemy_manager.comportement.alerte){
            enemy.FaceTarget();  
            
            enemy.agent.SetDestination(enemy.target.position); 
            enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
            
            yield return new  WaitForSeconds(0.02f); 
        } 

        yield return null; 
    }




    // On se dirige vers le Player en courant
    public void detection_player(enemy enemy){ 

        enemy.agent.speed = enemy.move_speed_attack; 
        enemy.agent.SetDestination(enemy.target.position);
        enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
    
        if(!boolAppelSoutien){
            appelRenfort(enemy);
            boolAppelSoutien = true;
        }
    }


    //on attaque la cible
    public void enemy_attack(enemy enemy){ 


        enemy.timerAttack += Time.deltaTime;
        enemy.agent.SetDestination(enemy.target.position);

        if (enemy.timerAttack >= enemy.cadence_de_frappe){
            enemy.anim.SetTrigger("attack"+Random.Range(1, 3));
            enemy_manager.instance.degatForPlayer = Random.Range(enemy.degatMin, enemy.degatMax); // on renseigne les degats infliges au Player
            enemy.timerAttack = 0;
        }      
    }


    // retour a la position initiale
    public void retour_a_la_base(enemy enemy){

        enemy.agent.stoppingDistance = 2.5f;
        enemy.agent.SetDestination(enemy.startPosition);
        enemy.HealthBar.GetComponent<Canvas>().enabled = false; 

       
        if (enemy.distanceBase > enemy.rayon_d_actionMax){
            enemy.agent.speed = PlayerGamePad.instance.SpeedMove + 0.05f; // on rentre plus vite que le player en dehors de la zone max
        }

        if(enemy._deplacement == enemy.deplacement.Patrouille){
            enemy.comportement_actuel = comportement.patrouille; 
            return;  
        }
        
        if(enemy.distanceBase < enemy.rayon_d_actionMax){ // On attends d'etre dans la safe zone

            if(enemy._deplacement == enemy.deplacement.Sentinel){
                enemy.agent.speed = enemy.speed_sentinelle; 
                enemy.agent.SetDestination(enemy.sentinelTarget);
                enemy.comportement_actuel = comportement.sentinel; 
            } 
            else if(enemy.agent.remainingDistance <= 3f){  //enemy dans sa position initiale
                enemy.comportement_actuel = comportement.attente;  
            }
        
        }

    }

   
    // enemy sen deplace en aleatoire dans son rayon max
    public void mode_sentinelle(enemy enemy){
  
        if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){

            enemy.agent.speed = enemy.speed_sentinelle;
    
            float x = enemy.startPosition.x;
            float z = enemy.startPosition.z;
            float xPos = Random.Range(x - enemy.rayon_d_actionMax, x + enemy.rayon_d_actionMax);
            float zPos = Random.Range(z - enemy.rayon_d_actionMax, z + enemy.rayon_d_actionMax);
            
            enemy.sentinelTarget = new Vector3(xPos,transform.position.y,zPos);
            enemy.agent.SetDestination(enemy.sentinelTarget);          
        }      
    }

    // enemy patrouille suivant un trajet predini
    public void mode_patrouille(enemy enemy){

        enemy.agent.SetDestination(enemy.WayPoint[enemy.current_patrol_point].position);

        enemy.Finparcours = enemy.current_patrol_point == 0;


        if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 3f){  // prochaine destination
            enemy.agent.speed = enemy.speed_patrouille;

            if (enemy._parcours == enemy.parcours.allerRetour){
                if(enemy.Finparcours){
                    enemy.current_patrol_point = (enemy.current_patrol_point + 1) % enemy.WayPoint.Length;
                }
                else{
                    enemy.current_patrol_point = (enemy.current_patrol_point - 1) % enemy.WayPoint.Length;
                }
            }

             // mode Boucle
            if(enemy._parcours == enemy.parcours.boucle){  
                enemy.current_patrol_point = (enemy.current_patrol_point + 1) % enemy.WayPoint.Length;
            }
        }  
    }
    

    // On appelle les potes si y en a
    public void appelRenfort(enemy enemy){
        foreach (enemy renfort in enemy.groupe_soutien){ // soutien
            renfort.comportement_actuel = enemy_manager.comportement.cible_detectee;    
        }

        if(enemy.nbrEnemy > 0){
            StartCoroutine(createEnemyCaserne(enemy));
        }
    }


    public IEnumerator createEnemyCaserne(enemy enemy){ 

        // logique qui se fait une seule fois AVANT


        while(enemy.nbrEnemy > 0){ // nest
        // logique en boucle PENDANT

            enemy Clone = Instantiate(enemy.newEnemy, enemy.emplacement_caserne.position, enemy.emplacement_caserne.rotation).GetComponent<enemy>();
            Clone.comportement_actuel = enemy_manager.comportement.cible_detectee;
            Clone.CharacteristicEnemyPv(Random.Range(30,80));
            Clone.move_speed_attack = Random.Range(0.5f,6f);
            Clone.cadence_de_frappe = Random.Range(1,4);   
                         
            enemy.nbrEnemy--;
            yield return new  WaitForSeconds(enemy.cadence_enemy); 
        }

        // logique qui se fait une seule fois APRES

        yield return null; 
    }


    //  public IEnumerator attack_target(enemy enemy){ 

    //     while(enemy.comportement_actuel == enemy_manager.comportement.attack){
    
    //         print("top");
    //         yield return new  WaitForSeconds(enemy.cadence_de_frappe); 
    //     } 
    //        print("the end");
    //     yield return new  WaitForSeconds(0.1f); 
    // }






}
