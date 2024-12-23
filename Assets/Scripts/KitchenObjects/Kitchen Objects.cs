using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{

    //this script is a component of kitchen objects gameobject which will be responsible for having a track of which clear counter its sit on


    [SerializeField] KitchenObjectSO kitchenObjectSO;


    private IKitchenObjectParent kitchenObjectParent;

    public bool TryGetPlateOBject(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    
    public KitchenObjectSO GetKitchObjSO()
    {
        return kitchenObjectSO;
    }



    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        if (this.kitchenObjectParent != null)           // checks if any clear counter previsously attach, if attach
        {
            this.kitchenObjectParent.ClearKitchenObject();   //then remove the kitchen object refrence from that counter
        }

        this.kitchenObjectParent = kitchenObjectParent;         // and then assign clear counter to the newly given clear counter ref

        kitchenObjectParent.SetKitchenObject(this);     //now assigning this kitchenobject to the new clear counter

        transform.parent = kitchenObjectParent.GetKitchenObjSpawnPoint();  // making it child of selected Kitchen object Parent
        transform.localPosition = Vector3.zero;

        
    }



    public IKitchenObjectParent GetKItchenObjectParent()
    {
        return kitchenObjectParent;
    }


    public void DestroyItself()
    {
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }


    public static KitchenObjects SpawnKitchenObjectOnParent(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObjects kitchenObjects = kitchenObjectTransform.GetComponent<KitchenObjects>();

        kitchenObjects.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObjects;
    }


}
