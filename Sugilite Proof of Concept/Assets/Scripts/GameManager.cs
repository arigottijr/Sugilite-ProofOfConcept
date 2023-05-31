using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ChangeSoundName
    public TextMeshProUGUI[] drumPadText;
    public GameObject[] drumPad;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void ChangeSoundName(string soundName, GameObject clickedDrumPad) //function calls for string soundname and clicked drumpad
    {
        
        for (int i = 0; i < drumPad.Length; i++) //will look through the array of drumpads
        {
            if (drumPad[i] == clickedDrumPad) //if drumpad it loops through = the clicked drumpad
            {
                Debug.Log("if statement called");
                drumPadText[i].text = soundName; //change the text with the string given
                
            }
        }
    }

    public void RemoveSoundName(GameObject clickedDrumPad) //similar to above function
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
        if (!soundsPlaying) //if bool is false
        {
            for (int i = 0; i < drumPad.Length ; i++) //loop through the drumpads
            {
                if (drumPad[i].GetComponent<AudioSource>().clip != null) //then if the drumpad audio source has a clip
                {
                    drumPad[i].GetComponent<AudioSource>().Play(); //play the clip
                }
            }

            soundsPlaying = !soundsPlaying; //turn bool opposite to what it ccurrently is
            return; //return and stop code
        }
      
        if (soundsPlaying) //if sound is playing
        {
            for (int i = 0; i < drumPad.Length; i++) // loop through drumpads
            {
                if (drumPad[i].GetComponent<AudioSource>().clip != null) //and if clip is not null
                {
                    drumPad[i].GetComponent<AudioSource>().Stop(); //stop the sound
                }
            }
            soundsPlaying = !soundsPlaying;
        }
    }
    #endregion

}
