using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // TODO: buscar una forma de omitir el instantiate y destroy
    private RoomTemplates roomTemplates;
    //1 bottom, 2 top, 3 right, 4 left
    enum OpenSide
    {
        bottomDoor,
        topDoor,
        rightDoor,
        leftDoor
    }
    [SerializeField] private OpenSide openSide;

    private int rand;
    private bool spawned = false;


    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameManager.GetInstance.GetRoomTemplates;
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        if (spawned) return;

        switch (openSide)
        {
            case OpenSide.bottomDoor:
                rand = Random.Range(0, roomTemplates.TopRooms.Length);
                Instantiate(roomTemplates.TopRooms[rand], transform.position, transform.rotation);
                break;
            case OpenSide.topDoor:
                rand = Random.Range(0, roomTemplates.BottomRooms.Length);
                Instantiate(roomTemplates.BottomRooms[rand], transform.position, transform.rotation);
                break;
            case OpenSide.rightDoor:
                rand = Random.Range(0, roomTemplates.LeftRooms.Length);
                Instantiate(roomTemplates.LeftRooms[rand], transform.position, transform.rotation);
                break;
            case OpenSide.leftDoor:
                rand = Random.Range(0, roomTemplates.RightRooms.Length);
                Instantiate(roomTemplates.RightRooms[rand], transform.position, transform.rotation);
                break;
        }

        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
