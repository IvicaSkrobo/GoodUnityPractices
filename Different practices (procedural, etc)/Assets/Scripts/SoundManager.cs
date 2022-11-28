using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void PlayMusic()
    {

    }

    public void PlaySfx()
    {

    }

    public void SetMusicVolume()
    {

    }

    public void SetSfxVolume()
    {

    }


}
