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
        [SerializeField] protected Camera subjectCamera;
        [SerializeField] protected Camera solvingCamera;
        [SerializeField] protected float subjectFOV = 60f;
        [SerializeField] protected float solvingFOV = 2f;

        [Header("UI")]
        [SerializeField] protected Button backToSubjectButton;
        [SerializeField] protected Button backToReticleModeButton;
        [SerializeField] protected Button mysteryStartButton;
        [SerializeField] protected GameObject alwaysPanel;
        [SerializeField] protected GameObject solvingPanel;

        [Header("謎解き成功時隠す、表示するオブジェクト")]
        [SerializeField] private GameObject[] hideObjs;
        [SerializeField] private GameObject[] showObjs;

        // この謎解きのデータ
        protected MysteryData.MysteryState mysteryState;
        protected GameManager gameManager;

        protected int[] currentPIN;

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
            mysteryStartButton.onClick.AddListener(SpotMystery);

            currentPIN = new int[mysteryState.PIN.Length];
            for (int i = 0; i < currentPIN.Length; i++)
            {
                currentPIN[i] = 0;
            }

            mysteryStartButton.gameObject.SetActive(false);

            subjectCamera.depth = -1;
            solvingCamera.depth = -1;
            subjectCamera.fieldOfView = subjectFOV;
            solvingCamera.fieldOfView = solvingFOV;
            alwaysPanel.gameObject.SetActive(false);
            solvingPanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (gameManager.playerController == GameManager.PlayerController.MysteryMode)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {

                }
            }
        }

        public void StartMystery()
        {
            gameManager.playerController = GameManager.PlayerController.MysteryMode;
            SpotSubject();
        }

        protected virtual void SolvingMystery()
        {

        }

        protected void EndMystery()
        {
            BackToReticleMode();
        }

        protected virtual void SuccessMystery()
        {
            mysteryState.IsSolved = true;

            foreach (var obj in hideObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in showObjs)
            {
                obj.SetActive(true);
            }

            Debug.Log("SUCCESS!");
            BackToReticleMode();
        }

        protected void SpotSubject()
        {
            // 謎解き対象を映す
            subjectCamera.depth = 1;
            alwaysPanel.gameObject.SetActive(true);
            solvingPanel.gameObject.SetActive(false);

            backToSubjectButton.gameObject.SetActive(false);
            backToReticleModeButton.gameObject.SetActive(true);
            mysteryStartButton.gameObject.SetActive(true);
        }
        protected void SpotMystery()
        {
            // 謎解き部分を映す
            solvingCamera.depth = 1;
            solvingPanel.gameObject.SetActive(true);

            backToSubjectButton.gameObject.SetActive(true);
            backToReticleModeButton.gameObject.SetActive(false);
            mysteryStartButton.gameObject.SetActive(false);

            // 謎解きを開始する
            SolvingMystery();
        }

        protected void BackToSubuject()
        {
            SpotSubject();
        }

        protected void BackToReticleMode()
        {
            subjectCamera.depth = -1;
            solvingCamera.depth = -1;
            alwaysPanel.gameObject.SetActive(false);
            solvingPanel.gameObject.SetActive(false);
            gameManager.playerController = GameManager.PlayerController.ReticleMode;
        }
    }
}
