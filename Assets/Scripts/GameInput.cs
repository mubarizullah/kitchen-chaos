using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    PlayerInputActionClass playerInputActionClass;


    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;

    void Awake()
    {
        playerInputActionClass = new PlayerInputActionClass();
        playerInputActionClass.Player.Enable();
        playerInputActionClass.Player.Interact.performed += PlayerInteractions;
        playerInputActionClass.Player.InteractAlternate.performed += PlayerInteractAlternate;
    }


    public Vector2 GetMovementVector2Normalized()
    {
    Vector2 inputVector = playerInputActionClass.Player.Move.ReadValue<Vector2>();
    inputVector = inputVector.normalized;
    return inputVector;
    }



    public void PlayerInteractions(InputAction.CallbackContext context)
    {
     OnInteract?.Invoke(this, EventArgs.Empty);
    }


    public void PlayerInteractAlternate(InputAction.CallbackContext contextt)
    {
     OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }    
}
