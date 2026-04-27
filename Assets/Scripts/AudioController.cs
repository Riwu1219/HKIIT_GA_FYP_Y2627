using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public float defaultVolume = 1f;

    public bool isAudioPlaying => audioSource.isPlaying;

    private void Awake()
    {
        defaultVolume = audioSource.volume; // store original
    }

    public void PlayAudio()
    {
        audioSource.volume = defaultVolume; // reset volume
        audioSource.Play();
    }

    public void FadeInAudio(float fadeLength)
    {
        if (audioSource.isPlaying) { return; }

        audioSource.volume = 0;
        audioSource.Play();
        StartCoroutine(VolumeFade(defaultVolume, fadeLength));
    }

    public void FadeOutAudio(float fadeLength)
    {
        StartCoroutine(VolumeFade(0f, fadeLength));
    }

    public IEnumerator VolumeFade(float targetVolume, float fadeLength)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeLength)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeLength);
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (targetVolume == 0f)
        {
            audioSource.Stop();
            audioSource.volume = defaultVolume; // <-- critical fix
        }
    }
}