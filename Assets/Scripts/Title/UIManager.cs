using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TitlePanelBase[] titlePanels;

    void Start()
    {
        foreach(var panel in titlePanels)
        {
            panel.InitPanel(this);
        }
    }
    public void ShowPanel(TitlePanelBase.TitlePanelType type)
    {
        foreach(var panel in titlePanels)
        {
            if(panel.PanelType == type)
            {
                panel.ShowPanel();
                break;
            }
        }
    }
}
