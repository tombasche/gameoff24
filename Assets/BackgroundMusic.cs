using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }

    AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Pause();
    }
}