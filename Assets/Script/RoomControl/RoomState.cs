using System.Collections.Generic;
using UnityEngine;

namespace TECHC.Kamiyashiki
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "ScriptableObjects/RoomData")]
    public class RoomData : ScriptableObject
    {
        [System.Serializable]
        public class RoomState
        {
            public RoomName roomName;
            public bool isLocked = true;
        }

        public List<RoomState> roomStates;
    }
}