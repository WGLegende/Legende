using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTriggerAnim : MonoBehaviour{



    enemy enemyScript;
    EnemyAttack attackScript;

    bool modeShoot;


    void Start(){

        enemyScript = GetComponentInParent<enemy>();
        attackScript = GetComponentInParent<EnemyAttack>();
        
    }



    void Attack01(){

        enemyScript.PlaySound(0);     
    }


    void Attack02(){

        
    }

    void modeshoot(){

        modeShoot = true;
       
    }

     void modeshootEnd(){

        modeShoot = false;
       
    }

    void shoot(){

        // if(!modeShoot)
        // return;

        // enemyScript.PlaySound(6);
        // GameObject ProjectileClone = Instantiate(attackScript.Projectile,attackScript.OriginProjectile.position,attackScript.OriginProjectile.rotation);
        
        // if(ProjectileClone.GetComponent<Rigidbody>() != null){
        //     ProjectileClone.GetComponent<Rigidbody>().AddForce(attackScript.OriginProjectile.right *attackScript.power_projectile, ForceMode.Impulse);
        // }
        // Destroy(ProjectileClone,3);
       
    }

    void fxMoveSpecialAttack(){

        enemyScript.PlaySound(7);
        
    }
}
