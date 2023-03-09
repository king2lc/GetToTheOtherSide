using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer theMixer;
    public TMP_Text mastLabel,musicLabel,sfxLabel;
    public Slider mastSlider,musicSlider, sfxSlider;

void start(){

    //float vol = 0f;
    //theMixer.GetFloat("MasterVol",out vol);
    //mastSlider.value = vol;
    //mastLabel.text = Mathf.RoundToInt(mastSlider.value+80).ToString();

    
    }
    void update(){
        

    }
    public void SetMasterVol(){
        mastLabel.text = Mathf.RoundToInt(mastSlider.value+80).ToString();
        theMixer.SetFloat("MasterVol",mastSlider.value);

        PlayerPrefs.SetFloat("MasterVol",mastSlider.value);
    }
        public void SetMusicVol(){
        musicLabel.text = Mathf.RoundToInt(musicSlider.value+80).ToString();
        theMixer.SetFloat("MusicVol",musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol",musicSlider.value);
    }
        public void SetSFXVol(){
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value+80).ToString();
        theMixer.SetFloat("SFXVol",sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol",sfxSlider.value);
    }
}
