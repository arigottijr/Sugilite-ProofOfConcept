using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SliderCode : MonoBehaviour
{
  [SerializeField] private AudioMixer mixer;
  [SerializeField] private AudioSource source;
  [SerializeField] private TextMeshProUGUI valueText;
  [SerializeField] private AudioMixMode mixMode;
  
  public void OnChangeSlider(float value)
  {
    valueText.text = (value*100).ToString("N0");

    switch (mixMode)
    {
      case AudioMixMode.LinearAudioSourceVolume:
        source.volume = value;
        break;
      case AudioMixMode.LinearMixerVolume:
        mixer.SetFloat("Volume", (-80 + value * 80));
        break;
      case AudioMixMode.LogrithmicMixerVolume:
        mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        break;
    }
  }

  public enum AudioMixMode
  {
    LinearAudioSourceVolume,
    LinearMixerVolume,
    LogrithmicMixerVolume
  }
}
