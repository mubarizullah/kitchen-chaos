using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform recipeTemplate;
    [SerializeField] Transform container;
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false); ;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        UpdateVisuals();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var recipe in DeliveryManager.Instance.GetWaititngRecipeSOList())
        {
            Transform templateUI = Instantiate(recipeTemplate, container);
            templateUI.gameObject.SetActive(true);
            templateUI.GetComponent<RecipeTemplateSIngleUI>().SetRecipeText(recipe);

        }
    }

    private void Update()
    {
        UpdateVisuals();
    }
}
