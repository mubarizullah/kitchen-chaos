using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{

/// <summary>
/// THIS SCRIPT WILL SET ON CANVAS OF PLATE
/// </summary>


    [SerializeField] PlateKitchenObject plateKitchenObj;
    [SerializeField] Transform iconTemplate;   //...ref to the whole icon of ingredient template...//



    //...BODY OF CLASS....//
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);    //...on awake the icon template itself should be disable, it is just their to make copy of it through instantiating and dublicating its icon
    }


    private void Start()
    {
        plateKitchenObj.OnIngredientAdded += PlateKitchenObj_OnIngredientAdded;
    }


    private void PlateKitchenObj_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddEventArgs e)
    {
        UpdateIconVisual();
    }


    void UpdateIconVisual()
    {
        foreach (Transform child in transform)   //...every other child of this gameobject instead of the icon template itself should be destoyed
        {
            if (child == iconTemplate) continue;
            { Destroy(child.gameObject); }
        }

        foreach (var kitchenObjectSO in plateKitchenObj.GetKitchenObjectSOList())  //...checking every ingredient in the list and instantiating icon template under canvas after that we will
                                                                                   // enable that icontemplate and will send message to icontransform with the kitchenobject
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSOSprite(kitchenObjectSO);
        }
    }
}
