using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO plateObjectSO;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    float plateSpawnTime;
    float plateSpawnTimeMax = 4f;
    int plateCount;
    int plateCountMax = 4;
    [SerializeField] bool isPlatePresent;

    private void Update()
    {
        plateSpawnTime += Time.deltaTime;
        if (plateSpawnTime > plateSpawnTimeMax)
        {
            plateSpawnTime = 0;
            if (plateCount < plateCountMax)
            {
                plateCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
        if (plateCount == 0)
        {
            isPlatePresent = false;
        }
        else
        {
            isPlatePresent = true;
        }
    }

    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent() && isPlatePresent && !player.IsKitchenObjectPresent())  //counter has nothing 
        {
            KitchenObjects.SpawnKitchenObjectOnParent(plateObjectSO, player);
            plateCount--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
        else      //counter has kitchen object
        {
            
        }
    }
}
