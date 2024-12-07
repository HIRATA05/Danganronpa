using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum RoomNames
{
    Entrance,
    LivingRoom,
    Kitchen
}

[System.Serializable]
public class Room
{
    public RoomNames roomName; // ïîâÆÇÃñºëO (enum å^Ç…ïœçX)
    public GameObject position; // ïîâÆÇÃç¿ïW
}

public class RoomController : MonoBehaviour
{
    public List<Room> roomList = new List<Room>();

    private Dictionary<RoomNames, Vector3> roomPositions = new Dictionary<RoomNames, Vector3>();

    private void Awake()
    {
        foreach (var room in roomList)
        {
            if (!roomPositions.ContainsKey(room.roomName))
            {
                roomPositions.Add(room.roomName, room.position.transform.position);
            }
        }
    }

    public IEnumerator WaitAndMoveRoom(RoomNames roomName, float waitTime, Transform target)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        MoveRoom(roomName, target);
    }

    public void MoveRoom(RoomNames roomName, Transform target)
    {
        if (roomPositions.TryGetValue(roomName, out Vector3 position))
        {
            target.position = position;
        }
        else
        {
            Debug.LogWarning($"Room '{roomName}' does not exist!");
        }
    }
}
