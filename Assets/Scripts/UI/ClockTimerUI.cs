using UnityEngine;
using UnityEngine.UI;

public class ClockTimerUI : MonoBehaviour
{
    [SerializeField] Image imageCounter;

    public void Update()
    {
        imageCounter.fillAmount = GameManager.Instance.GetNormalizedGamePlayTimer();
    }
}
