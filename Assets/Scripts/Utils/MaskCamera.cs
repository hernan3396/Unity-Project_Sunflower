using UnityEngine;

public class MaskCamera : MonoBehaviour
{
    [SerializeField] Transform lightsCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        // Late update works best for this
        // avoids weird object movement perspective (?) dont know what to call it
        // but it makes sense
        // change to update and observe the box on game camera and you'll see
        // transform.position = lightsCamera.position;

        transform.position = new Vector3(lightsCamera.position.x, lightsCamera.position.y, transform.position.z);
    }
}
