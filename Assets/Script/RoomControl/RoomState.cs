using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "ScriptableObjects/RoomData")]
public class RoomData : ScriptableObject
{
    [System.Serializable]
    public class RoomState
    {
        public RoomName roomName;
        public bool isLocked = true;
    }

    public RoomName currentRoom;
    public List<RoomState> roomStates;
}
