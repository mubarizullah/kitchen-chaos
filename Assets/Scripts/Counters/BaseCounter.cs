using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnObjectDroppedOnCounter;
    private KitchenObjects kitchenObjects;
    [SerializeField] Transform spawnPointCounter;


    public virtual void Interact(Player player)
    {

    }

    public virtual void InteractAlternate(Player player)
    {

    }

    public Transform GetKitchenObjSpawnPoint()
    {
        return spawnPointCounter;
    }



    public void SetKitchenObject(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;

        if (kitchenObjects != null)
        {
            OnObjectDroppedOnCounter?.Invoke(this, EventArgs.Empty);
        }
    }



    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObjects;
    }



    public bool IsKitchenObjectPresent()
    {
        return kitchenObjects != null;
    }



    public void ClearKitchenObject()
    {
        kitchenObjects = null;
    }

}
