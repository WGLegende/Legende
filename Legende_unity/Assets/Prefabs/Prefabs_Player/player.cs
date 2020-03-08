using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float speed = 5;
    public float speedRotation = 5;
    Animator anim;
    public Transform camera_container;
    
    void Start()
    {
        // anim = GetComponent<Animator>();
        // Get the state of buttons, triggers and stick directions : hinput.gamepad[​0​].A.pressed hinput.gamepad[​6​].leftTrigger.pressed hinput.anyGamepad.rightStick.left.pressed 
        // Get the state of sticks and D-Pads : hinput.gamepad[​4​].leftStick.position hinput.gamepad[​1​].dPad.position 
        // Other useful features : hinput.gamepad[​0​].X.justPressed hinput.gamepad[​2​].rightBumper.doublePress hinput.anyGamepad.rightStick.vertical hinput.gamepad[​7​].Vibrate(0.5); 
        Vector3 leftStick = hinput.gamepad[0].leftStick.worldPositionFlat;
    }

    void FixedUpdate()
    {

        // Vector3 leftStick = hinput.gamepad[0].leftStick.worldPositionFlat;
        // Vector3 rightStick = hinput.gamepad[0].rightStick.worldPositionFlat;
        // // stick.x -1 = left
        // // stick.x 1 = right
        // // stick.z -1 = down
        // // stick.z 1 = up

        // if(leftStick.x < 0 || leftStick.x > 0 || leftStick.z < 0 || leftStick.z > 0){ // Mouvement left stick
        //     // anim.SetFloat("Speed", Mathf.Abs(leftStick.z));
        //     transform.Translate(leftStick * speed  * Time.deltaTime * (leftStick.z < 0 ? 0.5f : 1f), Space.Self);
        // }
        // else if(anim.GetFloat("Speed") != 0){
        //     // anim.SetFloat("Speed", 0);
        // }

        // if(rightStick.x < 0 || rightStick.x > 0 || rightStick.z < 0 || rightStick.z > 0){ // Mouvement right stick
        //     transform.Rotate(0, rightStick.x * speedRotation  * Time.deltaTime, 0, Space.Self); // rotate right/left character.

        //     if(rightStick.z < -0.2 || rightStick.z > 0.2){ // Rotate up/Down camera.
        //         float camera_Y = rightStick.z * speedRotation/5  * Time.deltaTime;
        //         float angle = camera_container.transform.localEulerAngles.x;
        //         angle = (angle > 180) ? angle - 360 : angle;

        //         camera_Y = angle < -40 && rightStick.z > 0 ? 0 : angle > 20 && rightStick.z < 0?  0 : camera_Y;
        //         camera_container.transform.Rotate(-camera_Y, 0, 0, Space.Self);
        //     }
        // }

        // anim.SetBool("Attacking", hinput.gamepad[0].B.pressed);
    }
}
