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

    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        fadeOut.TriggerFadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
