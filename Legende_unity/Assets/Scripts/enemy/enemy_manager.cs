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
        defense,
        sentinel,
        retour_base,
        patrouille,
        dead
    }

    public float degatForPlayer;
    public bool player_is_attack;
    public bool boolAppelSoutien = false;
    bool zicFight;

    public List<enemy> mesEnemyList = new List<enemy>();


    void Awake(){
        instance = this;   
    }

    void Update(){

        foreach (enemy enemy in mesEnemyList){
           
            if(enemy.old_comportement != enemy.current_comportement){
    
                if(enemy.current_comportement == enemy_manager.comportement.dead){ 
                    enemy.DisparitionEnemy(); 
                    print("deeeeeddd commortement");    
                }
                                      
                if(enemy.current_comportement == enemy_manager.comportement.alerte){
                    StartCoroutine(do_alert_walk(enemy)); 
                    print("alerte");
                }

                else if (enemy.current_comportement == enemy_manager.comportement.cible_detectee){
                    StartCoroutine(detection_player(enemy));
                    print("cible detcte")      ;
                }

                else if (enemy.current_comportement == enemy_manager.comportement.attack){
                    StartCoroutine(enemy_attack(enemy)); 
                }

                else if (enemy.current_comportement == enemy_manager.comportement.defense){
                    StartCoroutine(mode_defense(enemy)); 
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
                
                else if (enemy.current_comportement == enemy_manager.comportement.attente){
                    enemy.current_comportement = enemy_manager.comportement.attente;
                } 

                if(mesEnemyList.Any(e => e.current_comportement == comportement.cible_detectee) && !zicFight){
                    Music_sound.instance.PlayMusic("FightTheme");
                    Music_sound.instance.FadeOutAndStop("MainTheme",2f); 
                    zicFight = true;
                }

                else if(mesEnemyList.All(e => e.current_comportement == comportement.patrouille ||
                                              e.current_comportement == comportement.sentinel ||
                                              e.current_comportement == comportement.retour_base ||
                                              e.current_comportement == comportement.dead ||
                                              e.current_comportement == comportement.attente)){

                    Music_sound.instance.PlayMusic("MainTheme");   
                    Music_sound.instance.FadeOutAndStop("FightTheme",2f);
                    zicFight = false;
                }

                enemy.old_comportement = enemy.current_comportement;
            }
        }      
    }

    public void playerAttack(){ // declenche par player manager
        player_is_attack = true;
    }


 // on se dirige vers le player en marchant
    IEnumerator do_alert_walk(enemy enemy){ 

        enemy.EnemyAttackScript.animAlerte(true);
        enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
        enemy.agent.speed = enemy.move_speed_walk;

        while(enemy.current_comportement == enemy_manager.comportement.alerte){

            enemy.FaceTarget();    
            enemy.agent.SetDestination(enemy.target.position); 
            yield return new  WaitForSeconds(0.02f); 
        } 
        yield return null; 
    }



    // on se dirige vers le player en courant en appelant les renforts si y a 
    IEnumerator detection_player(enemy enemy){

        enemy.EnemyAttackScript.animAlerte(true);
        enemy.HealthBar.GetComponent<Canvas>().enabled = true; 
        enemy.agent.speed = enemy.move_speed_attack; 

        if(!boolAppelSoutien && enemy.nbrEnemy > 0){
            appelRenfort(enemy);
            StartCoroutine(nest(enemy));
            boolAppelSoutien = true;
        }

       // yield return new WaitForSeconds(0.5f);// test pour eviter permutation trop rapide
      
        while(enemy.current_comportement == enemy_manager.comportement.cible_detectee){

                     
            enemy.agent.SetDestination(enemy.target.position);
            enemy.agent.stoppingDistance = enemy.distance_attack; 
          
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

        enemy.EnemyAttackScript.StartCoroutine(enemy.EnemyAttackScript.attackTarget()); // on declenche attack dans script enemyAttack
     
        while(enemy.current_comportement == enemy_manager.comportement.attack){
            
            enemy.FaceTarget();
            enemy.agent.SetDestination(enemy.target.position);
            enemy.agent.stoppingDistance = enemy.distance_attack; 
            yield return new  WaitForSeconds(0.02f); 
        } 

        yield return null; 
    }


     //en defense
    IEnumerator mode_defense(enemy enemy){ 

        player_is_attack = false;
        enemy.EnemyDefenseScript.enemyIsDefense();
      
        while(enemy.current_comportement == enemy_manager.comportement.defense){  
            enemy.FaceTarget();
            enemy.agent.SetDestination(enemy.target.position);
            yield return new  WaitForSeconds(0.02f); 
        } 
        yield return null; 
    }

  
    // retour a la position initiale
    IEnumerator retour_a_la_base(enemy enemy){

        enemy.EnemyAttackScript.FinAlerte();
        enemy.EnemyAttackScript.animAlerte(false);

        enemy.agent.stoppingDistance = 1f;
        enemy.agent.SetDestination(enemy.startPosition);
        enemy.HealthBar.GetComponent<Canvas>().enabled = false;

        if (enemy.distanceBase > enemy.rayon_d_actionMax){
            enemy.agent.speed = player_gamePad_manager.instance.SpeedMove + 0.5f; 
        }
  
        while(enemy.current_comportement == enemy_manager.comportement.retour_base){

            if(enemy.isFlying){// on remonte  
                StartCoroutine(remonte(enemy));
            } 

            if(enemy.agent.remainingDistance <=  enemy.agent.stoppingDistance ){  //enemy dans sa position initiale
                enemy.current_comportement =  enemy_manager.comportement.attente; 
                StartCoroutine(axeInitiale(enemy));// rotation intiale pour mode garde
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




    // On se remet dans l'axe initial pour le mode garde
    IEnumerator axeInitiale(enemy enemy){

        yield return new WaitForSeconds(1f);
        
        float timer = 0;
        while( timer <= 2f) {
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,enemy.startRotation, Time.deltaTime * 2f);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0.02f); 
        }     
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

        enemy.agent.stoppingDistance = 2f;
        enemy.EnemyAttackScript.FinAlerte();

        enemy.sentinelTarget = new Vector3(enemy.startPosition.x+Random.Range(0,10),transform.position.y,enemy.startPosition.z+Random.Range(0,10));
        enemy.agent.SetDestination(enemy.sentinelTarget);

        while(enemy.current_comportement == enemy_manager.comportement.sentinel){
              
            if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= enemy.agent.stoppingDistance){

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

        enemy.EnemyAttackScript.FinAlerte();
        enemy.agent.stoppingDistance = 1f;

        if(enemy.Trajet.Length == 0){
            Debug.LogError("AUCUN TRAJET TROUVE !!!");
        }else{
            
            while(enemy.current_comportement == enemy_manager.comportement.patrouille){
               
                enemy.agent.SetDestination(enemy.Trajet[enemy.point_de_trajet].position);
                    
                if(!enemy.agent.pathPending && enemy.agent.remainingDistance <= 2f){  // prochaine destination
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
        }
        yield return null; 
    }


    // petit check en fin de parcours aux alentours
    public IEnumerator waitTimeFinParcours(enemy enemy){ 

        yield return new  WaitForSeconds(2f);
        int count = 2; // nbr de position avant de repartir
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
     
            GameObject.Find("usine").GetComponent<Animator>().SetTrigger("createRobot");
            yield return new  WaitForSeconds(0.5f);   

            enemy Clone = Instantiate(enemy.newEnemy, enemy.emplacement_caserne.position, enemy.emplacement_caserne.rotation).GetComponent<enemy>();

            yield return new  WaitForSeconds(0.2f);    

            Clone.RandomizeEnemy();
        
            enemy.nbrEnemy--;  
            Clone.current_comportement = enemy_manager.comportement.cible_detectee;  

            yield return new  WaitForSeconds(enemy.cadence_enemy);    
        }         
      
        yield return null; 
    }


           
}
