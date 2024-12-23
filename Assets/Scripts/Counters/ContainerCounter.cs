using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabKitchenObject;

    [SerializeField] KitchenObjectSO kitchenObjectSO;

    
    public override void Interact(Player player)
    {
        if (!player.IsKitchenObjectPresent())
        {
            KitchenObjects.SpawnKitchenObjectOnParent(kitchenObjectSO, player);

            OnPlayerGrabKitchenObject?.Invoke(this, EventArgs.Empty);      //event called for counter animation
        }
      
    }

}
