using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_nest_rails : MonoBehaviour
{
    public static enemy_nest_rails instance;

    [Header("Characteristic")]
    public bool oneShot;
    public int min_duree_de_vie, max_duree_de_vie;
    public int nbr_cycle = 1;
    public float delai_cycle;

    [Header("Check for Enemy Up")]
    [Space(20)]
    public bool[] shema_du_cycle;
    public float frequence_apparition;

    [Header("Edition")]
    [Space(20)]
    public GameObject enemy_rails;
    public Animation anim;
    public GameObject particle_death;

    [Header("Audio Nest")]
    [Space(20)]
    public AudioClip[] clip_audio;
    AudioSource audio_source;

    [Header("Audio Enemy")]
    public AudioClip[] clip_audio_enemy;


    [Header("Enemy en Jeu")]
    [Space(20)]
    public List<enemy_rails> list_enemy = new List<enemy_rails>();
   
    [HideInInspector] public Battlehub.MeshDeformer2.SplineFollow SplineFollowKart;
    [HideInInspector] public bool justeOnce;

    float position_kart;
   

    void Start(){

        if(instance == null){
            instance = this;
        }
        SplineFollowKart = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        audio_source = GetComponent<AudioSource>();  
        enemy_rails_manager.instance.list_nest_rails.Add(this); 
    }


    void OnTriggerExit(Collider other){

        if(other.gameObject.name == "kart" && !justeOnce){
            position_kart = SplineFollowKart.m_t;// on recupere la position du kart 
            StartCoroutine(createEnemy());
            justeOnce = true;
        }  
    }


    IEnumerator createEnemy(){

        int i = 0;
        anim.Play("test_anim_nest_enemy_rail");
        audio_source.clip = clip_audio[0];
        audio_source.Play();

        yield return new WaitForSeconds(0.8f);

        while (i < nbr_cycle){

            for(int e = 0; e < shema_du_cycle.Length; e++){

                enemy_rails enemy_clone = Instantiate(enemy_rails,this.transform.position,this.transform.rotation).GetComponent<enemy_rails>();
                list_enemy.Add(enemy_clone); 

                yield return new WaitForSeconds(0.1f);
                enemy_clone.SplineFollowEnemy.Spline = SplineFollowKart.Spline;
                enemy_clone.SplineFollowEnemy.enabled = true; 

                yield return new WaitForSeconds(0.2f);
                enemy_clone.SplineFollowEnemy.Speed = enemy_clone.vitesse_dpl;
                enemy_clone.SplineFollowEnemy.IsRunning = true;
                enemy_clone.SplineFollowEnemy.Restart();
                enemy_clone.SplineFollowEnemy.m_t = position_kart;

                enemy_clone.oneShot = oneShot;
                enemy_clone.clips = clip_audio_enemy;
                enemy_clone.duree_de_vie = Random.Range(min_duree_de_vie,max_duree_de_vie + 1);
                enemy_clone.particule_death = particle_death;

                yield return new WaitForSeconds(0.1f);

                enemy_clone.gfx.SetActive(true);
                audio_source.clip = clip_audio[1];
                audio_source.Play();
                enemy_clone.can_up = shema_du_cycle[e];

                StartCoroutine(enemy_clone.followKart());
                yield return new WaitForSeconds(frequence_apparition);
            }
        
            i++;
            yield return new WaitForSeconds(delai_cycle);
        }
    }


    public void destroyAllEnemy(){ // declenche par enemy_manger_rails

        list_enemy.RemoveAll(list_item => list_item == null);

        foreach(enemy_rails e in list_enemy){
            e.destroyEnemy();
        }
        list_enemy.Clear();         
    }

}
