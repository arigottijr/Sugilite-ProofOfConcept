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
    public Canvas soundEditing;
    public Canvas reticle;

    private GameObject currentDrumPad;

    private bool inventoryUp = false;
    private bool soundEditorUp = false;
    private bool placeSoundUIUp = false;
    
    public GameObject camera;
    public TextMeshProUGUI backButtonDes;
    public GameManager gameManager;

    public AudioSource feedback;

     public void Update()
    { 
       
        if (Input.GetMouseButtonDown(0)) //when left click is pressed
        {
            RaycastHit hit; //shoot ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //from mouse and camera position
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Object") //if ray hits object with object tag
            {
                AudioClip audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip; //audioClip variable will become the audio clip of the object it hit
                Sprite objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite; //name will be name of object hit
                AddSound(objectSprite, audioClip); //will call add sound function with the name and the audio clip as the string and audioclip variable
                feedback.Play(); //play feedback sound
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check")
            {
                if (hit.collider.gameObject.GetComponent<AudioSource>().clip == null) //if there is no audio clip
                {
                    currentDrumPad = hit.collider.gameObject; //and currentSoundCheck will equal object hit
                    LockMouseAndCharacter(false); // calls choose sound function as false
                    ShowInventoryUI(true);
                    reticle.gameObject.SetActive(false);
                    placeSoundUIUp = true;
                    backButtonDes.gameObject.SetActive(true);
                }
            }
            
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "SoundEditor") //if ray hits object with object tag
            {
                if (soundEditorUp == false)
                {
                    soundEditing.gameObject.SetActive(true);
                    soundEditorUp = true;
                    LockMouseAndCharacter(false);
                    reticle.gameObject.SetActive(false);
                }
            }
        }
      

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (soundEditorUp == true)
            {
                soundEditing.gameObject.SetActive(false);
                soundEditorUp = false;
                LockMouseAndCharacter(true);
                reticle.gameObject.SetActive(true);
            }

            if (placeSoundUIUp == true)
            {
                ShowInventoryUI(false);
                LockMouseAndCharacter(true);
                placeSoundUIUp = false;
                backButtonDes.gameObject.SetActive(false);
                reticle.gameObject.SetActive(true);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit; //shoot ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //from mouse and camera position
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check") //if ray hits object with object tag
            {
                currentDrumPad = hit.collider.gameObject;
                AudioClip audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip; //audioClip variable will become the audio clip of the object it hit
                Sprite objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite; //name will be name of object hit
                audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip = null; //takes audio clip out of the holder
                objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite = null; //takes sprite out of the holder
                gameManager.RemoveSoundName(currentDrumPad);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !inventoryUp) //if enter is pressed when inventoryUp bool is false
        {
            ShowInventoryUI(true);
            LockMouseAndCharacter(false);
            reticle.gameObject.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && inventoryUp) //if enter is pressed when bool is true
        {
           ShowInventoryUI(false);
           LockMouseAndCharacter(true);
           reticle.gameObject.SetActive(true);

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
                    currentDrumPad.GetComponent<AudioSource>().clip = sound; //get the gameObject Audio source component and make the Audio Clip in the audio source = the sound variable from the dictionary
                    currentDrumPad.GetComponentInChildren<SpriteRenderer>(true).sprite = soundIcon;
                    obj.GetComponent<Image>().sprite = null; //turn the sprite in inventory to null
                    obj.GetComponent<Image>().color = Color.clear; //make the transparency clear
                    LockMouseAndCharacter(true); //call choose sound as bool
                    ShowInventoryUI(false);
                    
                    keysToRemove.Add(soundIcon);
                    
                    gameManager.ChangeSoundName(sound.ToString().Replace("(UnityEngine.AudioClip)", ""), currentDrumPad); //sound name will equal the sound and current drumpad will be currentdrumpad

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

    void LockMouseAndCharacter(bool choose)
    {
        gameObject.GetComponent<FirstPersonDrifter>().enabled = choose; //turn the player script true or false to stop movement
        gameObject.GetComponent<MouseLook>().enabled = choose; //true or false to stop camera rotation
        camera.gameObject.GetComponent<MouseLook>().enabled = choose; //true or false to stop camera rotation
        camera.gameObject.GetComponent<LockMouse>().LockCursor(choose); //true or false to lock mouse
        camera.gameObject.GetComponent<LockMouse>().enabled = choose;
        Cursor.visible = !choose;
        
    }

    void ShowInventoryUI(bool value)
    {
        inventory.gameObject.SetActive(value); 
        inventoryUp = value;
    }
    
    

}
