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

    public void PlayAudio(MusicController.TypeMusic _audioType)
    {
        if (_audioType == MusicController.TypeMusic.Fire)
        {
            _musicController.audioSource_Element.PlayOneShot(_musicController.listAudioClip[0]);
        }
        if(_audioType == MusicController.TypeMusic.FireLoop)
        {
            _musicController.audioSource_Element.PlayOneShot(_musicController.listAudioClip[1]);

        }
        if (_audioType == MusicController.TypeMusic.TouchBorder)
        {
            _musicController.audioSource_Element.PlayOneShot(_musicController.listAudioClip[2]);

        }
        if (_audioType == MusicController.TypeMusic.TouchZombie)
        {
            _musicController.audioSource_Element.PlayOneShot(_musicController.listAudioClip[3]);

        }
        if (_audioType == MusicController.TypeMusic.Bomb)
        {
            _musicController.audioSource_Element.PlayOneShot(_musicController.listAudioClip[Random.Range(4,7)]);

        }
    }
    public void PlayerBackGround()
    {
        //_musicController.audioSource_BackGround.Play();
    }
}
