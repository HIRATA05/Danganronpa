using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class DuraluminMystery : Mystery
    {
        // TODO:謎解き時、後ろのオブジェクト干渉しないように
        // TODO:ノートPC入手のフラグ

        [Header("南京錠の各ロック(左から)")]
        [SerializeField] private GameObject[] locks;

        [Header("南京錠の各ロックを動かすボタン")]
        [SerializeField] private Button[] lockButtons;

        private float rotAngle = 36;

        protected override void SolvingMystery()
        {
            base.SolvingMystery();

            ResetLock();

            int length = lockButtons.Length;
            int half = lockButtons.Length / 2;
            for (int i = 0; i < length; i++)
            {
                int index = i; // ローカル変数にコピー
                if (index < half)
                {
                    lockButtons[index].onClick.AddListener(() => TurnLock(index, true));
                }
                else
                {
                    int adjustedIndex = index % half;
                    lockButtons[index].onClick.AddListener(() => TurnLock(adjustedIndex, false));
                }
            }
        }

        public void TurnLock(int lockNum, bool isUp)
        {
            float addRot = isUp ? rotAngle : -rotAngle;
            locks[lockNum].transform.Rotate(addRot, 0, 0);
            currentPIN[lockNum] = (currentPIN[lockNum] + (isUp ? 1 : -1) + 10) % 10; // 0の時下:-1+10=9、9%10=9;9の時上:1+10=11、11%10=1
            CheckLock();
        }

        public void ResetLock()
        {
            for (int i = 0; i < currentPIN.Length; i++)
            {
                locks[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                currentPIN[i] = 0;
            }
        }

        public void CheckLock()
        {
            for (int i = 0; i < currentPIN.Length; i++)
            {
                if (currentPIN[i] != mysteryState.PIN[i])
                {
                    return;
                }
            }
            SuccessMystery();
        }
    }
}
