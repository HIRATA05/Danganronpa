using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCharaGym : MonoBehaviour, IReceiveSearch
{
    //体育館のキャラ

    [SerializeField] private DialogueText NormalText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

        gameManager.OpenTextWindow(NormalText);

    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //探索開始前は表示する
        if (gameManager.eventFlagData.AdventureStart)
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}
