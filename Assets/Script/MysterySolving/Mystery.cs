using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class Mystery : MonoBehaviour
    {
        [Header("謎解きデータ")]
        [SerializeField] protected MysteryData mysteryData;

        [Header("この謎解きのIDを選択")]
        [SerializeField] protected MysteryID mysteryID;

        [Header("謎解き部分を映すカメラ")]
        [SerializeField] protected Camera mysteryCamera;

        [Header("戻るボタン")]
        [SerializeField] protected Button backToSubjectButton;
        [SerializeField] protected Button backToReticleModeButton;

        // この謎解きのデータ
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
            // 謎解き対象を映す

        }
        private void SpotMystery()
        {
            // 謎解き部分を映す

            // 謎解きを開始する
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
