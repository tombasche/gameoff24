using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    float nodePauseTime = 3f;
    float pauseTimer = 0f;

    [SerializeField]
    Transform pathNodesParent;

    Node[] movementNodes;

    EnemyController controller;

    [SerializeField]
    int currentNodeIdx;

    [SerializeField]
    float distanceToNodeThreshold = 0.12f;

    [SerializeField]
    float viewDistance = 10f;

    [SerializeField]
    float soundRadius = 2f; // if you're moving next to them

    [SerializeField]
    Transform exclamationMark;

    [SerializeField]
    AudioClip alarmSound;

    AudioSource audioSource;

    bool runningAnimation = false;
    Animator animator;

    PlayerController playerController;

    public enum State
    {
        Patrolling,
        SeeingPlayer,
        SpottedPlayer,
        Panicking,
    }

    [SerializeField]
    State state;

    private void Awake()
    {
        state = State.Patrolling;
        controller = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerController = FindFirstObjectByType<PlayerController>();

        movementNodes = pathNodesParent.GetComponentsInChildren<Node>();

        SetInitialPositionToFirstNode();
    }

    void Start()
    {
        currentNodeIdx = 0;
    }

    void SetInitialPositionToFirstNode()
    {
        transform.position = movementNodes[0].transform.position;
    }

    private void Update()
    {
        HandleState();
        LookForPlayer();
    }

    int NextNodeIdx(int currentNode)
    {
        int nextNodeIdx = currentNode + 1;

        // At end of array, go back to start
        if (nextNodeIdx == movementNodes.Length)
        {
            nextNodeIdx = 0;
        }
        return nextNodeIdx;
    }

    Node NextNode(int currentNode)
    {
        int idx = NextNodeIdx(currentNode);
        return movementNodes[idx];
    }

    bool IsCloseToPosition(Node n) => 
        Vector3.Distance(transform.position, n.transform.position) <= distanceToNodeThreshold;

    void LookForPlayer()
    {
        if (state == State.Patrolling)
        {
            var ray = controller.FacingDirection() * viewDistance;
            Debug.DrawRay(transform.position, ray, Color.red);

            var noFilter = new ContactFilter2D().NoFilter();
            List<RaycastHit2D> results = new();

            Physics2D.Raycast(transform.position, ray, noFilter, results);

            if (results.Count == 0) return;

            RaycastHit2D closestObject = results.OrderBy(r => r.distance).First();

            // We found the player object
            if (closestObject.collider.CompareTag("Player"))
            {
                // Now are they hidden?
                if (!closestObject.collider.GetComponent<PlayerHiding>().IsHidden())
                {
                    SoundTheAlarm();
                }
            }

            if (PlayerMovingNearby())
            {
                SoundTheAlarm();
            }
        }
    }

    bool PlayerMovingNearby()
    {
        if (!playerController.IsMoving()) return false;

        var playerPosition = playerController.transform.position;

        bool playerMovingNearby = Vector2.Distance(transform.position, playerPosition) < soundRadius;

        if (!playerMovingNearby) return false;

        // If the player _is_ moving nearby, draw a line from the enemy to the player
        // and see if there's a wall blocking at least so they can't "see" the player move
        var ray = (playerPosition - transform.position).normalized;
        Debug.DrawRay(transform.position, ray, Color.blue);

        var noFilter = new ContactFilter2D().NoFilter();
        List<RaycastHit2D> results = new();

        Physics2D.Raycast(transform.position, ray, noFilter, results);

        RaycastHit2D closestObject = results.OrderBy(r => r.distance).First();
        if (closestObject.collider.CompareTag("Player") && playerMovingNearby)
        {
            return true;
        }

        return false;
    }

    void SoundTheAlarm()
    {
        audioSource.PlayOneShot(alarmSound);
        state = State.SeeingPlayer;
    }

    void HandleState()
    {
        if (state == State.Patrolling)
        {
            exclamationMark.gameObject.SetActive(false);
            Node currentNode = movementNodes[currentNodeIdx];
            Node nextNode = NextNode(currentNodeIdx);

            if (IsCloseToPosition(nextNode))
            {
                if (nextNode.GetShouldPause())
                {
                    PauseOnNode();
                }
                else
                {
                    currentNodeIdx = NextNodeIdx(currentNodeIdx);
                }
            }
            else
            {
                var nextNodePos = nextNode.transform.position;
                var currentNodePos = currentNode.transform.position;
                Vector2 difference = (nextNodePos - currentNodePos).normalized;
                controller.Movement(difference);
            }
        }

        if (state == State.SeeingPlayer)
        {
            state = State.SpottedPlayer;
            exclamationMark.gameObject.SetActive(true);
            controller.Movement(Vector2.zero);
        }

        if (state == State.SpottedPlayer)
        {
            FindFirstObjectByType<CameraControl>().ZoomToEnemy(transform);
            state = State.Panicking;
        }

        if (state == State.Panicking && !runningAnimation)
        {
            runningAnimation = true;
            animator.SetTrigger("Surprised");
            FindFirstObjectByType<LevelManager>().LostLevel();
        }
    }

    void PauseOnNode()
    {
        controller.Movement(Vector2.zero);
        pauseTimer += Time.deltaTime;

        if (pauseTimer >= nodePauseTime)
        {
            pauseTimer = 0f;
            currentNodeIdx = NextNodeIdx(currentNodeIdx);
        }
    }
}
