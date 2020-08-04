using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]

public class script_test : MonoBehaviour{

    public Transform PointSpline;
    public static script_test instance;
   
    // [System.Serializable]
    // public struct MyStruct
    // {
    //     public enum MyEnum { hello, world }
    //     public MyEnum m_MyEnum;
    // }
    
    // public MyStruct[] m_MyStruct;


   
    public base_clip[] _clip_audio; 
    AudioClip clip_play;
    AudioSource source;


     public AudioClip[] testasseclip;
   // public string[] names = new string[] {"Matt", "Joanne", "Robert"};
 
 

    void Start(){

        instance = this;

        source = GetComponent<AudioSource>();

        if(PointSpline != null){
            print("x :"+PointSpline.transform.position.x);
            print("y :"+PointSpline.transform.position.y);
            print("z :"+PointSpline.transform.position.z);
        }
    }

    
    void Update(){

        if(Input.GetKeyDown("s")){

           // clip_play = base_clip.sound_clip[0];

           // source.clip =  base_clip._clip_audio.sound_clip[0];
            source.Play();
        }
      
       
    }
}
