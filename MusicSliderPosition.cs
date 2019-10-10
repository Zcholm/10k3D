using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSliderPosition : MonoBehaviour
{
    private float sliderInitValue;
    public AudioMixer audioMixer;
    public GameObject musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.GetFloat("musicVolume", out sliderInitValue);
        if (sliderInitValue < -50) {
            sliderInitValue = -50;
        }
        //musicSlider.value = sliderInitValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
