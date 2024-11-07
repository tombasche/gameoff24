using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    bool shouldPauseHere = false;

    public bool GetShouldPause() => shouldPauseHere;
}
