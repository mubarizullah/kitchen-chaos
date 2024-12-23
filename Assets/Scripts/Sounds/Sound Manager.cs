using UnityEngine;



public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundClipsSO soundClipsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        Player.Instance.OnObjectPickup += Player_OnObjectPickup;
        BaseCounter.OnObjectDroppedOnCounter += BaseCounter_OnObjectDroppedOnCounter;
        CuttinggCounter.OnAnyCut += CuttinggCounter_OnAnyCut;
        TrashBin.OnObjectTrashed += TrashBin_OnObjectTrashed;
    }

    private void TrashBin_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashBin trashBin = sender as TrashBin;
        PlaySound(soundClipsSO.trashArray[Random.Range(0, soundClipsSO.trashArray.Length)], trashBin.transform.position);
    }

    private void BaseCounter_OnObjectDroppedOnCounter(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundClipsSO.objectDropArray[Random.Range(0, soundClipsSO.objectDropArray.Length)], baseCounter.transform.position);
    }

    private void Player_OnObjectPickup(object sender, System.EventArgs e)
    {
        PlaySound(soundClipsSO.objectPickArray[Random.Range(0, soundClipsSO.objectPickArray.Length)], Player.Instance.transform.position);
    }

    private void CuttinggCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttinggCounter cuttinggCounter = sender as CuttinggCounter;
        PlaySound(soundClipsSO.cutSoundsArray[Random.Range(0, soundClipsSO.cutSoundsArray.Length)], cuttinggCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {

        PlaySound(soundClipsSO.deliveryFailArray[Random.Range(0, soundClipsSO.deliverySuccessArray.Length)], DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(soundClipsSO.deliverySuccessArray[Random.Range(0,soundClipsSO.deliverySuccessArray.Length)], DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip audio, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audio, position, volume);
    }
}
