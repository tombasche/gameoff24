using UnityEngine;

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

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
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
        // Move from one node to the next pausing for nodePauseTime
        // Calculate the movement to the next node in terms of x=(-1,1) & y=(-1,1)
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

        if (CanSeePlayer())
        {
            Debug.Log("gotcha!");
        }
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

    bool CanSeePlayer()
    {
        return false;
    }
}
