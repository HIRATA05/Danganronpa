using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChara : MonoBehaviour, IReceiveSearch
{

    //調べた時のテキスト情報をテキスト管理スクリプトに渡す

    [SerializeField] private DialogueText dialogueText;

    GameManager gameManager;
    TextWindow textWindow;


    public void ReceiveSearch()
    {
        
        Debug.Log("Enemy は 1ダメージ食らった");
        //場合によってはフラグによって条件分岐

        //渡す
        textWindow.dialogueText = dialogueText;

        gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();

    }

    void Update()
    {
        
    }
}
