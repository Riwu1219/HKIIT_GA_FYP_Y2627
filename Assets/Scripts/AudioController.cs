using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public float defaultVolume;
    public bool isAudioPlaying => audioSource.isPlaying;

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void FadeInAudio(float fadeLength)
    {
        if (audioSource.isPlaying) { return; }
        float endVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();
        StartCoroutine(VolumeFade(audioSource, endVolume, fadeLength));
    }

    public void FadeOutAudio(float fadeLength)
    {
        StartCoroutine(VolumeFade(audioSource, 0f, fadeLength));
    }

    public IEnumerator VolumeFade(AudioSource _AudioSource, float endVolume, float fadeLength)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeLength)
        {
            audioSource.volume = startVolume + ((endVolume - startVolume) * ((Time.time - startTime) / fadeLength));
            yield return null;
        }

        if (endVolume == 0) { audioSource.Stop(); }
    }
}
