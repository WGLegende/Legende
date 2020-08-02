using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class saveEnemy : MonoBehaviour
{
    public static saveEnemy instance;

    public List<enemy> SaveEnemyList = new List<enemy>();
    

    void Awake(){
        instance = this;  
    }


    public void restoreEnemy(){// called par player_main

        Debug.Log(SaveEnemyList.Any(e => e.current_comportement != enemy_manager.comportement.dead) ? "Les enemis ne sont pas tous morts" : "Les ennemis sont tous morts");

        if(SaveEnemyList.Any(e => e.current_comportement != enemy_manager.comportement.dead)){

            foreach (enemy enemy in SaveEnemyList){

                if(enemy.current_comportement == enemy_manager.comportement.dead){ // si l'ennemi est dead

                   enemy.Initialize();
                   enemy.current_comportement = enemy_manager.comportement.retour_base;
  
                }

                else{
                    enemy.current_comportement = enemy_manager.comportement.retour_base;
                    enemy.gameObject.transform.position = enemy.startPosition;
                    enemy.gameObject.transform.rotation = enemy.startRotation;
                    enemy.CharacteristicEnemyPv(enemy.maxPv);  
                }
            }
        }

        else{
            Debug.Log("Je Clear la Liste");
            enemy_manager.instance.StopAllCoroutines();

            foreach (enemy enemy in SaveEnemyList){
               Destroy(enemy.gameObject);
            }
            SaveEnemyList.Clear();
            enemy_manager.instance.mesEnemyList.Clear();
        }
    }









}
