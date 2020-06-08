using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBouclier : MonoBehaviour
{
    // Script a attacher sur bouclier avec un collider

    public static EnemyBouclier instance;

    public EnemyDefense EnemyDefenseScript;
    Animator anim;
    
    public int resistance_Shield;
    public GameObject destroyEffect;
  
    void Start(){

        instance = this;  
        anim = GetComponentInParent<Animator>();
        EnemyDefenseScript =  GetComponentInParent<EnemyDefense>();
    }

    void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "degatEnemy"){

            if(resistance_Shield > 0){

                resistance_Shield -= 5; // todo a voir pour les degats
                anim.SetTrigger("getHitShield");
                EnemyDefenseScript.enemyScript.PlaySound(4);
            }

            else{ // destruction bouclier

               // freezeEffect.instance.SlowMotion();

                EnemyDefenseScript.enemyScript.PlaySound(3);
                anim.SetBool("defenseShield",false);
                EnemyDefenseScript.StopAllCoroutines();
                EnemyDefenseScript.enemyScript.isDefense = false;

                Vector3 positionSave = transform.position; // on recupere la position pour la creation de la particule
                Quaternion rotationSave = transform.rotation; // idem avec rotation
                GameObject particuleBroken = Instantiate(destroyEffect, positionSave, rotationSave);
              
                Destroy(particuleBroken, 2f);
                gameObject.SetActive(false); 
            }
        }
    }
}
