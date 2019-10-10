using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider fxSlider;
    public AudioMixer audioMixer;
    private float musicInitVolume;
    private float fxInitVolume;


    // Start is called before the first frame update
    void Start()
    {
        // Get the actual value for the 
        audioMixer.GetFloat("musicVolume", out musicInitVolume);
        if (musicInitVolume < -50) {
            musicInitVolume = -50;
        }
        musicSlider.SetValueWithoutNotify(musicInitVolume);

        if (fxInitVolume < -50) {
            fxInitVolume = -50;
        }
        audioMixer.GetFloat("fxVolume", out fxInitVolume);
        fxSlider.SetValueWithoutNotify(fxInitVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
