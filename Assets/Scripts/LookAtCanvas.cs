using UnityEngine;

public class LookAtCanvas : MonoBehaviour
{
    
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
