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
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadNextLevel()
    {
        audioSource.Stop();
        yield return new WaitForSeconds(1f);
        // Play victory sound
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        
        // TODO check if last level and get next scene
        // int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }
}
