using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    FadeOut fadeOut;

    private void Awake()
    {
        fadeOut = FindFirstObjectByType<FadeOut>();
    }

    private void Start()
    {
        fadeOut.TriggerFadeIn();
        BackgroundMusic.Instance.StartMusic();
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
        BackgroundMusic.Instance.StopMusic();
        yield return new WaitForSeconds(1f);
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel);
    }

    IEnumerator LoadNextLevel()
    {
        BackgroundMusic.Instance.StopMusic();
        yield return new WaitForSeconds(1f);
        // Play victory sound
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        
        // TODO check if last level and get next scene
        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel + 1);
    }
}
