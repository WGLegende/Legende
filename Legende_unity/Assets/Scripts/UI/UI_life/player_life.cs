using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_life : MonoBehaviour
{
    public static player_life instance;
    public RectTransform Player_Life_Container;
    public GameObject PF_Player_Life;
    public List<GameObject> _Quarter_life_list;

    int player_life_aimed;
    
    void Start()
    {
        if(instance == null){
            instance = this;
        }
        init_player_life();
    }

    void init_player_life(){
        foreach (Transform child in Player_Life_Container){
            Destroy(child.gameObject);
        }

        for(int i = 1; i <= player_main.instance.player_life_max; i++){
            if(i%4 == 0){
                _PLife newLife = Instantiate(PF_Player_Life).GetComponent<_PLife>(); // Ajoute un coeur vide
                newLife.gameObject.transform.SetParent(Player_Life_Container, false);
                for(int j = 1; j <= 4; j++){
                        _Quarter_life_list.Add(newLife.quartLife[j]);
                }
            }
        }
        change_player_life(0);
    }

    void addNewPlayerLife(){
        player_main.instance.player_life_max += 4;
        player_main.instance.player_life_current += 4;
        init_player_life();
    }
    
    public void change_player_life(float amount){
        player_main.instance.player_life_current = (int)Mathf.Clamp(player_main.instance.player_life_current + amount, 0, player_main.instance.player_life_max);
        for(int i = 0; i < _Quarter_life_list.Count;  i++){
            _Quarter_life_list[i].SetActive(i < player_main.instance.player_life_current);
        }
        if(player_main.instance.player_life_current <= 0){
            player_main.instance.player_dies();  
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            change_player_life(-3);

        }
        if(Input.GetKeyDown(KeyCode.Q)){
            change_player_life(3);

        }
    }
}
