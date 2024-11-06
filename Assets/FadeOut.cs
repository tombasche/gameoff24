using UnityEngine;

public class FadeOut : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip fadeInClip;

    [SerializeField]
    AudioClip fadeOutClip;


    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerFadeIn()
    {
        // TODO I got confused at some point?
        animator.SetTrigger("StartFadeIn");
        audioSource.PlayOneShot(fadeOutClip);
    }

    public void TriggerFadeOut()
    {
        animator.SetTrigger("StartFadeOut");
        audioSource.PlayOneShot(fadeInClip);

    }
}
