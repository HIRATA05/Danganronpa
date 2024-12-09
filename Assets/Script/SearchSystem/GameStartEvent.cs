using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class GameStartEvent : MonoBehaviour
{
    //ゲーム開始時の会話発生

    [SerializeField, Header("最初の会話テキスト")] private DialogueText StartText;

    private GameManager gameManager;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();

        //Debug.Log("GameStart_ClassRoom");
        //gameManager.OpenTextWindow(StartText);
        if (gameManager.eventFlagData.GameStart)
        {
            Debug.Log("GameStart_ClassRoom");
            gameManager.eventFlagData.GameStart = false;
            gameManager.OpenTextWindow(StartText);
        }
    }

    void Update()
    {
        
    }
}
