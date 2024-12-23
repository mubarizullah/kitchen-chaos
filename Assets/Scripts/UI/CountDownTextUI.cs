using UnityEngine;
using TMPro;

public class CountDownTextUI : MonoBehaviour
{
    TextMeshProUGUI countDownText;

    private void Start()
    {
        countDownText = GetComponent<TextMeshProUGUI>();
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownTimerActive())
        {
            Show();
        }
        else
        {
            Hide();
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

    private void Update()
    {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDown()).ToString();  //this approach show number without decimal which are greater than 0


        //countDownText.text = (GameManager.Instance.countDownTimer.ToString("0"));     this appraoch will show number without any decimal point, there are multiple ways too show decimal point
    }

}
