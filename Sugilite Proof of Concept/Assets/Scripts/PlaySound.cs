using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
  public GameObject source1, source2, source3, source4,
  source5;
  public bool soundsPlaying = false;
  
  public void PlaySounds()
  {
      
      source1.GetComponent<AudioSource>().Play();
      source2.GetComponent<AudioSource>().Play();
      source3.GetComponent<AudioSource>().Play();
      source4.GetComponent<AudioSource>().Play();
      source5.GetComponent<AudioSource>().Play();
     

  }
  
}
