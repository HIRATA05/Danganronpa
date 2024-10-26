using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] Button titleBtn;
    private void Start()
    {
        titleBtn.onClick.AddListener(LoadTitle);
    }

    private void LoadTitle()
    {
        StartCoroutine(SceneController.WaitAndLoadScene(SceneName.Title, 0.1f));
    }
}
