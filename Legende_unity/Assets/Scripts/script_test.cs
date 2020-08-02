using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]

public class script_test : MonoBehaviour{


    public Transform objetToMove;
    public Transform start;
    public Transform end;
    public Transform PointSpline;
   
   
    public float vitessekart = 10f;

    public float test = 0;


    void Start()
    {
        print("x :"+PointSpline.transform.position.x);
        print("y :"+PointSpline.transform.position.y);
        print("z :"+PointSpline.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       
       //objetToMove.transform.position = Vector3.Lerp(start.position,end.position,test);
        //transform.rotation = Quaternion.Lerp(start.rotation,end.rotation,test);
      
       
        
     
        
    }
}
