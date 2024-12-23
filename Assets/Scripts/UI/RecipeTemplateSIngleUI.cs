using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeTemplateSIngleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeTemplateText;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeText(RecipeSO recipeSO)
    {
        recipeTemplateText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            {
                Destroy(child.gameObject);
            }
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.ingredientsOfRecipe)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.icon;
         
        }
    }
}
