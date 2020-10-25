using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scripts_manager : MonoBehaviour
{
    // Use this to start other scripts in good order
    void Awake()
    {
        Invoke("start_scripts", 0.5f);




    }

    void start_scripts(){

        player_life.instance.Start_player_life();
        player_armor.instance.Start_player_armor();

    }



}
