using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour

{
   
    public float force_canon;
    public float duree_de_vie;
    public float degats_infliges;

   
    public GameObject particule_fire;
    public GameObject particule_water;
    public GameObject particule_lava;

    public float rayon_explosion = 5f;
    public float force_explosion = 5f;

    
    void Start()
    {
     
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Cible_chariot"){

            GameObject particuleEffect = Instantiate(particule_fire,transform.position,transform.rotation); //creation de la particule
            Collider[] colliders = Physics.OverlapSphere(transform.position, rayon_explosion);

            foreach (Collider nearbyObject in colliders){
               Rigidbody rb =  nearbyObject.GetComponent<Rigidbody>();
                if (rb !=  null){
                   rb.AddExplosionForce(force_explosion, transform.position, rayon_explosion);
                }
            }

            Destroy(gameObject,0); // on detruit d'abord la bullet
            Destroy(particuleEffect,2); // et la particule 2s apres

         }else{
           Destroy(gameObject,duree_de_vie);
        }
        
    }

    public void shoot(Transform sourceShoot){

        GetComponent<Rigidbody>().AddForce(sourceShoot.forward*force_canon, ForceMode.Impulse);
             
    }
}
