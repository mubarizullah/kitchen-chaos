using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] selectedVisualArray;

       void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
    {
        if (eventArgs.selectedCounter == baseCounter)
        {
            Show();
        }
        else 
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visual in selectedVisualArray)
        {
            visual.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visual in selectedVisualArray)
        {
            visual.SetActive(false);
        }
    }
    
}
