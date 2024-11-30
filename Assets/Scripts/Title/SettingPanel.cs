using System;
using UnityEngine;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    public class SettingPanel : TitlePanelBase
    {
        [Header("SettingPanel Vars")]
        [SerializeField] private Button closeBtn;

        public override void InitPanel(UIManager titleUIManager)
        {
            base.InitPanel(titleUIManager);
            closeBtn.onClick.AddListener(CloseSetting);
        }

        private void CloseSetting()
        {
            base.ClosePanel();
            uiManager.ShowPanel(TitlePanelType.MainPanel);
        }
    }
}