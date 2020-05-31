using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefense : MonoBehaviour
{
    public static EnemyDefense instance;

    Animator anim;

    [HideInInspector] public enemy enemyScript;
    EnemyBouclier enemyBouclierScript;
    
    [Range(0f,100f)]
    public float Pcent_defense;
    
    public float temps_defense = 2f;
    public int resistance_bouclier;

    void Awake(){

        instance = this;

        anim = GetComponentInChildren<Animator>();
        enemyScript = GetComponentInParent<enemy>();
        enemyBouclierScript = GetComponentInChildren<EnemyBouclier>();
        
        if (enemyBouclierScript != null)
        enemyBouclierScript.resistance_Shield = resistance_bouclier;
       
    }

    

    // declenchee par enemy manager
    public void enemyIsDefense(){ 

        float i = Random.value;

        if(i*100 < Pcent_defense && enemyBouclierScript.resistance_Shield > 0 ){

            enemyScript.isDefense = true;
            anim.SetBool("defenseShield",true);
            enemyScript.PlaySound(7);
            StartCoroutine("timerDefense");
        }
    }


    // temps de defense
    IEnumerator timerDefense(){ 

        yield return new WaitForSeconds(temps_defense); 
        anim.SetBool("defenseShield",false);
        enemyScript.PlaySound(7);
        yield return new WaitForSeconds(1f); // temps de se retourner (anim)
        enemyScript.isDefense = false;
    }  


      
    







}
