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

    Switch switchInFrontOf = null;

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

        if (collision.CompareTag("Switch") && switchInFrontOf == null)
        {
            switchInFrontOf = collision.gameObject.GetComponent<Switch>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Computer") && inFrontOfPC)
        {
            inFrontOfPC = false;
        }

        if (collision.CompareTag("Switch") && switchInFrontOf != null)
        {
            switchInFrontOf = null;
        }
    }

    private void Update()
    {
        if (inFrontOfPC && playerControls.Player.Interact.WasPressedThisFrame())
        {
            audioSource.PlayOneShot(startPCSound);
            OnPCClicked?.Invoke();
        }

        if (switchInFrontOf != null && playerControls.Player.Interact.WasPressedThisFrame())
        {
            switchInFrontOf.Activate();
        }
    }
}

