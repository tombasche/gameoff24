using UnityEngine;

public class GameSettings : MonoBehaviour
{

    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
    }

    void Update()
    {
        // TODO replace with opening a menu - setting Time.timeScale = 0
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
