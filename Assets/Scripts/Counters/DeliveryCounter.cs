using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.IsKitchenObjectPresent())
        {
            if (player.GetKitchenObjects().TryGetPlateOBject(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

                player.GetKitchenObjects().DestroyItself();
            }
        }
        
    }
}
