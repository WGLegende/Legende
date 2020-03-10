using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerKeyboard : MonoBehaviour

{



public float walkSpeed;
public float turnSpeed;
public string inputFront;
public string inputBack;
public string inputLeft;
public string inputRight;

  float horizontalInput;
    void Start()
    {
          
//     horizontalInput = Input.GetAxis ("JoystickLeftX"); 
      
    }

  
    void FixedUpdate()
    {
        // if (Input.GetButton("ButtonX")){
        //     print("bouton X");
        // }
        //  if (Input.GetButton("ButtonA")){
        //     print("bouton A");
        // }
        //  if (Input.GetButton("ButtonB")){
        //     print("bouton B");
        // }
        //  if (Input.GetButton("ButtonY")){
        //     print("bouton Y");
        // }
        //  if (Input.GetButton("ButtonStart")){
        //     print("bouton Start");
        // }
        //  if (Input.GetButton("ButtonBack")){
        //     print("bouton Back");
        // }
          
        //        print(horizontalInput);

        
        //         if (Input.GetKey(inputFront)){
        //     transform.Translate(0,0,walkSpeed * Time.deltaTime);
        // }
        // if (Input.GetKey(inputBack)){
        //     transform.Translate(0,0,-walkSpeed * Time.deltaTime);
        // }
        // if (Input.GetKey(inputLeft)){
        //     transform.Rotate(0,-turnSpeed* Time.deltaTime,0);
        // }
        // if (Input.GetKey(inputRight)){
        //     transform.Rotate(0,turnSpeed* Time.deltaTime,0);
        // }
    }
}
