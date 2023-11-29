using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuHandler : MonoBehaviour
{
    public AudioMixer mainMixer;
    public GameObject pauseMenu;

    public void masterVolumeSlider(float sliderValue){
        mainMixer.SetFloat("masterVolume", sliderValue);
    }

    public void musicVolumeSlider(float sliderValue){
        mainMixer.SetFloat("musicVolume", sliderValue);
    }

    public void soundVolumeSlider(float sliderValue){
        mainMixer.SetFloat("soundVolume", sliderValue);
    }

    public void returnToPauseMenu(){
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        transform.position = new Vector3(0, -10, 0);
    }
}
