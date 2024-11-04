using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    bool inFrontOfPC = false;

    [SerializeField]
    AudioClip startPCSound;

    AudioSource audioSource;

    PlayerControls playerControls;

    public static event Action OnPCClicked;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Computer") && !inFrontOfPC)
        {
            inFrontOfPC = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Computer") && inFrontOfPC)
        {
            inFrontOfPC = false;
        }
    }

    private void Update()
    {
        if (inFrontOfPC && playerControls.Player.Interact.WasPressedThisFrame())
        {
            audioSource.PlayOneShot(startPCSound);
            OnPCClicked?.Invoke();
        }
    }
}

