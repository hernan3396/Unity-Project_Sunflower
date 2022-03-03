using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField] private GameObject[] bottomRooms;
    [SerializeField] private GameObject[] topRooms;
    [SerializeField] private GameObject[] rightRooms;
    [SerializeField] private GameObject[] leftRooms;

    // keep order as above lists
    [SerializeField] private Vector3[] newRoomOff = new Vector3[4] { new Vector3(0, -10, 0), new Vector3(0, 10, 0), new Vector3(10, 0, 0), new Vector3(-10, 0, 0) };

    public GameObject[] BottomRooms
    {
        get { return bottomRooms; }
    }
    public GameObject[] TopRooms
    {
        get { return topRooms; }
    }
    public GameObject[] RightRooms
    {
        get { return rightRooms; }
    }
    public GameObject[] LeftRooms
    {
        get { return leftRooms; }
    }

    public Vector3[] GetOffset
    {
        get { return newRoomOff; }
    }
}
