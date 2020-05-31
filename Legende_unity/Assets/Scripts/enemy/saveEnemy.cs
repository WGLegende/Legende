using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class saveEnemy : MonoBehaviour
{
    public static saveEnemy instance;

    public List<GameObject> SaveEnemyList = new List<GameObject>();
    

    void Awake(){

        instance = this;  
    }


    public void restoreEnemy(){

        if(SaveEnemyList.Count > 0){
       
            foreach (GameObject target in SaveEnemyList){

                if(SaveEnemyList.Any(e => e.GetComponent<enemy>().enabled == false)){ // si enemy dead on reset tout

                    target.transform.position = target.GetComponent<enemy>().startPosition;
                    target.transform.rotation = target.GetComponent<enemy>().startRotation;
                    target.GetComponent<enemy>().isAlive = true;
                    target.GetComponentInChildren<Animator>().SetBool("isAlive",true);
                    target.SetActive(true);
                    target.GetComponent<enemy>().activate_enemy();
                }

                if(SaveEnemyList.Any(e => e.GetComponent<enemy>().enabled == true)){ // on remet les Pv a full et reposition pour les vivants

                    target.GetComponent<enemy>().current_comportement = enemy_manager.comportement.retour_base;
                    
                    target.transform.position = target.GetComponent<enemy>().startPosition;
                    target.transform.rotation = target.GetComponent<enemy>().startRotation;

                    float value =  target.GetComponent<enemy>().maxPv;
                    target.GetComponent<enemy>().CharacteristicEnemyPv(value);
                }


                if(SaveEnemyList.All(e => e.GetComponent<enemy>().enabled == false)){ // si tous mort on les detruits

                    enemy_manager.instance.StopAllCoroutines();
                    Destroy(target);
                    enemy_manager.instance.mesEnemyList.Clear();
                    SaveEnemyList.Clear();
                }
            
            }
        }
    }









}
