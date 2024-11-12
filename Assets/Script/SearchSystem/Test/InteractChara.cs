using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractChara : MonoBehaviour, IReceiveSearch
{

    //調べた時のテキスト情報をテキスト管理スクリプトに渡す

    [SerializeField] private DialogueText dialogueText;

    private GameManager gameManager;
    private CinemachineVirtualCamera[] virtualCameras;



    public void ReceiveSearch()
    {
        
        Debug.Log("キャラを調べた");
        //場合によってはフラグによって条件分岐

        gameManager.OpenTextWindow(dialogueText, virtualCameras);

    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        virtualCameras = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    void Update()
    {
        
    }
}
