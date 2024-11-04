using UnityEngine;

public class FadeOut : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerFadeIn()
    {
        animator.SetTrigger("StartFadeIn");
    }

    public void TriggerFadeOut()
    {
        animator.SetTrigger("StartFadeOut");
    }
}
