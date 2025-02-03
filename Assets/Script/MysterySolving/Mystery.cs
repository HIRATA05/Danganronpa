using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class Mystery : MonoBehaviour
    {
        [Header("������f�[�^")]
        [SerializeField] protected MysteryData mysteryData;

        [Header("���̓������ID��I��")]
        [SerializeField] protected MysteryID mysteryID;

        [Header("������������f���J����")]
        [SerializeField] protected Camera mysteryCamera;

        [Header("�߂�{�^��")]
        [SerializeField] protected Button backToSubjectButton;
        [SerializeField] protected Button backToReticleModeButton;

        // ���̓�����̃f�[�^
        protected MysteryData.MysteryState mysteryState;
        protected GameManager gameManager;

        private void Awake()
        {
            GameObject gm = GameObject.Find("GameManager");
            gameManager = gm.GetComponent<GameManager>();

            foreach (var mystery in mysteryData.mysteryDatas)
            {
                mysteryState = mystery;
            }

            backToSubjectButton.onClick.AddListener(BackToSubuject);
            backToReticleModeButton.onClick.AddListener(BackToReticleMode);
        }

        public void StartMystery()
        {
            gameManager.playerController = GameManager.PlayerController.MysteryMode;
            SpotSubject();
        }

        protected virtual void SolvingMystery()
        {

        }

        private void EndMystery()
        {
            BackToReticleMode();
        }

        private void SuccessMystery()
        {

        }

        private void SpotSubject()
        {
            // ������Ώۂ��f��

        }
        private void SpotMystery()
        {
            // ������������f��

            // ��������J�n����
            SolvingMystery();
        }

        private void BackToSubuject()
        {
            SpotSubject();
        }

        private void BackToReticleMode()
        {
            gameManager.playerController = GameManager.PlayerController.ReticleMode;
        }
    }
}
