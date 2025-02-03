using System.Collections.Generic;
using UnityEngine;

namespace TECHC.Kamiyashiki
{
    public enum MysteryID
    {
        DuraluminCase,
    } 

    [CreateAssetMenu(fileName = "MyesteryData", menuName = "ScriptableObjects/MyesteryData")]
    public class MysteryData : ScriptableObject
    {
        [System.Serializable]
        public class MysteryState
        {
            public MysteryID mysteryID;
            public bool IsSolved;
            public int[] pass = new int[4];
        }

        public List<MysteryState> mysteryDatas = new List<MysteryState>();
    }
    
}