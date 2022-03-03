using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField] private GameObject[] bottomRooms;
    [SerializeField] private GameObject[] topRooms;
    [SerializeField] private GameObject[] rightRooms;
    [SerializeField] private GameObject[] leftRooms;

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
}
