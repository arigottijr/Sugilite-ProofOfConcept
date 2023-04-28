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
      
      source1.gameObject.GetComponent<AudioSource>().Play();
      source2.gameObject.GetComponent<AudioSource>().Play();
      source3.gameObject.GetComponent<AudioSource>().Play();
      source4.gameObject.GetComponent<AudioSource>().Play();
      source5.gameObject.GetComponent<AudioSource>().Play();
     

  }
  
}
