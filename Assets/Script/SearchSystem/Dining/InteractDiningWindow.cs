using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDiningWindow : MonoBehaviour, IReceiveSearch
{
    //調べた時のテキスト情報をテキスト管理スクリプトに渡す

    [SerializeField] private DialogueText NormalText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("窓を調べた");
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

    }
}
