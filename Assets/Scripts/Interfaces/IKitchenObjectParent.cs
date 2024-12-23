using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjSpawnPoint();



    public void SetKitchenObject(KitchenObjects kitchenObjects);



    public KitchenObjects GetKitchenObjects();



    public bool IsKitchenObjectPresent();



    public void ClearKitchenObject();


}
