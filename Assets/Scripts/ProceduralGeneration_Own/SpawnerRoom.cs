using UnityEngine;

public class SpawnerRoom : MonoBehaviour
{
    enum OpenDoor
    {
        bottomDoor,
        topDoor,
        rightDoor,
        leftDoor
    }

    [SerializeField] private OpenDoor[] openDoors;
    private RoomTemplates roomTemplates;
    private int rand;
    // use same order as OpenDoors array in inspector
    [SerializeField] private Vector3[] newRoomOff;

    private void Start()
    {
        roomTemplates = GameManager.GetInstance.GetRoomTemplates;

        SpawnRooms();
    }

    private void SpawnRooms()
    {
        foreach (OpenDoor door in openDoors)
        {
            switch (door)
            {
                case OpenDoor.topDoor:
                    // if door is top, needs a room with a bottom door
                    rand = Random.Range(0, roomTemplates.BottomRooms.Length);
                    Instantiate(roomTemplates.BottomRooms[rand], transform.position + newRoomOff[(int)OpenDoor.topDoor], transform.rotation);
                    break;
                case OpenDoor.bottomDoor:
                    // if door is bottom, needs a room with a top door
                    rand = Random.Range(0, roomTemplates.TopRooms.Length);
                    Instantiate(roomTemplates.TopRooms[rand], transform.position + newRoomOff[(int)OpenDoor.bottomDoor], transform.rotation);
                    break;
                case OpenDoor.rightDoor:
                    // if door is right, needs a room with a left door
                    rand = Random.Range(0, roomTemplates.LeftRooms.Length);
                    Instantiate(roomTemplates.LeftRooms[rand], transform.position + newRoomOff[(int)OpenDoor.rightDoor], transform.rotation);
                    break;
                case OpenDoor.leftDoor:
                    // if door is left, needs a room with a right door
                    rand = Random.Range(0, roomTemplates.RightRooms.Length);
                    Instantiate(roomTemplates.RightRooms[rand], transform.position + newRoomOff[(int)OpenDoor.leftDoor], transform.rotation);
                    break;
            }
        }
    }
}
