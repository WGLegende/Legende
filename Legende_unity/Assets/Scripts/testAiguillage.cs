using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAiguillage : MonoBehaviour
{
     
      public Transform ContainerMain;
      public Transform ContainerSwitch;
      float position1;
      float position2;

    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
         position1 = ContainerMain.position.x;
         position2 =  ContainerSwitch.position.x;
         print("container: "+position1);
         print("cube: "+position2);


           if(position1 == position2){

                print("touche !!!");
           }
    }
    

}
