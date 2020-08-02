using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_nest_rails : MonoBehaviour
{
    public static enemy_nest_rails instance;

    public int nbr_cycle = 1;
    public float delai_cycle;
    
    [Header("Check for Enemy Up")]
    public bool[] shema;
    public float frequence_apparition;

    [Header("Edition")]
    public GameObject enemy_rails;
    public Animation anim;
    public AudioClip[] clip_audio;

    [Header("Enemy en Jeu")]
    public List<enemy_rails> list_enemy = new List<enemy_rails>();
   
    [HideInInspector] public Battlehub.MeshDeformer2.SplineFollow SplineFollowKart;
    [HideInInspector] public bool justeOnce;
    float position_kart;
    AudioSource audio_source;
   

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

            for(int e = 0; e < shema.Length; e++){

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

                yield return new WaitForSeconds(0.1f);
                enemy_clone.gfx.SetActive(true);
                audio_source.clip = clip_audio[1];
                audio_source.Play();
                enemy_clone.can_up = shema[e];

                StartCoroutine(enemy_clone.followKart());
                yield return new WaitForSeconds(frequence_apparition);
            }
        
            i++;
            yield return new WaitForSeconds(delai_cycle);
        }
    }


    public void destroyAllEnemy(){

        list_enemy.RemoveAll(list_item => list_item == null);

        foreach(enemy_rails e in list_enemy){
            e.destroyEnemy();
        }
        list_enemy.Clear();         
    }

}
