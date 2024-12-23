using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI recipeCountText;

    private void Start()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            Show();
            recipeCountText.text = DeliveryManager.Instance.GetRecipeDeliveredCount().ToString();
        }
        else
        {
            Hide();
        }
    }




    void Show()
    {
        gameOver.SetActive(true);
    }

    void Hide()
    {
        gameOver.SetActive(false);
    }


}
