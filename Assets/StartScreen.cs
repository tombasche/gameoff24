using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3.1f);
        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel + 1);
    }
}
