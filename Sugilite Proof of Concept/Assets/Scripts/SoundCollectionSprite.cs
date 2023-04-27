using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundCollectionSprite : MonoBehaviour
{
    public Canvas inventory;

    private GameObject currentSoundCheck;

    private bool inventoryUp = false;
    public GameObject camera;

     public void Update()
    { 
       
        if (Input.GetMouseButtonDown(0)) //when left click is pressed
        {
            RaycastHit hit; //shoot ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //from mouse and camera position
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Object") //if ray hits object with object tag
            {
                Debug.Log("Hit" + hit.collider.name); 
                
                Debug.Log("Audio:" + hit.collider.gameObject.GetComponent<AudioSource>().clip);

                AudioClip audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip; //audioClip variable will become the audio clip of the object it hit
                Debug.Log("Sprite:" + hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite);
                Sprite objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite; //name will be name of object hit
                Debug.Log("Sprite:" + hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite);
                AddSound(objectSprite, audioClip); //will call add sound function with the name and the audio clip as the string and audioclip variable
            }


        }
        if (Input.GetKeyDown(KeyCode.F)) //when E is pressed
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check")
            {
                if (hit.collider.gameObject.GetComponent<AudioSource>().clip == null) //if there is no audio clip
                {
                    currentSoundCheck = hit.collider.gameObject; //and currentSoundCheck will equal object hit
                    ChooseSound(false); // calls choose sound function as false

                }
                else //if sound already assigned then
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Play(); //audio clip will play
                    Debug.Log("Sound Assigned");
                }

                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Object")
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check")
            {
                AudioSource volume = hit.collider.gameObject.GetComponent<AudioSource>();
                SoundEditing.RaiseVolume(volume);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check")
            {
                AudioSource volume = hit.collider.gameObject.GetComponent<AudioSource>();
                SoundEditing.LowerVolume(volume);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit; //shoot ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //from mouse and camera position
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check") //if ray hits object with object tag
            {
                Debug.Log("Hit" + hit.collider.name); 
                AudioClip audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip; //audioClip variable will become the audio clip of the object it hit
                Sprite objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite; //name will be name of object hit
                AddSound(objectSprite, audioClip); //will call add sound function with the name and the audio clip as the string and audioclip variable
                audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip = null; //takes audio clip out of the holder
                objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite = null; //takes sprite out of the holder
                
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !inventoryUp) //if enter is pressed when inventoryUp bool is false
        {
            inventory.gameObject.SetActive(true); //activate canvas
            inventoryUp = true; //turn inventory up bool to true
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && inventoryUp) //if enter is pressed when bool is true
        {
            inventory.gameObject.SetActive(false); //turn off canvas
            inventoryUp = false; //turn bool false
        }
        
        
    }

    private Dictionary<Sprite, AudioClip> soundsCollected = new Dictionary<Sprite, AudioClip>(); //creates new dictionary called soundsCollected
    public Image[] soundSprite; //creates array for image boxes

    public void AddSound(Sprite soundIcon, AudioClip sound) //Function takes a string and audio clip variable
    {
        soundsCollected.Add(soundIcon, sound); //string is added as the key and audio clip as value in dictionary
        DisplaySounds(soundIcon);
    }
    

    public void RemoveSound(GameObject obj) //function takes a game object
    {
        List<Sprite> keysToRemove = new List<Sprite>(); // create a list of keys to remove
        foreach(KeyValuePair<Sprite, AudioClip> keyValuePair in soundsCollected) //looks through the dictionary, grabbing the keys and corresponding value
        {
            if (keyValuePair.Key == obj.GetComponent<Image>().sprite) //if the text in the input field matches a key in the dictionary
            {
                    Sprite soundIcon = keyValuePair.Key; //the matching key will become the string for soundName variable
                    AudioClip sound = keyValuePair.Value; //the corresponding value to the key will become the audioClip for sound variable
                    currentSoundCheck.GetComponent<AudioSource>().clip = sound; //get the gameObject Audio source component and make the Audio Clip in the audio source = the sound variable from the dictionary
                    currentSoundCheck.GetComponentInChildren<SpriteRenderer>(true).sprite = soundIcon;
                    obj.GetComponent<Image>().sprite = null; //turn the sprite in inventory to null
                    obj.GetComponent<Image>().color = Color.clear; //make the transparency clear
                    ChooseSound(true); //call choose sound as bool
                    
                    keysToRemove.Add(soundIcon);

            }

        }
        foreach (Sprite key in keysToRemove)
        {
            soundsCollected.Remove(key); // remove the keys from the dictionary outside the loop
        }
    }

    public void DisplaySounds(Sprite soundIcon)
    {
        for (int i = 0; i < soundSprite.Length; i++)
            {
                if (soundSprite[i].sprite == null) //this is where sprite is placed.
                {
                    soundSprite[i].sprite = soundIcon;
                    soundSprite[i].color = Color.white;
                    return;
                }
            }
    }

    void ChooseSound(bool choose)
    {
        gameObject.GetComponent<FirstPersonDrifter>().enabled = choose; //turn the player script true or false to stop movement
        gameObject.GetComponent<MouseLook>().enabled = choose; //true or false to stop camera rotation
        camera.gameObject.GetComponent<MouseLook>().enabled = choose; //true or false to stop camera rotation
        camera.gameObject.GetComponent<LockMouse>().LockCursor(choose); //true or false to lock mouse
        camera.gameObject.GetComponent<LockMouse>().enabled = choose; 
        inventory.gameObject.SetActive(!choose); 
        inventoryUp = !choose; 
        Cursor.visible = !choose;
        
    }
    

}
