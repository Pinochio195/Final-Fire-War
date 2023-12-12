using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;

public class MusicManager : RingSingleton<MusicManager>
{
    [HeaderTextColor(.55f, .55f, .55f, headerText = "Music")]
    public MusicController _musicController;
    private void Start()
    {
        PlayerBackGround();
    }

    public void PlayAudio_Grenade()
    {
        //_musicController.audioSource_Grenade.PlayOneShot(_musicController.audioClip_Grenade);
    }
    public void PlayerBackGround()
    {
        //_musicController.audioSource_BackGround.Play();
    }
}
