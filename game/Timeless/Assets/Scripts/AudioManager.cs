using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public float targetVolume;
    public float fadeInDuration;
    public float fadeOutDuration;

    void Start()
    {
        musicSource.volume = 0f;
        musicSource.Play();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, 1f, elapsed / fadeInDuration);
            yield return null;
        }
        musicSource.volume = 1f;

        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(1f, targetVolume, elapsed / fadeOutDuration);
            yield return null;
        }
        musicSource.volume = targetVolume;
    }
}