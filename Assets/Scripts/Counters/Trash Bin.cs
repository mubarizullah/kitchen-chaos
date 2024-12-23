using UnityEngine;
using System;

public class TrashBin : BaseCounter
{
    public static event EventHandler OnObjectTrashed;
    public override void Interact(Player player)
    {
        if (player.IsKitchenObjectPresent())
        {
            KitchenObjects kitchenObject = player.GetKitchenObjects();
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
            kitchenObject.DestroyItself();
        }
    }
}
