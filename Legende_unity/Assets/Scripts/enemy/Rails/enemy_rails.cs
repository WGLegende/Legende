using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_rails : MonoBehaviour{

    public static enemy_rails instance;

    Battlehub.MeshDeformer2.SplineFollow SplineFollowKart;

    [HideInInspector] public Battlehub.MeshDeformer2.SplineFollow SplineFollowEnemy;
    [HideInInspector] public GameObject gfx;
    [HideInInspector] public bool can_up;

    [HideInInspector]  public bool oneShot;
    public float vitesse_dpl;
    public float vitesse_attack;
    public float vitesse_up = 6;
    [HideInInspector] public float duree_de_vie;
  
    public AudioClip[] clips;
    [HideInInspector] public AudioSource audio_source;

    [HideInInspector] public GameObject particule_death;
    Animation anim;


    void Start(){

        if(instance == null){
            instance = this;
        }

        gfx = transform.GetChild(0).gameObject;
        SplineFollowEnemy = GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        SplineFollowKart = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        audio_source = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();

 
    }


    private void Update() {
        if(Input.GetKeyDown("f")){

            StartCoroutine(auto_destroy());
        }
    }

   
    public IEnumerator followKart(){

        audio_source.clip = clips[0];
        audio_source.Play();
        StartCoroutine(auto_destroy());

        if(can_up){
            StartCoroutine(up_enemy());
        }

        while(true){

            if(SplineFollowEnemy.T < SplineFollowKart.T){
                SplineFollowEnemy.Speed = vitesse_dpl + (vitesse_attack * Time.deltaTime);
            }

            yield return new WaitForSeconds(2f);
    
            if((SplineFollowEnemy.T > SplineFollowKart.T) && !oneShot){ 
                SplineFollowEnemy.Speed = - vitesse_dpl - (vitesse_attack * Time.deltaTime);
            }

            yield return null;
         
        }
    }


    IEnumerator up_enemy(){

        yield return new WaitForSeconds(1f);

        while(gfx.transform.localPosition.y < kart_manager.instance.hauteur_kart_max + 2.5f){
            gfx.transform.Translate(Vector3.up * Time.deltaTime * vitesse_up);
            yield return new WaitForSeconds(0.01f);
        }

    }


    public void Collision(){

        AudioSource.PlayClipAtPoint(clips[1], transform.position);
        GameObject Particule = Instantiate(particule_death,gfx.transform.position,gfx.transform.rotation);
        Destroy(gameObject,0f);
        Destroy(Particule,4f);
           
    }

    public void destroyEnemy(){

        Destroy(gameObject,0f); 
    }

    IEnumerator auto_destroy(){

        yield return new WaitForSeconds(duree_de_vie - 1.4f);
        anim.Play("test_sphere_rails");

        yield return new WaitForSeconds(1.4f);
        AudioSource.PlayClipAtPoint(clips[1], transform.position);
        GameObject Particule = Instantiate(particule_death,gfx.transform.position,gfx.transform.rotation);
        Destroy(gameObject,0f); 
        Destroy(Particule,4f);
    }
}
