using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    float fadeOutTime = 2f;

    FadeOut fadeOut;

    private void Awake()
    {
        fadeOut = FindFirstObjectByType<FadeOut>();
    }

    private void Start()
    {
        fadeOut.TriggerFadeIn();
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
        yield return new WaitForSeconds(1f);
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);
        // Play victory sound
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        
        // TODO check if last level and get next scene
        // int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }
}
