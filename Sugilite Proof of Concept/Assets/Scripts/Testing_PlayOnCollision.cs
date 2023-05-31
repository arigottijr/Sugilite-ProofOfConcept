using UnityEngine;

public class Testing_PlayOnCollision : MonoBehaviour
{
    public AudioSource objectAudio;
    bool isPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        objectAudio = GetComponent<AudioSource>();
        isPlaying = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (!isPlaying)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlaying = true;
                objectAudio.PlayOneShot(objectAudio.clip);
            }
            
            Debug.Log("CollisionAudioSource: " + objectAudio);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isPlaying)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlaying = false;
            }
        }
    }
}
