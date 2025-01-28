using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractDoor : MonoBehaviour, IReceiveSearch
{
    //調べた時のテキスト情報をテキスト管理スクリプトに渡す

    [SerializeField] private DialogueText GameStartText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("ドアを調べた");
        //場合によってはフラグによって条件分岐
        if (gameManager.eventFlagData.GameStart_All_TalkStart)
        {
            //別の部屋に移動
            SceneManager.LoadScene("Map");
        }
        else
        {
            gameManager.OpenTextWindow(GameStartText);
        }

    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {

    }
}
