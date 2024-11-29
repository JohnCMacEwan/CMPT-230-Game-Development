using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<AudioClip> music;

    private float musicDuration = 0;

    private int clip = 0;

    private void Update()
    {
        if (Time.time >= musicDuration)
        {
            source.clip = music[clip];
            source.Play();
            clip++;
            if (clip >= music.Count) clip = 0;
            musicDuration = Time.time + source.clip.length;
        }
    }
}
