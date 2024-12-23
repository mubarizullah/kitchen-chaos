using UnityEngine;
using System;


public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressBarChangedEventArgs> OnProgressBarUpdate;    // declaring event
    public event EventHandler<OnFryingStateChangedEventArgs> OnFryingStateChanged;
    public class OnFryingStateChangedEventArgs: EventArgs
    {
        public FryingState fryingStateOfEvenetArgs;
    }

    [SerializeField] MeatCookRecipeSO[] meatCookRecipeSOs;
    [SerializeField] BurningRecipeSO[] burningRecipeSOs;

    MeatCookRecipeSO meatCookingRecipeSO;
    BurningRecipeSO burningRecipeSO;
    float fryingTimer;
    float burningTime;

    public enum FryingState
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public FryingState fryingState;





    /// <summary>
               ///BODY STATRS HERE
    /// </summary>
    private void Start()
    {
        fryingState = FryingState.Idle;
    }

    private void Update()
    {
        if (IsKitchenObjectPresent())
        {


            switch (fryingState)
            {
                case FryingState.Idle:       //idle state logic
                    fryingTimer = 0f;
                    burningTime = 0f;
                    break;

                case FryingState.Frying:   //frying state logic

                        fryingTimer += Time.deltaTime;
                    OnProgressBarUpdate?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs { progressNormalized = fryingTimer / meatCookingRecipeSO.maxCutForCutting });

                    if (fryingTimer > meatCookingRecipeSO.maxCutForCutting)
                        {
                            GetKitchenObjects().DestroyItself();
                            //Fried

                            KitchenObjects.SpawnKitchenObjectOnParent(meatCookingRecipeSO.output, this);
   
                            fryingState = FryingState.Fried;
                        }
                    break;


                case FryingState.Fried:    //fried state logic

                    burningRecipeSO = GetBurningRecipeWithInput(GetKitchenObjects().GetKitchObjSO());
                    burningTime += Time.deltaTime;

                    OnProgressBarUpdate?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs { progressNormalized = burningTime / burningRecipeSO.maxTimeForBurning });

                    if (burningTime > burningRecipeSO.maxTimeForBurning)
                    {

                        GetKitchenObjects().DestroyItself();
                        //Fried

                        KitchenObjects.SpawnKitchenObjectOnParent(burningRecipeSO.output, this);

                        fryingState = FryingState.Burned;
                    }
                    break;


                case FryingState.Burned:   //burned state
                    
                    break;
            }

        }
        OnFryingStateChanged?.Invoke(this, new OnFryingStateChangedEventArgs { fryingStateOfEvenetArgs = fryingState });
    }


    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent() && HasValidRecepie(player.GetKitchenObjects().GetKitchObjSO()))     //player has kitchen object, that can be cutted
            {
                fryingState = FryingState.Frying;
                player.GetKitchenObjects().SetKitchenObjectParent(this);

                meatCookingRecipeSO = GetMeatRecipeWithInput(GetKitchenObjects().GetKitchObjSO());  //when interact we get the recipe

                fryingTimer = 0f;
                fryingState = FryingState.Frying;
                OnProgressBarUpdate?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs { progressNormalized = fryingTimer / meatCookingRecipeSO.maxCutForCutting});
                
            }
            else         //both dont have kitchen object
            {
                //CAN DO SWAP HERE
            }
        }
        else      //counter has kitchen object
        {
            if (!player.IsKitchenObjectPresent())    //player has nothing
            {
                GetKitchenObjects().SetKitchenObjectParent(player);
                burningTime = 0f;
                fryingState = FryingState.Idle;
            }
            else    //both have kitchen object
            {
                //putting kitchen object over plate
                if (player.GetKitchenObjects().TryGetPlateOBject(out PlateKitchenObject plateKitchenObject))      // checking if the kitchen object the player is holding is of type PlateKitchenObject (and it is because it is inheriting form KitchenObjects script)
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObjects().GetKitchObjSO()))
                    {
                        GetKitchenObjects().DestroyItself();
                        fryingTimer = 0f;
                        burningTime = 0f;
                        fryingState = FryingState.Idle;
                    }
                }
            }
            
        }

    }


    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputSO)
    {
        MeatCookRecipeSO meatRecipeSO = GetMeatRecipeWithInput(inputSO);
        if (meatRecipeSO != null)
        {
            return meatRecipeSO.output;
        }
        else
        {
            return null;
        }
    } // this method return us the output from the recipe SO 


    private bool HasValidRecepie(KitchenObjectSO inputSO)
    {
        MeatCookRecipeSO meatRecipeSO = GetMeatRecipeWithInput(inputSO);
        return meatRecipeSO != null;
    }  // this method return true if it has the recipe of the input SO


    private MeatCookRecipeSO GetMeatRecipeWithInput(KitchenObjectSO kitchenObjectSOInput)
    {
        foreach (var recipe in meatCookRecipeSOs)
        {
            if (recipe.input == kitchenObjectSOInput)
            {
                return recipe;
            }
        }
        return null;
    }    // this method returns us the recipe SO from the kitchen Object Input it has


    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO kitchenObjectSOInput)
    {
        foreach (var recipes in burningRecipeSOs)
        {
            if (recipes.input == kitchenObjectSOInput)
            {
                return recipes;
            }
        }
        return null;
    }






}


