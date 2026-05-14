using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    public static AudioFeedback Instance;

    [SerializeField] private AudioSource audioSource;

    [Header("Destroyed by player")]
    [SerializeField] private AudioClip correctDestroySound;
    [SerializeField] private AudioClip wrongDestroySound;

    [Header("Reached AI")]
    [SerializeField] private AudioClip safeReachedAISound;
    [SerializeField] private AudioClip badReachedAISound;

    [Header("Player")]
    [SerializeField] private AudioClip scissorsSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCorrectDestroy()
    {
        audioSource.PlayOneShot(correctDestroySound);
    }

    public void PlayWrongDestroy()
    {
        audioSource.PlayOneShot(wrongDestroySound);
    }

    public void PlaySafeReachedAI()
    {
        audioSource.PlayOneShot(safeReachedAISound);
    }

    public void PlayBadReachedAI()
    {
        audioSource.PlayOneShot(badReachedAISound);
    }

    public void PlayScissors()
    {
        audioSource.PlayOneShot(scissorsSound);
    }
}
