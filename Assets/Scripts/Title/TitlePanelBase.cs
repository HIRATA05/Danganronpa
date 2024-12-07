using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{

    public class TitlePanelBase : MonoBehaviour
    {
        [field: SerializeField, Header("TitlePanelBase Vars")]
        public TitlePanelType PanelType { get; private set; }

        [SerializeField] private Animator panelAnimator;
        protected UIManager uiManager;

        public enum TitlePanelType
        {
            None,
            MainPanel,
            SettingPanel,
        }

        public virtual void InitPanel(UIManager _uiManager)
        {
            uiManager = _uiManager;
        }

        public void ShowPanel()
        {
            this.gameObject.SetActive(true);
            const string POP_IN_CLIP_NAME = "In";
            panelAnimator.Play(POP_IN_CLIP_NAME);
        }
        protected void ClosePanel()
        {
            const string POP_OUT_CLIP_NAME = "Out";
            StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, panelAnimator, POP_OUT_CLIP_NAME, false));
        }
    }
}