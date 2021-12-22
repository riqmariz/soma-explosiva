using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioFile;


    [Range(0f, 1f)]
    public float volume = .5f;
    [Range(0f, .5f)]
    public float RandomVolume;

    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 2f)]
    public float RandomPitch;

    public bool loop;
    public bool IsBGM;

    [HideInInspector]
    public AudioSource source;

    public void Play()
    {
        if (!this.IsBGM)
        {
            this.source.volume = this.volume + (Random.Range(0f, this.RandomVolume) - (this.RandomVolume / 2));
            this.source.pitch = this.pitch + (Random.Range(0f, this.RandomPitch) - (this.RandomPitch / 2));
        }
        this.source.Play();
    }
}

