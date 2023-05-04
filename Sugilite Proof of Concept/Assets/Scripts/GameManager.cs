using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ChangeSoundName
    public TextMeshProUGUI[] drumPadText;
    public GameObject[] drumPad;
    public void ChangeSoundName(string soundName, GameObject clickedDrumPad)
    {
        
        for (int i = 0; i < drumPad.Length; i++)
        {
            if (drumPad[i] == clickedDrumPad)
            {
                Debug.Log("if statement called");
                drumPadText[i].text = soundName;
                
            }
        }
    }

    public void RemoveSoundName(GameObject clickedDrumPad)
    {
        for (int i = 0; i < drumPad.Length; i++)
        {
            if (drumPad[i] == clickedDrumPad)
            {
                Debug.Log("if statement called");
                drumPadText[i].text = "No Sound";
            }
        }
    }
    #endregion

    #region Play Sound Function
    public bool soundsPlaying = false;
    
    public void PlaySounds()
    {
        if (!soundsPlaying)
        {
            for (int i = 0; i < drumPad.Length ; i++)
            {
                if (drumPad[i].GetComponent<AudioSource>().clip != null)
                {
                    drumPad[i].GetComponent<AudioSource>().Play();
                    Debug.Log("Playing: " + drumPad[i]);
                }
            }

            soundsPlaying = !soundsPlaying;
            return;
        }
      
        if (soundsPlaying)
        {
            for (int i = 0; i < drumPad.Length; i++)
            {
                if (drumPad[i].GetComponent<AudioSource>().clip != null)
                {
                    drumPad[i].GetComponent<AudioSource>().Stop();
                    Debug.Log("Not Playing: " + drumPad[i]);

                }
            }
            soundsPlaying = !soundsPlaying;
        }
    }
    #endregion
}
