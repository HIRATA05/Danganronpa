using UnityEngine;

namespace TECHC.Kamiyashiki
{

    [CreateAssetMenu(fileName = "RoomData", menuName = "RoomData")]
    public class MonsterData : ScriptableObject
    {
        public string roomNameJapanese;
        public SceneName sceneName;
        public Canvas inGameCanvas, mapCanvas;
    }

}