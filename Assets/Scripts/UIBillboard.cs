using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    Quaternion originalRotation;
    
    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation * originalRotation;   
    }
}
