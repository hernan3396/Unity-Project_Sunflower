using UnityEngine;

public class SpawnerRoom : MonoBehaviour
{
    #region Enum
    enum OpenDoor
    {
        bottomDoor,
        topDoor,
        rightDoor,
        leftDoor
    }
    #endregion

    #region Components
    private RoomTemplates roomTemplates;
    #endregion

    #region Doors
    [SerializeField] private bool[] doorsAvalible = new bool[4] { true, true, true, true }; // este valor es igual de largo que OpenDoor length
    [SerializeField] private OpenDoor[] openDoors;
    private DoorList doorList = new DoorList();
    #endregion

    #region Variables
    private int rand;
    #endregion


    private void Start()
    {
        roomTemplates = GameManager.GetInstance.GetRoomTemplates;

        doorList.doors = new Door[openDoors.Length];
        for (int i = 0; i < doorList.doors.Length; i++)
        {
            doorList.doors[i] = new Door();
            doorList.doors[i].openDoor = openDoors[i];
            doorList.doors[i].newRoomOff = roomTemplates.GetOffset[(int)doorList.doors[i].openDoor];
        }

        Invoke("SpawnRooms", 0.3f); // le pongo un poquito de delay para que sea mas visual pero no es necesario
    }

    private void SpawnRooms()
    // se podria hacer en una funcion sola en vez de este chorizo pero da igual
    {
        foreach (Door door in doorList.doors)
        {
            switch (door.openDoor)
            {
                case OpenDoor.bottomDoor:
                    if (doorsAvalible[(int)OpenDoor.bottomDoor])
                    {
                        // if you spawn a room to the bottom you need an open to the top on the new room
                        rand = Random.Range(0, roomTemplates.TopRooms.Length);

                        GameObject go = Instantiate(roomTemplates.TopRooms[rand], transform.position + door.newRoomOff, transform.rotation);
                        go.GetComponent<SpawnerRoom>().DisableAvability((int)OpenDoor.topDoor);
                    }
                    break;
                case OpenDoor.topDoor:
                    if (doorsAvalible[(int)OpenDoor.topDoor])
                    {
                        // if you spawn a room to the top you need an open to the down on the new room
                        rand = Random.Range(0, roomTemplates.BottomRooms.Length);

                        GameObject go = Instantiate(roomTemplates.BottomRooms[rand], transform.position + door.newRoomOff, transform.rotation);
                        go.GetComponent<SpawnerRoom>().DisableAvability((int)OpenDoor.bottomDoor);
                    }
                    break;
                case OpenDoor.rightDoor:
                    if (doorsAvalible[(int)OpenDoor.rightDoor])
                    {
                        // if you spawn a room to the right you need an open to the left on the new room
                        rand = Random.Range(0, roomTemplates.LeftRooms.Length);

                        GameObject go = Instantiate(roomTemplates.LeftRooms[rand], transform.position + door.newRoomOff, transform.rotation);
                        go.GetComponent<SpawnerRoom>().DisableAvability((int)OpenDoor.leftDoor);
                    }
                    break;
                case OpenDoor.leftDoor:
                    if (doorsAvalible[(int)OpenDoor.leftDoor])
                    {
                        // if you spawn a room to the left you need an open to the right on the new room
                        rand = Random.Range(0, roomTemplates.RightRooms.Length);

                        GameObject go = Instantiate(roomTemplates.RightRooms[rand], transform.position + door.newRoomOff, transform.rotation);
                        go.GetComponent<SpawnerRoom>().DisableAvability((int)OpenDoor.rightDoor);
                    }
                    break;
            }
        }
    }

    public void DisableAvability(int index)
    {
        doorsAvalible[index] = false;
    }

    #region Clases
    private class Door
    {
        public OpenDoor openDoor;
        public Vector3 newRoomOff;
    }

    private class DoorList
    {
        public Door[] doors;
    }
    #endregion
}
