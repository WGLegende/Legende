using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
 
    void Start() {
        if(destroyPlayerPrefs){
            PlayerPrefs.DeleteAll();
        }
    }
}
