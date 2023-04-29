using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
  public GameObject source1, source2, source3, source4,
  source5, source6;
  public bool soundsPlaying = false;
  
  public void PlaySounds()
  {
      if (soundsPlaying)
      {
          source1.GetComponent<AudioSource>().Play();
          source2.GetComponent<AudioSource>().Play();
          source3.GetComponent<AudioSource>().Play();
          source4.GetComponent<AudioSource>().Play();
          source5.GetComponent<AudioSource>().Play();
          source6.GetComponent<AudioSource>().Play();
          soundsPlaying = !soundsPlaying;
      }
      else if (!soundsPlaying)
      {
          source1.GetComponent<AudioSource>().Stop();
          source2.GetComponent<AudioSource>().Stop();
          source3.GetComponent<AudioSource>().Stop();
          source4.GetComponent<AudioSource>().Stop();
          source5.GetComponent<AudioSource>().Stop();
          source6.GetComponent<AudioSource>().Stop();
          soundsPlaying = !soundsPlaying;
      }
      
     
  }
  
}
