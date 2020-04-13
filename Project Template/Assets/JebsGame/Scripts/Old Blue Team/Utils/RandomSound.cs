using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();

    public void PlayRandom()
    {
        source.PlayOneShot(clips[Random.Range(0,clips.Count - 1)]);
    }
}
