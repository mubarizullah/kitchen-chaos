using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{


  public event EventHandler OnObjectPickup;

  public static Player Instance{get; private set;}       // using singleton patterm making proprty of Player class in which every class can get its fields but no one can set

  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

  public class OnSelectedCounterChangedEventArgs : EventArgs 
  {
    public BaseCounter selectedCounter;   //this variable will be checked with the clearCounter variable present in SelectedCounter class
  }

  




  
  private BaseCounter selectedCounter;

  float bodyHeight = 2f;

  float bodyRadius = 0.7f;

  Vector3 lastInteractibleDir;

  [SerializeField] float playerSpeed;

  [SerializeField] GameInput gameInput;

  [SerializeField] LayerMask layerMaskForCounters;

  [SerializeField] Transform kitchenObjectHoldPoint;

  private KitchenObjects kitchenObject;










  void Awake()
  {
    if (Instance != null)
    {
      Debug.LogError("more than one instance of player");
    }
    Instance = this;
  }


  void Start()
  {
    gameInput.OnInteract += GameInput_OnInteract;
    gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;
  }


  private void Update()
   {    
    HandleInteraction();
   }

    private void FixedUpdate()
    {
        HandleMovement();
    }


    void GameInput_OnInteract(object sender, EventArgs eventArgs)    //as the delegate was of type EventArgs so we have to make two paremeter inside that method
   {
     if (!GameManager.Instance.IsGamePlaying()) return;  // if the state of game is NOT game Playing do not execute further code

        if (selectedCounter != null)
      {
         selectedCounter.Interact(this);
      }
   }


    void GameInput_OnInteractAlternate(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;    // if the state of game is NOT game Playing do not execute further code

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void HandleInteraction()
   {
    Vector2 inputVector2FromGameInput = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector2FromGameInput.x, 0, inputVector2FromGameInput.y);

    if (movDir != Vector3.zero)    //we check if the movement input is not zero and has a value we immediately store it in a new varialble called lastInteractDir
    {
      lastInteractibleDir = movDir;  //reason: when we stop movDir becomes zero so even if we are near any object we are not throwing raycast in any direction
    }
   
    float playerInteractionDist = 1f;
    if (Physics.Raycast(transform.position, lastInteractibleDir, out RaycastHit hitInfo, playerInteractionDist))
      {
            if(hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))   
            {
              
                  if (baseCounter != selectedCounter)
                  {
                    selectedCounter = baseCounter;
                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
                  }
            }
            else
            {
              selectedCounter = null;
              OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
            }
      }

    else
          {
             selectedCounter = null;
             OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
          }
    
   }


  private void HandleMovement()
   {
    Vector2 inputVector2FromGameInput = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector2FromGameInput.x, 0, inputVector2FromGameInput.y);
    Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
    Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
    
    Vector3 endPoint = transform.position + Vector3.up * bodyHeight;                            //position of this gameobject is its base while we move up from base 2 units to reach its height
    bool canMove = !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDir, playerSpeed * Time.fixedDeltaTime);
    
    if (!canMove)       //mean we have hit something lets check if we can move on x axis for this we will direction dedicated for x-axis
    {
      canMove = movDir.x != 0 && !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDirX, playerSpeed* Time.fixedDeltaTime, layerMaskForCounters);

      if (canMove)
      {
        movDir = movDirX;
      }
      else
      {
        canMove = movDir.z != 0 && !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDirZ, playerSpeed * Time.deltaTime, layerMaskForCounters);
        if (canMove)
        {
          movDir = movDirZ;
        }
      }


    }


    if (canMove)
    {
      transform.position += movDir * playerSpeed * Time.fixedDeltaTime;
    }
    
    float playerRotation = 10f;
    transform.forward = Vector3.Slerp(transform.forward, movDir , Time.fixedDeltaTime * playerRotation);       //this line is for the rotation of the body when its moving and rotating it on the same direction where moving
   }



  public Transform GetKitchenObjSpawnPoint()
    {
        return kitchenObjectHoldPoint;
    }  //will return us the hold point of kitchen object of player



  public void SetKitchenObject(KitchenObjects kitchenObjects)
    {
        this.kitchenObject = kitchenObjects;

        if (kitchenObject != null)
        {
            OnObjectPickup?.Invoke(this, EventArgs.Empty);
        }
    }     //update the kitchen object of player



  public KitchenObjects GetKitchenObjects()
    {
        return kitchenObject;
    }    //will return us the kitchen object present on player



  public bool IsKitchenObjectPresent()
    {
        return kitchenObject != null;
    }    //checks if kitchen object is present or not



  public void ClearKitchenObject()     //set kichen object to null means remove it
    {
        kitchenObject = null;
    }
}
