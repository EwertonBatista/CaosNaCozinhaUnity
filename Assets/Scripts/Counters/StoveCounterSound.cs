using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSFX = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSFX)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
