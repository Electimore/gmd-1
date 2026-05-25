using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject disclaimerCanvas;
    [SerializeField] private CanvasGroup introCanvasGroup;
    [SerializeField] private AudioSource introAudioSource;
    [SerializeField] private string nextSceneName = "MainGame";
    [SerializeField] private float fadeSpeed = 1f;

    private void Start()
    {
        disclaimerCanvas.SetActive(true);
        introCanvasGroup.gameObject.SetActive(false);
        introCanvasGroup.alpha = 0f;
    }

    public void OnProceed()
    {
        disclaimerCanvas.SetActive(false);
        introCanvasGroup.gameObject.SetActive(true);
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        while (introCanvasGroup.alpha < 1f)
        {
            introCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        introAudioSource.Play();

        yield return new WaitForSeconds(introAudioSource.clip.length);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextSceneName);
    }
}