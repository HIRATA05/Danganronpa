using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class DuraluminMystery : Mystery
    {
        [Header("�싞���̊e���b�N(������)")]
        [SerializeField] private GameObject[] locks;

        [Header("�싞���̊e���b�N�𓮂����{�^��")]
        [SerializeField] private Button[] lockButtons;

        private float rotAngle = 36;

        private void Start()
        {
            // ���b�N�㉺�{�^���Ɋ֐������蓖��
            int length = lockButtons.Length;
            int half = lockButtons.Length / 2;
            for (int i = 0; i < length; i++)
            {
                int index = i; // ���[�J���ϐ��ɃR�s�[
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

        protected override void SolvingMystery()
        {
            base.SolvingMystery();

            ResetLock();
        }

        public void TurnLock(int lockNum, bool isUp)
        {
            float addRot = isUp ? rotAngle : -rotAngle;
            locks[lockNum].transform.Rotate(addRot, 0, 0);
            currentPIN[lockNum] = (currentPIN[lockNum] + (isUp ? 1 : -1) + 10) % 10; // 0�̎���:-1+10=9�A9%10=9;9�̎���:1+10=11�A11%10=1
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
