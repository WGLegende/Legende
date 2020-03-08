using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosition : MonoBehaviour
{

     Vector3 playerPos;
    //EventManager EventManager;


    void Start()
    {
        playerPos = transform.position;
       // EventManager = GameObject.Find ("EventGame").GetComponent<EventManager>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)){
            print("Touche 0");
            transform.position = new Vector3(0.31f, 2f, 0f); // Plaine centrale
        }

        if (Input.GetKeyDown(KeyCode.Keypad1)){
            print("Touche 1");
            transform.position = new Vector3(-43f, 2f, 70f); // Faille d'Argile
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)){
            print("Touche 2");
            transform.position = new Vector3(79f, 26f, -30f); // Bourg Du Givre
        }

          if (Input.GetKeyDown(KeyCode.Keypad3)){
            print("Touche 3");
            transform.position = new Vector3(-72f, 2f, 2.1f); // SourceVie
        }

        if (Input.GetKeyDown(KeyCode.Keypad4)){
            print("Touche 4");
            transform.position = new Vector3(23f, 2f, 87f); // Ruine d'Argile
        }

         if (Input.GetKeyDown(KeyCode.Keypad5)){
            print("Touche 5");
            transform.position = new Vector3(-8.5f, 4f, -140f); // Inside
        }

        // if (GameObject.Find ("EventGame").GetComponent<EventManager>().InsideHouse == 1){
        //      transform.position = new Vector3(-0.31f, -0.49f, -99.2f); // Inside
//            EventManager.instance.InsideHouse = 2;

        // }

        // if (GameObject.Find ("EventGame").GetComponent<EventManager>().InsideHouse == 3){
        //      transform.position = new Vector3(-56f, -0.49f, 82f); // outside
        //     GameObject.Find ("EventGame").GetComponent<EventManager>().InsideHouse = 0;

        // }
    }



   
        



    
}
