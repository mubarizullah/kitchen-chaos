using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateKitchenObject : KitchenObjects
{
    private List<KitchenObjectSO> plateIngredientsList;
    [SerializeField] List<KitchenObjectSO> validKitchenObjectsSOForPlate;


    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdded;   //declaring an event for plate inredients added
    public class OnIngredientAddEventArgs : EventArgs      // making a class to store and ingredient's kitchenObjSO 
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake()
    {
        plateIngredientsList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredients(KitchenObjectSO kitchenObjs)      // this fucntion will check if the kitchenobject is valid and the kitchenobject is not added before 
    {
        if (!validKitchenObjectsSOForPlate.Contains(kitchenObjs))
        {
            return false;
        }
        if (plateIngredientsList.Contains(kitchenObjs))
        {
            return false;
        }
        plateIngredientsList.Add(kitchenObjs);     //if it is valid and not added before than we add the kitchenObj in the list of ingredients
        OnIngredientAdded?.Invoke(this, new OnIngredientAddEventArgs { kitchenObjectSO = kitchenObjs });
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList () //this function will return the list of the ingredients
    {
        return plateIngredientsList;    
    }
}
