using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    bool isInRange = false;
    [SerializeField]
    bool isHidden = false;

    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
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
        if (collision.CompareTag("Hideable") && !isInRange)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable") && isInRange)
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (playerControls.Player.Hide.WasPressedThisFrame() && isInRange)
        {
            if (isHidden)
            {
                DontHide();
            }
            else
            {
                Hide();
            }
        }
    }

    public void Hide()
    {
        if (isInRange)
        {
            animator.SetBool("isHiding", true);
            isHidden = true;
        }

    }
    public void DontHide()
    {
        animator.SetBool("isHiding", false);
        isHidden = false;
    }
    public bool IsHidden() => isHidden;
}