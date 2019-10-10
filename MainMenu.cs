using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void Start()
    {
        Parameters.AI = false;
    }

    public void SetMusicVolume(float volume) {
        if (volume < -49) {
            volume = -80;
        }
        audioMixer.SetFloat("musicVolume", volume);
    }
    public void SetFXVolume(float volume) {
        if (volume < -49) {
            volume = -80;
        }
        audioMixer.SetFloat("fxVolume", volume);
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ExitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }


}
