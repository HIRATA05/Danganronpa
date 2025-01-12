using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWindowRopeGarden : MonoBehaviour, IReceiveSearch
{
    //2階開放後ロープ付き窓

    [SerializeField] private DialogueText F2MoveText; //2階への移動
    [SerializeField] private DialogueText MoveAfterText;//2階移動後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //2階への移動前
        if (!gameManager.eventFlagData.F2Intrusion)
        {
            gameManager.OpenTextWindow(F2MoveText);
        }
        //2階への移動後
        else
        {
            gameManager.OpenTextWindow(MoveAfterText);
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
