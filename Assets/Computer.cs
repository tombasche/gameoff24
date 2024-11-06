using UnityEngine;

public class Computer : MonoBehaviour
{
    LevelManager levelManager;

    [SerializeField]
    ParticleSystem clickedParticlesZeros;

    [SerializeField]
    ParticleSystem clickedParticlesOnes;

    private void Awake()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    private void OnEnable()
    {
        PlayerInteraction.OnPCClicked += Handle_OnPCClicked;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnPCClicked -= Handle_OnPCClicked;
    }

    void Handle_OnPCClicked()
    {
        // Show "Downloading" bar - Downloading secrets ... 
        clickedParticlesOnes.Play();
        clickedParticlesZeros.Play();
        levelManager.WonLevel();
    }
}
