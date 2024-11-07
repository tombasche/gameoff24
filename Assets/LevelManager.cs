using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    AudioSource audioSource;

    FadeOut fadeOut;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fadeOut = FindFirstObjectByType<FadeOut>();
    }

    private void Start()
    {
        fadeOut.TriggerFadeIn();
        audioSource.Play();
    }

    public void LostLevel()
    {
        StartCoroutine(RestartLevel());
    }

    public void WonLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator RestartLevel()
    {
        audioSource.Stop();
        yield return new WaitForSeconds(1f);
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel);
    }

    IEnumerator LoadNextLevel()
    {
        audioSource.Stop();
        yield return new WaitForSeconds(1f);
        // Play victory sound
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        
        // TODO check if last level and get next scene
        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel + 1);
    }
}
