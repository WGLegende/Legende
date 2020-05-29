
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class clipSound {


    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 1f;

    [Range(.1f,3f)] 
    public float pitch = 1f;

    public bool loop;
    
    [Range(0f,1f)]
    public float spatialBlend;

    [HideInInspector]
    public AudioSource source;


}
