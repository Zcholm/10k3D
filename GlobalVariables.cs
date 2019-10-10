using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    private static bool bgMusic;
    private static bool soundFx;

    public static bool BGMusic {
        get {
            return bgMusic;
        }
        set {
            bgMusic = value;
        }
    }

    public static bool SoundFx {
        get {
            return soundFx;
        }
        set {
            soundFx = value;
        }
    }



}
