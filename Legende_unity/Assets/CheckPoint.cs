using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter(){

         GameObject.Find ("MineCart").GetComponent<Animator>().SetBool("newChariot", false); // affichage ui clé    
    }
}
