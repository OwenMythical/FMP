using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioSource Audio;
    public int MinPitch;
    public int MaxPitch;
    void Start()
    {
        StartCoroutine(SoundPlay());
    }

    IEnumerator SoundPlay()
    {
        while (true)
        {
            float RandomPitch = (float)Random.Range(MinPitch, MaxPitch) / 10f;
            Audio.pitch = RandomPitch;
            yield return new WaitForSeconds(Random.Range(5, 10));
            Audio.Play();
        }
    }
}
