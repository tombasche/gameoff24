using System.Collections;
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    [SerializeField]
    int shakes = 5;

    [SerializeField]
    int shakeMagnitude = 15;

    [SerializeField]
    float timeBetweenShakes = 0.1f;

    private void OnEnable()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        for(int i = 0; i <= shakes; i++)
        {
            transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude));
            yield return new WaitForSeconds(timeBetweenShakes);
        }
    }
}
