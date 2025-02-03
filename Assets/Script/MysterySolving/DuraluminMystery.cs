using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class DuraluminMystery : Mystery
    {
        [Header("南京錠の各ロック(左から)")]
        [SerializeField] private GameObject[] locks;

        [SerializeField] private Button[] lockButtons;

        private float rotAngle = 36;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.playerController = GameManager.PlayerController.MysteryMode;
                SolvingMystery();
            }
        }

        protected override void SolvingMystery()
        {
            base.SolvingMystery();

            int length = lockButtons.Length;
            int half = lockButtons.Length / 2;
            for (int i = 0; i < length; i++)
            {
                int index = i; // ローカル変数にコピー
                if (index < half)
                {
                    lockButtons[index].onClick.AddListener(() => TurnLock(index, true));
                    Debug.Log("TurnLock(" + index + ", " + "True)");
                }
                else
                {
                    int adjustedIndex = index % half;
                    lockButtons[index].onClick.AddListener(() => TurnLock(adjustedIndex, false));
                    Debug.Log("TurnLock(" + adjustedIndex + ", " + "False)");
                }
            }
        }

        public void TurnLock(int lockNum, bool isUp)
        {
            float addRot = isUp ? rotAngle : -rotAngle;
            locks[lockNum].transform.Rotate(addRot, 0, 0);
        }

        public void ResetLock()
        {
            foreach (var _lock in locks)
            {
                _lock.transform.Rotate(0, 0, 0);
            }
        }
    }
}
