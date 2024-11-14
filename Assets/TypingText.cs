using System.Collections;
using TMPro;
using UnityEngine;

public class TypingText : MonoBehaviour
{
    [SerializeField]
    AudioClip typingSound;

    [SerializeField]
    AudioClip thud;

    AudioSource audioSource;

    TextMeshProUGUI text;

    [SerializeField]
    string[] textString;

    [SerializeField]
    float delayBetweenCharacters;

    [SerializeField]
    float timeBetweenLines = 1f;

    [SerializeField]
    float lineTtl = 5f;

    float startingPitch;

    [SerializeField]
    bool startGame = true;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        textString = text.text.Split("\n");
        text.text = "";
        startingPitch = audioSource.pitch;
    }

    private void Start()
    {
        StartCoroutine(BuildText());
    }

    IEnumerator BuildText()
    {
        foreach (string textPiece in textString)
        {
            for (int i = 0; i < textPiece.Length; i++)
            {
                text.text = string.Concat(text.text, textPiece[i]);
                PlayTypingSound();
                yield return new WaitForSeconds(delayBetweenCharacters);
            }
            yield return new WaitForSeconds(timeBetweenLines);

            text.text += "\n";
        }
        yield return new WaitForSeconds(lineTtl);
        if (startGame)
        {
            FindFirstObjectByType<StartScreen>().StartGame();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(thud, Camera.main.transform.position, 0.8f);
        }
        else
        {
            Application.Quit();
        }

    }

    void PlayTypingSound()
    {
        float randomAmount = UnityEngine.Random.Range(-0.1f, 0.1f);
        audioSource.pitch += randomAmount;
        audioSource.PlayOneShot(typingSound);
        audioSource.pitch = startingPitch;
    }
}