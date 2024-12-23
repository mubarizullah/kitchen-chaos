using UnityEngine;

public class StoveCounterVsuals : MonoBehaviour
{
    [SerializeField] GameObject[] fryingVisuals;
    [SerializeField] StoveCounter stoveCounter;
    public bool showVisuals;

    private void Start()
    {
        stoveCounter.OnFryingStateChanged += StoveCounter_OnFryingStateChanged;
    }

    

    private void StoveCounter_OnFryingStateChanged(object sender, StoveCounter.OnFryingStateChangedEventArgs e)
    {
        if (e.fryingStateOfEvenetArgs == StoveCounter.FryingState.Frying || e.fryingStateOfEvenetArgs == StoveCounter.FryingState.Fried)
        {
            showVisuals = true;
        }
        else
        {
            showVisuals = false;
        }
    }
    private void Update()
    {
            foreach (var visual in fryingVisuals)
            {
                visual.SetActive(showVisuals);
            }
    }
}
