using UnityEngine;
using System;
using System.Collections.Generic;

public class PlateCompleteVisuals : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;


    [Serializable]
    public struct KitchenObjectSO_GameObject     // making a struct to store kitchenObjectSO and the refrence of the gameobjects present inside the plate (e.g burger ingredients)
    {
        public KitchenObjectSO kitchenObjSO;
        public GameObject ingredientGameObject;
    }


    [SerializeField] List<KitchenObjectSO_GameObject> ingredientPlateStructList;   //...making list of the struct to show in the inspector...




    //....BODY OF THE CLASS....///

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;     
    }


    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddEventArgs e)    //...when ingredient is added....//
    {
        foreach(KitchenObjectSO_GameObject ingredient in ingredientPlateStructList)
        {
            if(ingredient.kitchenObjSO == e.kitchenObjectSO)
            {
                ingredient.ingredientGameObject.SetActive(true);
            }
        }
    }
}
