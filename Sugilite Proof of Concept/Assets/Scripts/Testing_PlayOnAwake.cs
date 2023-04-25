using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_PlayOnAwake : MonoBehaviour
{
    public AudioSource objectAudio;
    string objectName;

    public bool isPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        objectName = objectAudio.gameObject.name;
        isPlaying = false;
        Debug.Log("Audio output: " + objectName + " & " + objectAudio);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            objectAudio.loop = true;
            objectAudio.Play();
            if (objectAudio == null)
            {
                Debug.Log("Sound is null on: " + objectName);
            }
        }
    }
}
