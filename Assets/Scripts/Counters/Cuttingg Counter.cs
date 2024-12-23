using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttinggCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;

    [SerializeField] CuttingRecipeSO[] cuttingObjectsSOs;

    [SerializeField] int cuttingProgress;



    public event EventHandler<IHasProgress.OnProgressBarChangedEventArgs> OnProgressBarUpdate;    // declaring event

    
    public event EventHandler OnCutVisuals;


    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent() && HasValidRecepie(player.GetKitchenObjects().GetKitchObjSO()))     //player has kitchen object, that can be cutted
            {
                player.GetKitchenObjects().SetKitchenObjectParent(this);

                cuttingProgress = 0;     // if player drops, kitchen object


                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObjects().GetKitchObjSO());

                OnProgressBarUpdate?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs   //firing event for progress Ui Update
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCutForCutting 
                });

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
                // putting kitchen object over plate
                if (player.GetKitchenObjects().TryGetPlateOBject(out PlateKitchenObject plateKitchenObject))      // checking if the kitchen object the player is holding is of type PlateKitchenObject (and it is because it is inheriting form KitchenObjects script)
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObjects().GetKitchObjSO()))
                    {
                        GetKitchenObjects().DestroyItself();
                    }
                }
            }
        }
    }  // this is the basic interaction to place kitchen objects on the counter


    public override void InteractAlternate(Player player)
    {
        if (IsKitchenObjectPresent() && HasValidRecepie(GetKitchenObjects().GetKitchObjSO()))    // if any kitchen object is present that can be cutted
        {
            cuttingProgress++;

            OnCutVisuals?.Invoke(this, EventArgs.Empty);

            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObjects().GetKitchObjSO());  //gets the recipe SO specific to the kitchen object we have

            OnProgressBarUpdate?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCutForCutting
            });   //firing event for progress Ui Update


            // underlying code will only execute when cutting progress is complete
            if (cuttingProgress >= cuttingRecipeSO.maxCutForCutting)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObjects().GetKitchObjSO());  //call the function we made to get the output for the recepie

                GetKitchenObjects().DestroyItself();  //destroy previous kitchenObject after if we destroy it first we dont have data for input of Recepie

                KitchenObjects.SpawnKitchenObjectOnParent(outputKitchenObjectSO, this);  //now spawn the output( SLICES )
            }
        }
    } // this interaction is used to cut kitchen objects present on cutting counter


    public KitchenObjectSO GetOutputForInput (KitchenObjectSO inputSO) 
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    } // this method return us the output from the recipe SO 


    private bool HasValidRecepie(KitchenObjectSO inputSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputSO);
        return cuttingRecipeSO != null;
    }  // this method return true if it has the recipe of the input SO


    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO kitchenObjectSOInput)
    {
        foreach (var recipe in cuttingObjectsSOs)
        {
            if (recipe.input == kitchenObjectSOInput)
            {
                return recipe;
            }
        }
        return null;
    }    // this method returns us the recipe SO from the kitchen Object Input it has
}
