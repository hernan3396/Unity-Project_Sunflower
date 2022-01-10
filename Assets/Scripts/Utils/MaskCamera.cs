using UnityEngine;

public class MaskCamera : MonoBehaviour
{
    [SerializeField] Transform lightsCamera;

    // Update is called once per frame
    void Update()
    {
        transform.position = lightsCamera.position;
    }
}
