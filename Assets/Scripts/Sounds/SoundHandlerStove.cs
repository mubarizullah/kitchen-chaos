using UnityEngine;

public class SoundHandlerStove : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] SoundClipsSO soundClipSO;
    

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        stoveCounter.OnFryingStateChanged += StoveCounter_OnFryingStateChanged;
        audioSource.clip = soundClipSO.stoveSizzleSound;
        Debug.Log(audioSource.clip);
    }

    private void StoveCounter_OnFryingStateChanged(object sender, StoveCounter.OnFryingStateChangedEventArgs e)
    {
        bool isFryingOrFried = (e.fryingStateOfEvenetArgs == StoveCounter.FryingState.Frying || e.fryingStateOfEvenetArgs == StoveCounter.FryingState.Fried);

        if (isFryingOrFried)
        {
            if (!audioSource.isPlaying)   // check if the sound is not playing before
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Pause();
        }
    }
}
