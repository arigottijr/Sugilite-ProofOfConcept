using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEditing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RaiseVolume(AudioSource volume)
    {
        volume.volume += 0.1f;
    }

    public static void LowerVolume(AudioSource volume)
    {
        volume.volume -= 0.1f;
    }
}
