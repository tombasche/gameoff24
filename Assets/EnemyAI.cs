using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    float nodePauseTime = 3f;
    float pauseTimer = 0f;

    [SerializeField]
    Transform[] movementNodes;

    EnemyController controller;

    [SerializeField]
    int currentNodeIdx;

    [SerializeField]
    float distanceToNodeThreshold = 0.12f;

    [SerializeField]
    float viewDistance = 10f;

    [SerializeField]
    Transform exclamationMark;

    [SerializeField]
    AudioClip alarmSound;

    AudioSource audioSource;

    bool runningAnimation = false;
    Animator animator;

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
        SetInitialPositionToFirstNode();
    }

    void Start()
    {
        currentNodeIdx = 0;
    }

    void SetInitialPositionToFirstNode()
    {
        transform.position = movementNodes[0].position;
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

    Vector3 NextNode(int currentNode)
    {
        int idx = NextNodeIdx(currentNode);
        return movementNodes[idx].position;
    }

    bool IsCloseToPosition(Vector3 position) => 
        Vector3.Distance(transform.position, position) <= distanceToNodeThreshold;

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

            var orderedResult = results.OrderBy(r => r.distance).ToList();

            RaycastHit2D closestObject = results.OrderBy(r => r.distance).First();

            if (closestObject.collider.CompareTag("Player"))
            {
                audioSource.PlayOneShot(alarmSound);
                state = State.SeeingPlayer;
            }
        }
    }

    void HandleState()
    {
        if (state == State.Patrolling)
        {
            exclamationMark.gameObject.SetActive(false);
            Vector3 currentNode = movementNodes[currentNodeIdx].position;
            Vector3 nextNode = NextNode(currentNodeIdx);

            if (IsCloseToPosition(nextNode))
            {
                controller.Movement(Vector2.zero);
                pauseTimer += Time.deltaTime;

                if (pauseTimer >= nodePauseTime)
                {
                    pauseTimer = 0f;
                    currentNodeIdx = NextNodeIdx(currentNodeIdx);
                }
            }
            else
            {
                Vector2 difference = (nextNode - currentNode).normalized;
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
}
