using UnityEngine;
using System.Collections.Generic;
using System;


public class DeliveryManager : MonoBehaviour
{
    [SerializeField] RecipeListSO recipeListSO;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;




    private List<RecipeSO> waitingRecipeSOList;
    [SerializeField] float recipeTimerMax = 8f;
    
    float recipeTimer;
    int waitngRecipeMaxCount = 4;

    int successfulDeliveries;

    public static DeliveryManager Instance { get; private set; }



    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        recipeTimer += Time.deltaTime;   //incrementing the time

        if (recipeTimer >= recipeTimerMax)     // check if our timer is greater than or equal to 4 then we do some logic
        {
            recipeTimer = 0f;   //first of all resetting our timer so it can do logic again after 4 seconds

            if (waitingRecipeSOList.Count < waitngRecipeMaxCount)      // checking if our self made list is not full (if there are less than 4 recipe add more recipe)
            {
                RecipeSO currentWaitingRecipeSO = recipeListSO.waitingRecipeList[UnityEngine.Random.Range(0, recipeListSO.waitingRecipeList.Count)];   // make a field of type RecipeSO and assign it as a random recipe
                waitingRecipeSOList.Add(currentWaitingRecipeSO);   // add that Recipe to our waitingList
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }



    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO currentWaitingRecipeSO = waitingRecipeSOList[i];      //checking current recipe in the waitingRecipeSOlist

            if (currentWaitingRecipeSO.ingredientsOfRecipe.Count == plateKitchenObject.GetKitchenObjectSOList().Count)   // checking if the number of ingredient in the first recipe in waingRecipeList matches with the number of ingredients present in the plateKitchenObject
            {
                // if this if is true, mean both plate and the current recipe has same number of ingredients

                bool plateContentsMatchesRecipe = false;

                // now check each ingredients in current waitingRecipeFirst
                foreach (KitchenObjectSO ingredientSO in currentWaitingRecipeSO.ingredientsOfRecipe)
                {
                    bool ingredientFound = false;

                    // now check each ingredients in current plate
                    foreach (KitchenObjectSO ingredientInPlateSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (ingredientSO == ingredientInPlateSO)
                        {
                            //ingredients matches
                            ingredientFound = true;
                            plateContentsMatchesRecipe = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //the recipe ingredient was not found in the plate
                        plateContentsMatchesRecipe = false;
                    }

                }

                if (plateContentsMatchesRecipe)
                {
                    //PLAYER DELIVERED THE CORRECT RECIPE
                    //the recipe ingredients was found in the recipe
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    Debug.Log("Player delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    recipeTimer = 0f;
                    successfulDeliveries++;
                    return;
                }
            }

        }

        //no matches found
        Debug.Log("player did not deliver correct recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }



    public List<RecipeSO> GetWaititngRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetRecipeDeliveredCount()
    {   
        return successfulDeliveries;
    }
}
