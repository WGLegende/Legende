using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_enemy_rail : MonoBehaviour{


    void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == "degatEnemy"){

            GetComponentInParent<enemy_rails>().Collision();
        }
    }
}
