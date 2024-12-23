using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    [SerializeField] Player player;

    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent())     //counter already has kitchen object
            {
                player.GetKitchenObjects().SetKitchenObjectParent(this);
            }
            else         //both dont have kitchen object
            {
                
            }
        }
        else      //counter has kitchen object
        {
            if (!player.IsKitchenObjectPresent())    //player has nothing
            {
                GetKitchenObjects().SetKitchenObjectParent(player);
            }
            else    //both have kitchen object
            {
                //putting kitchen object over plate
                if (player.GetKitchenObjects().TryGetPlateOBject(out PlateKitchenObject plateKitchenObject))      // checking if the kitchen object the player is holding is of type PlateKitchenObject (and it is because it is inheriting form KitchenObjects script)
                {
                    //player is holding a plate and counter has other kitchen object
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObjects().GetKitchObjSO()))
                    {
                        GetKitchenObjects().DestroyItself();
                    }
                }
                else
                {
                    //if counter has plate and player has other kitchen Object
                    if (GetKitchenObjects().TryGetPlateOBject(out PlateKitchenObject plateKitchenObject1))
                    {
                        if(plateKitchenObject1.TryAddIngredients(player.GetKitchenObjects().GetKitchObjSO()))
                        {
                            player.GetKitchenObjects().DestroyItself();
                        }
                    }
                }    
            }
        }
    }

    
    


   
}









