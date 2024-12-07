using System;
using UnityEngine;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class MainPanel : TitlePanelBase
    {
        [Header("MainPanel Vars")]
        [SerializeField] private Button startBtn;
        [SerializeField] private Button settingBtn;

        public override void InitPanel(UIManager titleUIManager)
        {
            base.InitPanel(titleUIManager);
            startBtn.onClick.AddListener(LoadInGame);
            settingBtn.onClick.AddListener(OpenSetting);
        }

        private void LoadInGame()
        {
            StartCoroutine(SceneController.WaitAndLoadScene(uiManager.startSceneName, 0.1f));
        }

        private void OpenSetting()
        {
            base.ClosePanel();
            uiManager.ShowPanel(TitlePanelType.SettingPanel);
        }
    }
}