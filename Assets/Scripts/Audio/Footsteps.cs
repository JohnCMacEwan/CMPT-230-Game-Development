using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> steps;

    [SerializeField]
    private GameObject player;

    private AudioSource source;

    private int clip = 0;

    private void Start()
    {
        source = player.AddComponent<AudioSource>();
        source.volume = 0.1f;
    }

    public void PlayStep()
    {
        source.PlayOneShot(steps[clip]);
    }
}
