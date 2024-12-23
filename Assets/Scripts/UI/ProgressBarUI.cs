using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] GameObject hasProgressGameobject;
    IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameobject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError(hasProgress + "does not have i interface");
        }
        hasProgress.OnProgressBarUpdate += HasProgress_OnProgressBarUpdate;
        barImage.fillAmount = 0f;
        Hide();
    }
    
    private void HasProgress_OnProgressBarUpdate(object sender, IHasProgress.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0 || e.progressNormalized == 1  )
        {
            Hide();
        }
        else
        {
            Show();
        }
    }


    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
