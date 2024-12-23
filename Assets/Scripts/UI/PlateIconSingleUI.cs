using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] Image iconImage;

    public void SetKitchenObjectSOSprite(KitchenObjectSO kitchenObjectSO)  //the function is called in updateVisual of PlateIconUI script and it
                                                                           //sends the kitchenobjectso so we can  assign the icon field of it to the current sprite of this gameobject's image componenet
    {
        iconImage.sprite = kitchenObjectSO.icon;
    }
}
