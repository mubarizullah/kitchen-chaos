using UnityEngine;
using System.Collections.Generic;


public class PlateCounterVisuals : MonoBehaviour
{
    
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform plateVisuals;
    [SerializeField] Transform counterTopPoint;
    private List<GameObject> plateSpawnedList;
    public bool isPlatePresent;

    private void Awake()
    {
        plateSpawnedList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObj = plateSpawnedList[plateSpawnedList.Count - 1];
        plateSpawnedList.Remove(plateGameObj);
        Destroy(plateGameObj);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisual = Instantiate(plateVisuals, counterTopPoint);
        float plateOffSet = .1f;
        plateVisual.localPosition = new Vector3(0, plateOffSet * plateSpawnedList.Count);

        plateSpawnedList.Add(plateVisual.gameObject);
    }
}
