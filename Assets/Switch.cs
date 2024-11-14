using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    string forceFieldTag;

    public void Activate()
    {
        var forceField = GameObject.FindGameObjectWithTag(forceFieldTag);
        if (forceField != null)
        {
            forceField.SetActive(!forceField.activeSelf);
        }

        Quaternion rotation = transform.rotation;
        rotation.z = 180;
        transform.rotation = rotation;
    }
}
